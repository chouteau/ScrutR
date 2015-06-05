using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ScrutR.Conditions
{
	public class SerializedCondition
	{
		public string Id { get; set; }
		public string PropertyName { get; set; }
		public int ComparatorId { get; set; }
		public object PropertyValue { get; set; }
		public object BetweenValue { get; set; }
		public string Name { get; set; }
		public string FullTypeName { get; set; }
	}
}
