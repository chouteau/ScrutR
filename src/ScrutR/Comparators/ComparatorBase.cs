using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Comparators
{
	[Serializable]
	public abstract class ComparatorBase
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual string NameFormat { get; set; }
		public ComparatorMode Mode;

		public virtual string GetFormatedName(Conditions.ConditionBase nc)
		{
			return string.Format(NameFormat, nc.PropertyValue, nc.BetweenPropertyValue);
		}

		public virtual bool Evaluate(IComparable propertyValue, IComparable value)
		{
			throw new NotImplementedException();
		}
		public virtual bool Evaluate(IComparable propertyValue, IComparable value, IComparable betweenvalue)
		{
			throw new NotImplementedException();
		}
		public virtual bool Evaluate<T>(List<T> propertyValues, IComparable value)
		{
			throw new NotImplementedException();
		}
	}

}
