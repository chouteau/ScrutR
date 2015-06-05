using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Models
{
	public class Recipient : IRecipient
	{
		public string FullName { get; set; }
		public string Email { get; set; }
		public string MobileNumber { get; set; }
		public string ProcessId { get; set; }
	}
}
