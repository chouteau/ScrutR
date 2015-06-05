using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Publishers
{
	public class EmailPublisher : ScrutR.Publishers.PublisherBase
	{
		public override string Id
		{
			get { return "36AEA8E1-D0E4-43e1-8353-456110281613"; }
		}

		public override string Name
		{
			get
			{
				return "Email";
			}
		}

		public override void SendNotification(Models.IRecipient recipient, string title, string message)
		{
			if (recipient.Email == null || string.IsNullOrWhiteSpace(recipient.Email.Trim()))
			{
				return;
			}

			var mailMessage = new System.Net.Mail.MailMessage();

			mailMessage.Subject = title;
			mailMessage.Body = message;
			mailMessage.From = new System.Net.Mail.MailAddress(GlobalConfiguration.Configuration.Settings.FromEmail, GlobalConfiguration.Configuration.Settings.FromName);
			mailMessage.To.Add(new System.Net.Mail.MailAddress(recipient.Email, recipient.FullName));

			var client = new System.Net.Mail.SmtpClient();
			try
			{
				client.Send(mailMessage);
			}
			catch (Exception exe)
			{
				exe.Data.Add("smtp.host", client.Host);
				exe.Data.Add("smtp.port", client.Port);
				exe.Data.Add("smtp.timeout", client.Timeout);
				exe.Data.Add("smtp.deliverymethod", client.DeliveryMethod);
				exe.Data.Add("smtp.enablessl", client.EnableSsl);
				exe.Data.Add("smtp.usedefaultcredentials", client.UseDefaultCredentials);
				GlobalConfiguration.Configuration.Logger.Error(exe);
				throw;
			}
		}

		public override void Initialize()
		{
			//
		}

	}
}
