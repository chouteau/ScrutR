using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Repositories.Datas
{
	public class NotificationData
	{
		public string Id { get; set; }
		public string EventName { get; set; }
		public string Entity { get; set; }
		public string FullTypeName { get; set; }
		public DateTime CreationDate { get; set; }
		public int Priority { get; set; }
		public string Creator { get; set; }
	}
}
