using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace ScrutR.Repositories.Mapping
{
	public class NotificationMap : EntityTypeConfiguration<Datas.NotificationData>
	{
		public NotificationMap()
		{
			HasKey(i => i.Id);

			ToTable("ScrutR_Notification");
		}
	}
}
