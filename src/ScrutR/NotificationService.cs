using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrutR
{
	public class NotificationService : INotifier
	{
		public NotificationService(Lazy<Repositories.IRepository> repository)
		{
			Repository = repository;
		}

		protected Lazy<Repositories.IRepository> Repository { get; private set; }

		internal Models.Notification CreationNotification()
		{
			var notification = new Models.Notification();
			notification.Id = Guid.NewGuid().ToString();
			notification.CreationDate = DateTime.Now;
			notification.Priority = Priority.Normal;
			return notification;
		}

		public virtual Task PushAsync<T>(string eventName, T entity, string assemblyQualifiedName = null, Models.IRecipient creator = null, Priority priority = Priority.Normal)
			where T : class, new()
		{
			return Task.Run(() =>
			{
				Push(eventName, entity, assemblyQualifiedName, creator, priority);
			});
		}

		internal void Push<T>(string eventName, T entity, string assemblyQualifiedName, Models.IRecipient creator = null, Priority priority = Priority.Normal)
			where T : class, new()
		{
			var notification = CreationNotification();
			notification.Priority = priority;
			notification.Entity = entity;
			notification.FullType = assemblyQualifiedName ?? typeof(T).AssemblyQualifiedName;
			notification.EventName = eventName;
			notification.Creator = creator;

			try
			{
				Repository.Value.SaveNotification(notification);
			}
			catch (Exception ex)
			{
				GlobalConfiguration.Configuration.Logger.Error(ex);
			}
		}
	}
}
