using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Conditions
{
	public static class ConditionSerializer
	{

		public static string Serialize(IList<ConditionBase> listbase)
		{
			var list = new List<Conditions.SerializedCondition>();
			foreach (var item in listbase)
			{
				list.Add(new Conditions.SerializedCondition()
				{
					Id = item.Id ,
					ComparatorId = item.ComparatorId,
					PropertyName = item.PropertyName,
					PropertyValue = item.PropertyValue,
					BetweenValue = item.BetweenPropertyValue,
					FullTypeName = item.GetType().AssemblyQualifiedName,
					Name = item.Title
				});
			}

			var result = Newtonsoft.Json.JsonConvert.SerializeObject(list);
			return result;
		}

		public static IList<Conditions.ConditionBase> Deserialize(string content)
		{
			if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(content.Trim()))
			{
				return null;
			}

			var result = new List<Conditions.ConditionBase>();
			var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Conditions.SerializedCondition>>(content);
			foreach (var item in list)
			{
				var ncb = CreateInstance(item);
				if (ncb != null)
				{
					result.Add(ncb);
				}
			}

			return result;
		}

		private static Conditions.ConditionBase CreateInstance(SerializedCondition condition)
		{
			var type = Type.GetType(condition.FullTypeName);
			if (type == null)
			{
				GlobalConfiguration.Configuration.Logger.Error("Type {0} for condition does not exists", condition.FullTypeName);
				return null;		
			}

			Conditions.ConditionBase result = null;
			try
			{
				result = (Conditions.ConditionBase)Activator.CreateInstance(type, null);
				result.BetweenPropertyValue = condition.BetweenValue;
				result.ComparatorId = condition.ComparatorId;
				result.PropertyName = condition.PropertyName;
				result.PropertyValue = condition.PropertyValue;
				result.TitleFormat = condition.Name;
			}
			catch(Exception ex)
			{
				GlobalConfiguration.Configuration.Logger.Error(ex);
			}

			return result;
		}


	}
}
