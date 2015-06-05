using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Entity;

namespace ScrutR.Repositories
{
	public class ScrutRDbContext : System.Data.Entity.DbContext
	{
		public ScrutRDbContext()
			: base("name=ScrutRConnectionString")
		{
			var csBuilder = new SqlConnectionStringBuilder(this.Database.Connection.ConnectionString);
			csBuilder.ApplicationName = this.GetType().Assembly.FullName + "," + System.Environment.MachineName;
			this.Database.Connection.ConnectionString = csBuilder.ConnectionString;
			Configuration.ProxyCreationEnabled = false;
			Configuration.AutoDetectChangesEnabled = true;
			Configuration.LazyLoadingEnabled = true;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Configurations.Add(new Mapping.NotificationMap());
			modelBuilder.Configurations.Add(new Mapping.SubscriptionMap());
		}
	}
}
