using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Models
{
	public static class ModelMapping
	{
		public static Notification ConvertToModel(this Repositories.Datas.NotificationData data)
		{
			var result = new Notification();
			result.CreationDate = data.CreationDate;
			if (result.Creator != null)
			{
				result.Creator = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Recipient>(data.Creator);
			}
			result.Entity = Newtonsoft.Json.JsonConvert.DeserializeObject(data.Entity, Type.GetType(data.FullTypeName));
			result.EventName = data.EventName;
			result.FullType = data.FullTypeName;
			result.Id = data.Id;
			result.Priority = (Priority)data.Priority;

			return result;
		}

		public static Repositories.Datas.NotificationData ConvertToData(this Models.Notification model)
		{
			var result = new Repositories.Datas.NotificationData();
			result.CreationDate = model.CreationDate;
			if (model.Creator != null)
			{
				result.Creator = Newtonsoft.Json.JsonConvert.SerializeObject(model.Creator);
			}
			result.Entity = Newtonsoft.Json.JsonConvert.SerializeObject(model.Entity);
			result.EventName = model.EventName;
			result.FullTypeName = model.FullType;
			result.Id = model.Id;
			result.Priority = (int)model.Priority;

			return result;
		}

		public static Subscription ConvertToModel(this Repositories.Datas.SubscriptionData data)
		{
			var result = new Subscription();
			result.BodyFormat = data.BodyFormat;
			result.Collector = (Collector)data.Collector;
			result.ConditionList = Conditions.ConditionSerializer.Deserialize(data.ConditionList);
			result.CreationDate = data.CreationDate;
			result.EventName = data.EventName;
			result.FullTypeName = data.FullTypeName;
			result.Id = data.Id;
			result.PublisherList = new List<Publishers.PublisherBase>();
			var publisherTypeList = data.PublisherList.Split('|');
			foreach (var item in publisherTypeList)
			{
				if (publisherTypeList.Any(i => i.GetType().FullName == item))
				{
					continue;
				}

				var type = Type.GetType(item);
				if (type == null)
				{
					GlobalConfiguration.Configuration.Logger.Error("type {0} for publisher does not exists", type);
					continue;
				}

				var publisher = (Publishers.PublisherBase)Activator.CreateInstance(type, null);
				result.PublisherList.Add(publisher);
			}
			if (data.Recipient != null)
			{
				result.Recipient = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Recipient>(data.Recipient);
			}
			result.SubjectFormat = data.SubjectFormat;

			return result;
		}

		public static Repositories.Datas.SubscriptionData ConvertToData(this Subscription model)
		{
			var result = new Repositories.Datas.SubscriptionData();
			result.BodyFormat = model.BodyFormat;
			result.Collector = (int)model.Collector;
			result.ConditionList = Conditions.ConditionSerializer.Serialize(model.ConditionList);
			result.CreationDate = model.CreationDate;
			result.EventName = model.EventName;
			result.FullTypeName = model.FullTypeName;
			result.Id = model.Id;
			result.PublisherList = string.Join("|",model.PublisherList.Select(i => i.GetType().AssemblyQualifiedName));
			result.Recipient = Newtonsoft.Json.JsonConvert.SerializeObject(model.Recipient);
			result.SubjectFormat = model.SubjectFormat;

			return result;
		}
	}
}
