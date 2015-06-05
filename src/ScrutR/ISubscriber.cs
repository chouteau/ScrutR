using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrutR
{
	public interface ISubscriber
	{
		Models.Subscription CreateSubscription();
		Models.Subscription GetSubscriptionById(string id);
		void AddSubscription(Models.Subscription subscription);
		void DeleteSubscription(Models.Subscription subscription);
		void ProcessNotification(Models.Notification notification);
		void ResetNotifications();
		void Start();
		void Stop();
	}
}
