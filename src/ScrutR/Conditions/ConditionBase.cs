using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScrutR.Conditions
{
	/// <summary>
	/// Represente un element de condition pour que la notification ai lieu
	/// </summary>
	public abstract class ConditionBase : ICloneable
	{
		public ConditionBase()
		{
			Initialize();
		}

		public ConditionBase(string titleFormat, string propertyName)
			: this()
		{
			this.TitleFormat = titleFormat;
			this.PropertyName = propertyName;
		}

		protected abstract void Initialize();
		public abstract string Id { get; }
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string TitleFormat { get; set; }
		/// <summary>
		/// Gets or sets the name of the property.
		/// </summary>
		/// <value>The name of the property.</value>
		public string PropertyName { get; set; }
		/// <summary>
		/// Gets or sets the property value.
		/// </summary>
		/// <value>The property value.</value>
		public object PropertyValue { get; set; }
		/// <summary>
		/// Gets or sets the comparator.
		/// </summary>
		/// <value>The comparator.</value>
		[System.Xml.Serialization.XmlIgnore]
		public Comparators.ComparatorBase SelectedComparator { get; set; }
		/// <summary>
		/// Gets or sets the between property value.
		/// </summary>
		/// <value>The between property value.</value>
		public object BetweenPropertyValue { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance is selected.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
		/// </value>
		[System.Xml.Serialization.XmlIgnore]
		public bool IsSelected { get; set; }

		/// <summary>
		/// Gets or sets the comparator list.
		/// </summary>
		/// <value>The comparator list.</value>
		[System.Xml.Serialization.XmlIgnore]
		public List<Comparators.ComparatorBase> ComparatorList { get; set; }

		[Bindable(true)]
		public int ComparatorId
		{
			get
			{
				if (SelectedComparator != null)
				{
					return SelectedComparator.Id;
				}
				return 0;
			}
			set
			{
				SelectedComparator = ComparatorList.Find(delegate(Comparators.ComparatorBase i)
				{
					return i.Id.Equals(value);
				});
			}
		}


		[System.Xml.Serialization.XmlIgnore]
		public virtual string Title
		{
			get
			{
				return string.Format(TitleFormat, DefaultPartText);
			}
		}

		[System.Xml.Serialization.XmlIgnore]
		public abstract string DefaultPartText { get; }

		[System.Xml.Serialization.XmlIgnore]
		public virtual string PartText 
		{
			get
			{
				this.Validate();
				if (this.IsValid)
				{
					return this.SelectedComparator.GetFormatedName(this);
				}
				return DefaultPartText;
			}
		}

		[System.Xml.Serialization.XmlIgnore]
		public virtual string TitleWithCondition
		{
			get
			{
				return string.Format(Title, PartText);
			}
		}

		/// <summary>
		/// Evaluates the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		public abstract bool Evaluate(object entity);

		/// <summary>
		/// Updates the value.
		/// </summary>
		/// <param name="comparator">The comparator.</param>
		/// <param name="value">The value.</param>
		public virtual void UpdateValue(Comparators.ComparatorBase comparator, object value)
		{
			this.SelectedComparator = comparator;
			this.PropertyValue = value;
		}

		/// <summary>
		/// Updates the value.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		public virtual void UpdateValue(object from, object to)
		{
			// this.Comparator = Comparator.Between;
			this.PropertyValue = from;
			this.BetweenPropertyValue = to;
		}

		public bool IsValid { get; set; }

		public void Validate()
		{
			if (this.SelectedComparator == null)
			{
				IsValid = false;
				return;
			}

			if (this.SelectedComparator.Mode == ComparatorMode.Value 
				&& this.PropertyValue == null)
			{
				IsValid = false;
				return;
			}

			if (this.SelectedComparator.Mode == ComparatorMode.Range 
				&& (this.PropertyValue == null || this.BetweenPropertyValue == null))
			{
				IsValid = false;
				return;
			}

			if (this.SelectedComparator.Mode == ComparatorMode.List 
				&& this.PropertyValue == null)
			{
				IsValid = false;
				return;
			}

			IsValid = true;
		}

		public virtual IEnumerable<ConditionListValue> GetValueList()
		{
			throw new NotImplementedException();
		}

		public abstract void AddToPropertyValue(object item);
		public abstract void RemoveFromPropertyValue(object item);

		public virtual void ClearPropertyValue<T>()
		{
			if (PropertyValue != null)
			{
				PropertyValue = null;
			}
		}

		protected void AddToPropertyValue<T>(T item)
		{
			var list = PropertyValue as List<T>;
			if (list == null)
			{
				list = new List<T>();
			}
			if (!list.Contains((T)item))
			{
				list.Add((T)item);
			}
			PropertyValue = list;
		}

		protected void RemoveFromPropertyValue<T>(T item)
		{
			var list = PropertyValue as List<T>;
			if (list == null)
			{
				return;
			}
			if (list.Contains((T)item))
			{
				list.Remove((T)item);
			}
			if (list.Count == 0)
			{
				PropertyValue = list;
			}
			else
			{
				PropertyValue = list;
			}
		}

		public bool ContainsPropertyValue(ConditionListValue item)
		{
			if (PropertyValue is System.Collections.IList)
			{
				return ((System.Collections.IList)PropertyValue).Contains(item.Key);
			}
			return false;
		}

		#region ICloneable Members

		public virtual object Clone()
		{
			ConditionBase o = (ConditionBase) this.MemberwiseClone();
			return o;
		}

		#endregion

	}
}
