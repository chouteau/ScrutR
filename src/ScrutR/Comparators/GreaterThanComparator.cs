using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Comparators
{
	/// <summary>
	/// Comparateur Plus grand que
	/// </summary>
	public class GreaterThanComparator : ComparatorBase
	{
		public GreaterThanComparator()
		{
			Id = -4;
			Name = "Supérieur à";
			NameFormat = "supérieur à {0}";
			Mode = ComparatorMode.Value;
		}

		public override bool Evaluate(IComparable x, IComparable y)
		{
			var result = x.CompareTo(y);
			if (result <= 0)
			{
				return false;
			}
			return true;
		}
	}
}
