using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Comparators
{
	/// <summary>
	/// Comparateur , est egal a
	/// </summary>
	public class EqualsComparator : ComparatorBase
	{
		public EqualsComparator()
		{
			Id = -1;
			Name = "Egal à";
			NameFormat = "égal à {0}";
			Mode = ComparatorMode.Value;
		}

		public override bool Evaluate(IComparable propertyValue, IComparable value)
		{
			return propertyValue.Equals(value);
		}
	}
}
