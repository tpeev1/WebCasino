using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebCasino.Service.Utility.APICurrencyConvertor;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestManager;
using WebCasino.ServiceTests.Utility.APICurrencyConvertorTests.Fakes;

namespace WebCasino.ServiceTests.Utility.APICurrencyConvertorTests.CurrencyConvertorTest
{
	[TestClass]
	public class CallApiWithCurrencyBase_Should
	{
		[TestMethod]
		public async Task ThrowArgumentNullException_WhenUserCurrencyIsNull()
		{
			var httpClientMock = new Mock<HttpClient>();


			var apiRequester = new APIRequester(httpClientMock.Object);

			var currencyConvertor = new CurrencyConvertor(apiRequester);

			string userCurrenccy = null;
			

			await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => currencyConvertor.CallApiWithCurrencyBase(userCurrenccy));
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_WhenUserCurrencyLengthIsSmallerThenThreeSigns()
		{
			var httpClientMock = new Mock<HttpClient>();


			var apiRequester = new APIRequester(httpClientMock.Object);

			var currencyConvertor = new CurrencyConvertor(apiRequester);

			string userCurrenccy = "UA";
			
			await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => currencyConvertor.CallApiWithCurrencyBase(userCurrenccy));
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_WhenUserCurrencyLengthIsLongerThenThreeSigns()
		{
			var httpClientMock = new Mock<HttpClient>();


			var apiRequester = new APIRequester(httpClientMock.Object);

			var currencyConvertor = new CurrencyConvertor(apiRequester);

			string userCurrenccy = "BGNL";

			await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => currencyConvertor.CallApiWithCurrencyBase(userCurrenccy));
		}

		//[TestMethod]
		//public async Task TEST()
		//{
		//	var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
		//	var testUri = "http://sample/smpl";
		//	var testJson = "ok";
			
		//	mockHttpMessageHandler
		//		.Protected()
		//		.Setup<Task<HttpResponseMessage>>("SendAsync", 
		//			ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri == testUri), 
		//			ItExpr.IsAny<CancellationToken>())
		//		.Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
		//		{ Content = new StringContent(testJson) }));

		//	using (var client = new HttpClient(mockHttpMessageHandler.Object))
		//	{
		//		var requester = new APIRequester(client);
		//		var result = requester.Request(testUri);

		//		var response = await client.GetStringAsync(testUri);

		//		Assert.AreEqual(testJson, response);
		//	}
		//}

	}
}
