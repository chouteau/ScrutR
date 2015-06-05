using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Comparators
{
	/// <summary>
	/// Comparateur, est compris entre 2 valeurs
	/// </summary>
	public class BetweenComparator : ComparatorBase
	{
		public BetweenComparator()
		{
			Id = -6;
			Name = "Compris entre";
			NameFormat = "compris entre {0} et {1}";
			Mode = ComparatorMode.Range;
		}

		public override bool Evaluate(IComparable x, IComparable y, IComparable z)
		{
			var from = x.CompareTo(y);
			var to = x.CompareTo(z);

			if (from >= 0 && to <= 0)
			{
				return true;
			}
			return false;
		}
	}
}
