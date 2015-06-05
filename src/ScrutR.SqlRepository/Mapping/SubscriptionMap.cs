using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace ScrutR.Repositories.Mapping
{
	public class SubscriptionMap : EntityTypeConfiguration<Datas.SubscriptionData>
	{
		public SubscriptionMap()
		{
			HasKey(i => i.Id);

			ToTable("ScrutR_Subscription");
		}
	}
}
