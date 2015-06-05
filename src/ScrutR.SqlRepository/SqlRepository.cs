using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScrutR.Models;

namespace ScrutR.Repositories
{
	public class SqlRepository : CloudSoft.Repositories.SqlRepository<ScrutRDbContext>
		, IRepository
	{
		public virtual IList<Models.Notification> GetAllNofication(int pageIndex = 0, int pageSize = 100)
		{
			var query = (from notification in Query<Datas.NotificationData>()
					   orderby new { notification.Priority, notification.CreationDate }
					   select notification).Skip(pageIndex * pageSize).Take(pageSize);
			
			var result= new List<Models.Notification>();
			foreach(var item in query)
			{
				result.Add(item.ConvertToModel());
			}

			return result;
		}

		public virtual IList<Models.Subscription> GetAllSubscription()
		{
			var query = from subscription in Query<Datas.SubscriptionData>()
					   select subscription;

			var result = new List<Models.Subscription>();
			foreach (var item in query)
			{
				result.Add(item.ConvertToModel());
			}

			return result;
		}

		public virtual void ResetNotifications()
		{
			// ExecuteStoreQuery<Datas.NotificationData>("delete scrutr_notification");
		}

		public virtual void SaveNotification(Models.Notification notification)
		{
			var data = notification.ConvertToData();
			Insert(data);
		}

		public virtual void DeleteNotification(Models.Notification notification)
		{
			var data = notification.ConvertToData();
			Delete(data);
		}

		public virtual void SaveSubscription(Models.Subscription subscription)
		{
			var data = subscription.ConvertToData();
			var exists = Get<Repositories.Datas.SubscriptionData>(i => i.Id == subscription.Id);
			if (exists == null)
			{
				Insert(data);
			}
			else
			{
				Update(data);
			}
		}

		public virtual void DeleteSubscription(Models.Subscription subscription)
		{
			var data = Get<Datas.SubscriptionData>(i => i.Id == subscription.Id);
			if (data != null)
			{
				Delete(data);
			}
		}

		public static Repositories.IRepository InitializeRepository()
		{
			var dbContextFactory = new CloudSoft.Repositories.DbContextFactory<Repositories.ScrutRDbContext>();
			var schemaInitializer = new CloudSoft.Repositories.Initializers.SqlSchemaInitializer<Repositories.ScrutRDbContext>(dbContextFactory);
			schemaInitializer.Initialize("_scrutr_schema", null);
			var result = new Repositories.SqlRepository();
			return result;
		}
	}
}
