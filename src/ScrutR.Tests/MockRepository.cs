using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Tests
{
	public class MockRepository : ScrutR.Repositories.IRepository
	{
		private List<Publishers.PublisherBase> m_Publishers;
		private List<Conditions.ConditionBase> m_Conditions;
		private List<Models.Notification> m_Notifications;
		private List<Models.Subscription> m_Subscriptions;

		public MockRepository()
		{
			m_Publishers = new List<Publishers.PublisherBase>();
			m_Conditions = new List<Conditions.ConditionBase>();
			m_Notifications = new List<Models.Notification>();
			m_Subscriptions = new List<Models.Subscription>();
		}

		public void ResetNotifications()
		{
			m_Notifications.Clear();
		}

		public IList<Models.Notification> GetAllNofication(int pageIndex, int pageSize)
		{
			return m_Notifications.Skip(pageIndex * pageSize).Take(pageSize).ToList();
		}

		public IList<Models.Subscription> GetAllSubscription()
		{
			return m_Subscriptions;
		}

		public void SaveNotification(Models.Notification notification)
		{
			m_Notifications.Add(notification);
		}

		public void DeleteNotification(Models.Notification notification)
		{
			m_Notifications.Remove(notification);
		}

		public void SaveSubscription(Models.Subscription subscription)
		{
			m_Subscriptions.Add(subscription);
		}

		public void DeleteSubscription(Models.Subscription subscription)
		{
			m_Subscriptions.Remove(subscription);
		}
	}
}
