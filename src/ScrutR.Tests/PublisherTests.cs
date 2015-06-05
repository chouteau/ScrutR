using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrutR.Tests
{
	[TestClass]
	public class PublisherTests
	{
		[TestMethod]
		public void Place_Holder()
		{
			var emailPublisher = new Publishers.EmailPublisher();

			var product = new Product();

			product.Title = "TestProduct";
			var input = "the {Entity.Title}";
			var result = emailPublisher.ApplyPlaceHolder(input, product);

			Assert.AreEqual(result, "the TestProduct");
		}

		[TestMethod]
		public void Not_Exists_Place_Holder()
		{
			var emailPublisher = new Publishers.EmailPublisher();

			var product = new Product();

			product.Title = "TestProduct";
			var input = "the {Entity.Test}";
			var result = emailPublisher.ApplyPlaceHolder(input, product);

			Assert.AreEqual(result, "the {Entity.Test}");

		}
	}
}
