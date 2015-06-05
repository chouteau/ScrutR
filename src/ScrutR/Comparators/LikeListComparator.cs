using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScrutR.Comparators
{
	public class LikeListComparator : ComparatorBase
	{
		public LikeListComparator()
		{
			Id = -8;
			Name = "Correspond à un élément de la liste";
			NameFormat = "correspond à {0}";
			Mode = ComparatorMode.List;
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
