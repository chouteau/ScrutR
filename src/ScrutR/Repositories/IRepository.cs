using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Repositories
{
	public interface IRepository
	{
		void ResetNotifications();
		IList<Models.Notification> GetAllNofication(int pageIndex = 0, int pageSize = 100);
		IList<Models.Subscription> GetAllSubscription();
		void SaveNotification(Models.Notification notification);
		void DeleteNotification(Models.Notification notification);
		void SaveSubscription(Models.Subscription subscription);
		void DeleteSubscription(Models.Subscription subscription);
	}
}
