using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrutR.Models
{
	public interface IRecipient
	{
		string FullName { get; set; }
		string Email { get; set; }
		string MobileNumber { get; set; }
		string ProcessId { get; set; }
	}
}
