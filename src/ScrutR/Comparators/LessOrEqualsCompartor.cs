using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Comparators
{
	/// <summary>
	/// Comparateur , plus petit ou egaal a
	/// </summary>
	public class LessOrEqualsCompartor : ComparatorBase
	{
		public LessOrEqualsCompartor()
		{
			Id = -3;
			Name = "Inférieur ou égal à";
			NameFormat = "inférieur ou égal à {0}";
			Mode = ComparatorMode.Value;
		}

		public override bool Evaluate(IComparable x, IComparable y)
		{
			var result = x.CompareTo(y);
			if (result >= 0)
			{
				return true;
			}
			return false;
		}
	}
}
