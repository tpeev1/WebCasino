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
			var retryHelperMock = new Mock<RetryHelper>();

			Assert.ThrowsException<ArgumentNullException>(() => new APIRequester(null, retryHelperMock.Object));
		}

		[TestMethod]
		public void ThrowArgumentNullException_WhenRetryHelperIsNull()
		{
			var httpClientMock = new Mock<HttpClient>();

			Assert.ThrowsException<ArgumentNullException>(() => new APIRequester(httpClientMock.Object, null));
		}

		[TestMethod]
		public void CreateInstanceOfAPIRequester_WhenCorrectParametersArePassed()
		{
			var httpClientMock = new Mock<HttpClient>();
			var retryHelperMock = new Mock<RetryHelper>();

			Assert.IsInstanceOfType(new APIRequester(httpClientMock.Object, retryHelperMock.Object), typeof(APIRequester));
		}
	}
}