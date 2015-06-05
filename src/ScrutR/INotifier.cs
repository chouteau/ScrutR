using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrutR
{
	public interface INotifier
	{
		Task PushAsync<T>(string eventName, T entity, string assemblyQualifiedName = null, Models.IRecipient creator = null, Priority priority = Priority.Normal)
			where T : class, new();
	}
}
