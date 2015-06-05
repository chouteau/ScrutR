using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Conditions
{
	public static class ConditionExtensions
	{
		public static IEnumerable<ConditionListValue> ToConditionValueList<T, TSource>(this IEnumerable<TSource> list, Func<TSource, T> keySelector, Func<TSource, string> valueSelector)
		{
			var result = new List<ConditionListValue>();
			foreach (var item in list)
			{
				var key = keySelector.Invoke(item);
				var value = valueSelector.Invoke(item);
				result.Add(new ConditionListValue()
					{
						Key = key,
						Label = value,
					});
			}
			return result;
		}
	}
}
