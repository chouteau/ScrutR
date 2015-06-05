using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Tests
{
	public class TestPublisher : ScrutR.Publishers.PublisherBase
	{
		public override string Id
		{
			get { return Guid.NewGuid().ToString(); }
		}

		public override string Name
		{
			get { return "TEST"; }
		}

		public override void SendNotification(Models.IRecipient recipient, string title, string message)
		{
			NotificationServiceTests.Subject = title;
		}
	}
}
