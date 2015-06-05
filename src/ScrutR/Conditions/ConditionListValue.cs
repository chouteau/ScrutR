using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Conditions
{
	public class ConditionListValue 
	{
		public ConditionListValue()
		{
		}

		public object Key { get; set; }
		public string Label { get; set; }

		public override string ToString()
		{
			return Label;
		}
	}
}
