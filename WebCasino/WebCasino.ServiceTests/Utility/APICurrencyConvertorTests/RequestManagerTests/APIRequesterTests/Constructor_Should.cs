using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net.Http;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestManager;

namespace WebCasino.ServiceTests.Utility.APICurrencyConvertorTests.RequestManagerTests.APIRequesterTests
{
	[TestClass]
	public class Constructor_Should
	{
		[TestMethod]
		public void ThrowArgumentNullException_WhenClientIsNull()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new APIRequester(null));
		}

		
		[TestMethod]
		public void CreateInstanceOfAPIRequester_WhenCorrectParametersArePassed()
		{
			var httpClientMock = new Mock<HttpClient>();
			

			Assert.IsInstanceOfType(new APIRequester(httpClientMock.Object), typeof(APIRequester));
		}
	}
}