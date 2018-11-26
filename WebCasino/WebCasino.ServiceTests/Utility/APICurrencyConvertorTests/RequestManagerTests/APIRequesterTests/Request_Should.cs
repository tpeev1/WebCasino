using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestManager;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestManager.Contracts;

namespace WebCasino.ServiceTests.Utility.APICurrencyConvertorTests.RequestManagerTests.APIRequesterTests
{
	[TestClass]
	public class Request_Should
	{
		[TestMethod]
		public async Task ThrowArgumentNullException_WhenConnectionIsNull()
		{
			var httpClientMock = new Mock<HttpClient>();
			var retryHelperMock = new Mock<RetryHelper>();

			var apiRequester = new APIRequester(httpClientMock.Object, retryHelperMock.Object);

			await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => apiRequester.Request(null, 1));
		}

		[TestMethod]
		public async Task ThrowArgumentNullException_WhenResultIsNull()
		{
			var httpClientMock = new Mock<HttpClient>();
			var retryHelperMock = new Mock<IRetryHelper>();

			var timeSpan = TimeSpan.FromSeconds(3);

			Func<Task> operation = () => Task.CompletedTask;

			var apiRequester = new APIRequester(httpClientMock.Object, retryHelperMock.Object);

			await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => apiRequester.Request("connection", 3));
		}

		//TODO: FIX THIS TEST
		//[TestMethod]
		//public async Task SucesfulRequest()
		//{
		//	var httpClientMock = new Mock<HttpClient>();
		//	var retryHelperMock = new Mock<IRetryHelper>();

		//	var times = 1;
		//	var timeSpan = TimeSpan.FromSeconds(3);

		//	string result = "Sad";
		//	Func<Task> operation = () => Task.CompletedTask;

		//	retryHelperMock.Setup(r => r.RetryOnExceptionAsync(times, timeSpan, operation))
		//		.Returns(Task.FromResult(result));
		//	httpClientMock.Setup(c => c.GetStringAsync("Connection"))
		//		.Returns(Task.FromResult(result));

		//	var apiRequester = new APIRequester(httpClientMock.Object, retryHelperMock.Object);
		//	var apiResult = await apiRequester.Request("connection", 3);

		//	Assert.IsTrue(result.Equals(apiRequester));
		//}
	}
}