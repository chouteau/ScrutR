using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Models
{
	public class Subscription
	{
		public Subscription()
		{
			PublisherList = new List<Publishers.PublisherBase>();
			ConditionList = new List<Conditions.ConditionBase>();
		}

		public string Id { get; set; }
		public string FullTypeName { get; set; } 
		public IRecipient Recipient { get; set; }
		public Collector Collector { get; set; }
		public IList<Conditions.ConditionBase> ConditionList { get; set; }
		public IList<Publishers.PublisherBase> PublisherList { get; set; }
		public string EventName { get; set; }
		public string SubjectFormat { get; set; }
		public string BodyFormat { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
