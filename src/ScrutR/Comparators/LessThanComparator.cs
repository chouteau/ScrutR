using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Comparators
{
	/// <summary>
	/// Compareteur, est plus petit que
	/// </summary>
	public class LessThanComparator : ComparatorBase
	{
		public LessThanComparator()
		{
			Id = -2;
			Name = "Inférieur à";
			NameFormat = "inférieur à {0}";
			Mode = ComparatorMode.Value;
		}

		public override bool Evaluate(IComparable x, IComparable y)
		{
			var result = x.CompareTo(y);
			if (result > 0)
			{
				return true;
			}
			return false;
		}
	}
}
