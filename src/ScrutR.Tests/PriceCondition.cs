using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Tests
{
	public class PriceCondition : ScrutR.Conditions.ConditionBase
	{
		public PriceCondition(string name, string propertyName)
			: base(name, propertyName)
		{
	
		}

		public override string Id
		{
			get
			{
				return "E6F0BA94-C999-4D34-AEF1-92B1AB899725";
			}
		}

		protected override void Initialize()
		{
			ComparatorList = new List<Comparators.ComparatorBase>();
			ComparatorList.Add(new Comparators.EqualsComparator());
			ComparatorList.Add(new Comparators.LessThanComparator());
			ComparatorList.Add(new Comparators.LessOrEqualsCompartor());
			ComparatorList.Add(new Comparators.GreaterThanComparator());
			ComparatorList.Add(new Comparators.GreaterThanOrEqualsComparator());
			ComparatorList.Add(new Comparators.BetweenComparator());
		}

		public override string DefaultPartText
		{
			get { return "price" ; }
		}

		public override bool Evaluate(object entity)
		{
			var pi = entity.GetType().GetProperty(this.PropertyName);
			decimal value = Convert.ToDecimal(pi.GetValue(entity, null));
			decimal propertyValue = Convert.ToDecimal(this.PropertyValue);
			if (SelectedComparator.Mode == ComparatorMode.Range)
			{
				var betweenValue = (decimal)this.BetweenPropertyValue;
				return SelectedComparator.Evaluate(propertyValue, value, betweenValue);
			}
			else
			{
				return SelectedComparator.Evaluate(propertyValue, value);
			}
		}

		public override void AddToPropertyValue(object item)
		{
			base.AddToPropertyValue((decimal)item);
		}

		public override void RemoveFromPropertyValue(object item)
		{
			base.RemoveFromPropertyValue((decimal)item);
		}
	}
}
