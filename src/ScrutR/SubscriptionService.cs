using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScrutR
{
	public class SubscriptionService : ISubscriber
	{
		private IList<Models.Subscription> m_InnerSubscriptionList;
		private IList<Models.Subscription> m_SubscriptionList;
		private System.Threading.Thread m_TimerThread;
		private System.Threading.ManualResetEvent m_EventStop;
		private System.Threading.ManualResetEvent m_NotificaitonEvent;

		public SubscriptionService(Lazy<Repositories.IRepository> repository)
		{
			Repository = repository;
			m_InnerSubscriptionList = new List<Models.Subscription>();
		}

		protected Lazy<Repositories.IRepository> Repository { get; private set; }

		public virtual Models.Subscription CreateSubscription()
		{
			var result = new Models.Subscription();
			result.Id = Guid.NewGuid().ToString();
			result.CreationDate = DateTime.Now;

			return result;
		}

		public virtual Models.Subscription GetSubscriptionById(string id)
		{
			var list = GetAllSubscriptions();
			return list.SingleOrDefault(i => i.Id == id);
		}

		public virtual IList<Models.Subscription> GetAllSubscriptions()
		{
			if (m_SubscriptionList != null)
			{
				return m_SubscriptionList.ToList();
			}

			m_SubscriptionList = Repository.Value.GetAllSubscription();
			foreach (var item in m_InnerSubscriptionList)
			{
				if (!m_SubscriptionList.Contains(item))
				{
					m_SubscriptionList.Add(item);
				}
			}

			return m_SubscriptionList.ToList();
		}

		public virtual void AddSubscription(Models.Subscription subscription)
		{
			if (!m_InnerSubscriptionList.Contains(subscription))
			{
				m_InnerSubscriptionList.Add(subscription);
				m_SubscriptionList = null;
			}
		}

		public virtual void SaveSubscription(Models.Subscription subscription)
		{
			Repository.Value.SaveSubscription(subscription);
			m_SubscriptionList = null;
		}

		public virtual void DeleteSubscription(Models.Subscription subscription)
		{
			try
			{
				Repository.Value.DeleteSubscription(subscription);
				m_SubscriptionList = null;
			}
			catch (Exception ex)
			{
				GlobalConfiguration.Configuration.Logger.Error(ex);
			}
		}

		public void ResetNotifications()
		{
			Repository.Value.ResetNotifications();
		}

		public virtual void Start()
		{
			m_NotificaitonEvent = new ManualResetEvent(false);
			m_EventStop = new System.Threading.ManualResetEvent(false);
			m_TimerThread = new System.Threading.Thread(Execute);
			m_TimerThread.IsBackground = true;
			m_TimerThread.Name = "TasksOnTime";
			m_TimerThread.Start();
		}

		private void Execute()
		{
			while (true)
			{
				try
				{
					ProcessPendingNotificationList();
				}
				catch (Exception ex)
				{
					GlobalConfiguration.Configuration.Logger.Error(ex);
				}

				var waitHandles = new WaitHandle[] { m_EventStop, m_NotificaitonEvent };
				int result = ManualResetEvent.WaitAny(waitHandles, GlobalConfiguration.Configuration.Settings.ScanInterval * 1000, true);
				if (result == 0)
				{
					break;
				}
				m_NotificaitonEvent.Reset();
			}
		}

		public virtual void Stop()
		{
			if (m_EventStop != null)
			{
				m_EventStop.Set();
			}

			if (m_TimerThread != null 
				&& !m_TimerThread.Join(TimeSpan.FromSeconds(5)))
			{
				m_TimerThread.Abort();
			}

		}

		internal void ProcessPendingNotificationList()
		{
			int pageIndex = 0;
			while (true)
			{
				var notificationList = Repository.Value.GetAllNofication(pageIndex);
				if (notificationList == null 
					|| notificationList.Count == 0)
				{
					break;
				}
				pageIndex++;
				foreach (var notification in notificationList)
				{
					ProcessNotification(notification);
				}
			}
		}

		public virtual void ProcessNotification(Models.Notification notification)
		{
			Repository.Value.DeleteNotification(notification);

			var subscriptionList = GetAllSubscriptions();
			// On recherche dans la liste des souscriptions qui est concerné
			// par l'evenement
			var sList = subscriptionList.Where(i => i.FullTypeName == notification.FullType);
			sList = sList.Where(i => i.EventName == notification.EventName);

			if (sList.Count() == 0)
			{
				// personne n'est concerné par cet evenement
				return;
			}

			//  On recherche dans la liste des abonnés qui est concerné;
			foreach (var subscription in sList)
			{
				// On verifie que l'abonné est bien concerné en fonction des conditions
				var match = Match(subscription.ConditionList, notification.Entity);
				if (!match)
				{
					continue;
				}

				foreach (var publisher in subscription.PublisherList)
				{
					try
					{
						var subject = publisher.ApplyPlaceHolder(subscription.SubjectFormat, notification.Entity);
						var body = publisher.ApplyPlaceHolder(subscription.BodyFormat, notification.Entity);
						publisher.Initialize();
						publisher.SendNotification(subscription.Recipient, subject, body);
					}
					catch (Exception ex)
					{
						GlobalConfiguration.Configuration.Logger.Error(ex);
						continue;
					}
				}

				if (subscription.Collector == Collector.Once)
				{
					DeleteSubscription(subscription);
				}
			}
		}

		private bool Match(IEnumerable<Conditions.ConditionBase> conditionList, object entity)
		{
			if (conditionList.Count() == 0)
			{
				return true;
			}

			// Si une seule condition ne match pas alors sortir
			foreach (var item in conditionList)
			{
				if (!item.Evaluate(entity))
				{
					return false;
				}
			}

			return true;
		}

	}
}
