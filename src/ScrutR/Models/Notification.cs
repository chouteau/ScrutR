using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Models
{
	public sealed class Notification
	{
		public string Id { get; set; }
		public string EventName { get; set; }
		public object Entity { get; set; }
		public string FullType { get; set; }
		public DateTime CreationDate { get; set; }
		public Priority Priority { get; set; }
		public IRecipient Creator { get; set; }
	}
}
