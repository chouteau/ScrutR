using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Repositories.Datas
{
	public class SubscriptionData
	{
		public string Id { get; set; }
		public string FullTypeName { get; set; }
		public string Recipient { get; set; }
		public int Collector { get; set; }
		public string ConditionList { get; set; }
		public string PublisherList { get; set; }
		public string EventName { get; set; }
		public string SubjectFormat { get; set; }
		public string BodyFormat { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
