using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestManager;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestManager.Contracts;
using WebCasino.ServiceTests.Utility.APICurrencyConvertorTests.RequestManagerTests.APIRequesterTests.Fakes;

namespace WebCasino.ServiceTests.Utility.APICurrencyConvertorTests.RequestManagerTests.APIRequesterTests
{
	[TestClass]
	public class Request_Should
	{
		[TestMethod]
		public async Task ThrowArgumentNullException_WhenConnectionIsNull()
		{
			var httpClientMock = new Mock<HttpClient>();
			
			var apiRequester = new APIRequester(httpClientMock.Object);

			await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => apiRequester.Request(null));
		}

		[TestMethod]
		public async Task ThrowInvalidOperationException_WhenResultIsNull()
		{
			var httpClientMock = new Mock<HttpClient>();

			var apiRequester = new APIRequester(httpClientMock.Object);

			await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => apiRequester.Request("connection"));
		}

		
		//[TestMethod]
		public async Task SucesfulRequest()
		{
		}
	}
}