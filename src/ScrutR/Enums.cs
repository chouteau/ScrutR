using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR
{
	public enum Priority
	{
		High,
		Normal,
		Low
	}

	public enum Collector
	{
		Once,
		Minute,
		Hour,
		Day,
		Week,
		Month,
	}

	public enum ComparatorMode
	{
		Value,
		Range, 
		List
	}
}
