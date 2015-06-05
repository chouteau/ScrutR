using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScrutR.Comparators
{
	/// <summary>
	/// Comparateur, est dans la liste
	/// </summary>
	public class IntoComparator : ComparatorBase
	{
		public IntoComparator()
		{
			Id = -7;
			Name = "Est compris dans la liste";
			NameFormat = "est égal à {0}";
			Mode = ComparatorMode.List;
		}

		public override bool Evaluate(IComparable propertyValue, IComparable value)
		{
			// ça veut dire toute la liste
			// Normalement c'est impossible
			if (propertyValue == null)
			{
				return true;
			}
			var list = propertyValue as IList;
			return list.Contains(value);
		}

		public override bool Evaluate<T>(List<T> propertyValues, IComparable value)
		{
			// ça veut dire toute la liste
			// Normalement c'est impossible
			if (propertyValues == null)
			{
				return true;
			}
			return propertyValues.Contains((T)value);
		}

		public override string GetFormatedName(Conditions.ConditionBase nc)
		{
			string result = null;
			IEnumerable<Conditions.ConditionListValue> list = nc.GetValueList();
			string separator = null; 
			foreach (var item in list)
			{
				result += string.Format("{0}{1}", separator, item.Label);
				separator = " ou ";
			}
			return string.Format(NameFormat, result);
		}

	}
}
