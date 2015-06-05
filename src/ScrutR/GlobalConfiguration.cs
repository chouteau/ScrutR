using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR
{
	public class GlobalConfiguration
	{
		private static Lazy<ScrutRConfiguration> m_Configuration
			= new Lazy<ScrutRConfiguration>(() =>
				{
					var result = new ScrutRConfiguration();
					result.Settings = new Settings();
					result.Logger = new DiagnosticsLogger();

					return result;
				}, true);

		public static ScrutRConfiguration Configuration
		{
			get
			{
				return m_Configuration.Value;
			}
		}
	}
}
