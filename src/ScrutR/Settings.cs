using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR
{
	public class Settings
	{
		public Settings()
		{
			FromEmail = Configuration.ConfigurationSettings.AppSettings["fromEmail"];
			FromName = Configuration.ConfigurationSettings.AppSettings["fromName"];
			ScanInterval = Convert.ToInt32(Configuration.ConfigurationSettings.AppSettings["scanInterval"] ?? "60");
			RootPath = Configuration.ConfigurationSettings.AppSettings["rootPath"];
		}

		public string FromEmail { get; set; }
		public string FromName { get; set; }
		public int ScanInterval { get; set; }
		public string RootPath { get; set; }
	}
}
