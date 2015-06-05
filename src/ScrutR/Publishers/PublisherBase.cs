using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Publishers
{
	public abstract class PublisherBase
	{
		public abstract string Id { get; }
		public abstract string Name { get; }

		public abstract void SendNotification(Models.IRecipient recipient, string title, string message);

		public virtual string ApplyPlaceHolder(string input, object entity)
		{
			if (input == null)
			{
				return input;
			}
			var result = input;
			var propertyList = entity.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
			foreach (var propertyInfo in propertyList)
			{
				if (propertyInfo.MemberType == System.Reflection.MemberTypes.Property
					&& !propertyInfo.IsSpecialName)
				{
					// indexed properties
					var ps = propertyInfo.GetGetMethod().GetParameters();
					if (ps != null
						&& ps.Length == 0)
					{
						var value = propertyInfo.GetValue(entity, null);
						if (value != null)
						{
							result = result.Replace("{Entity." + propertyInfo.Name + "}", value.ToString());
						}
					}
				}
			}
			return result;
		}
		
		public virtual void Initialize()
		{
		}

	}
}
