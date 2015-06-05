using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ScrutR.Tests
{
	[TestClass]
	public class NotificationServiceTests
	{
		[TestInitialize]
		public void Initialize()
		{
			// var repository = new Lazy<ScrutR.Repositories.IRepository>(ScrutR.Repositories.SqlRepository.InitializeRepository);
			var repository = new Lazy<ScrutR.Repositories.IRepository>(() => new MockRepository());

			NotificationService = new NotificationService(repository);
			SubscriptionService = new SubscriptionService(repository);
			// NotificationService.Repository = new MockRepository();
			SubscriptionService.Start();
		}

		protected ScrutR.INotifier NotificationService { get; private set; }
		protected ScrutR.ISubscriber SubscriptionService { get; private set; }

		[TestMethod]
		public void Send_Notification()
		{
			SubscriptionService.Stop();
			SubscriptionService.ResetNotifications();

			var person = new Models.Recipient();
			person.Email = "test@email.com";

			var product = new Product();
			product.Code = "Test";
			product.Price = 10;
			product.Stock = 1;
			product.Title = "Product test";

			var priceCondition = new PriceCondition("price", "Price");
			priceCondition.SelectedComparator = new Comparators.GreaterThanOrEqualsComparator();
			priceCondition.PropertyValue = 10;

			var subscription = SubscriptionService.CreateSubscription();
			subscription.Collector = Collector.Once;
			subscription.FullTypeName = typeof(Product).AssemblyQualifiedName;
			subscription.ConditionList.Add(priceCondition);
			subscription.PublisherList = new List<Publishers.PublisherBase>();
			var publisher = new TestPublisher();
			subscription.PublisherList.Add(publisher);
			subscription.EventName = "modification";
			subscription.Recipient = person;
			subscription.SubjectFormat = "Price greater than 10";
			subscription.BodyFormat = "Price greater than 10 for product {Product.Code}";

			SubscriptionService.AddSubscription(subscription);

			NotificationService.PushAsync("modification", product, product.GetType().AssemblyQualifiedName).Wait();

			((SubscriptionService) SubscriptionService).ProcessPendingNotificationList();
			SubscriptionService.DeleteSubscription(subscription);

			Assert.AreEqual(Subject, subscription.SubjectFormat);

			SubscriptionService.Stop();
		}

		public static string Subject { get; set; }
	}
}
