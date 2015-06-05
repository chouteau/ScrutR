using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Client35
{
    public class Notifier
    {
		public Notifier()
		{

		}

		public string BaseUrl { get; set; }
		public string ApiKey { get; set; }

		public void Push(string eventName, object model, string assembyQualifiedName, Action<Exception> callBack, Models.IRecipient person = null, Priority priority = Priority.Normal)
		{
			var resource = "/api/scrutrapi/postnotification";
			var request = new RestSharp.RestRequest(resource, RestSharp.Method.POST);
			request.RequestFormat = RestSharp.DataFormat.Json;
			request.XmlSerializer.DateFormat = "yyyy-MM-ddTHH:mm:ss.fff";
			request.JsonSerializer.DateFormat = "yyyy-MM-ddTHH:mm:ss.fff";
			request.AddHeader("apikey", ApiKey);
			request.AddHeader("Accept", "application/json, text/json, text/x-json");
			var body = new
			{
				EventName = eventName,
				Model = model,
				AssemblyQualifiedName = assembyQualifiedName,
				Person = person,
				Priority = priority
			};
			request.AddBody(body);

			var client = new RestSharp.RestClient(BaseUrl);

			var h = client.ExecuteAsync(request, (response, handle) =>
			{
				if (response.StatusCode != System.Net.HttpStatusCode.OK)
				{
					if (response.ErrorException != null)
					{
						callBack.Invoke(response.ErrorException);
					}
				}
			});
		}

    }
}
