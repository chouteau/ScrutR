using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Comparators
{
	/// <summary>
	/// Comparateur , supérieur ou egal à
	/// </summary>
	public class GreaterThanOrEqualsComparator : ComparatorBase
	{
		public GreaterThanOrEqualsComparator()
		{
			Id = -5;
			Name = "Supérieur ou égal à";
			NameFormat = "supérieur ou égal à {0}";
			Mode = ComparatorMode.Value;
		}

		public override bool Evaluate(IComparable x, IComparable y) 
		{
			var result = x.CompareTo(y);
			if (result <= 0)
			{
				return true;
			}
			return false;
		}
	}
}
