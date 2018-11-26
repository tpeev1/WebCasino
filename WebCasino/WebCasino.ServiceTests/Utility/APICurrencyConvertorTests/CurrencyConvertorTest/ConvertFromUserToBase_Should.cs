using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebCasino.Service.Utility.APICurrencyConvertor;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestManager;

namespace WebCasino.ServiceTests.Utility.APICurrencyConvertorTests.CurrencyConvertorTest
{
	[TestClass]
	public class ConvertFromUserToBase_Should
	{
		[TestMethod]
		public async Task ThrowArgumentNullException_WhenUserCurrencyIsNull()
		{
			var httpClientMock = new Mock<HttpClient>();
			var retryHelperMock = new Mock<RetryHelper>();

			var apiRequester = new APIRequester(httpClientMock.Object, retryHelperMock.Object);

			var currencyConvertor = new CurrencyConvertor(apiRequester);

			string userCurrenccy = null;
			double amount = 199;
			int pauseBetweenFailure = 3;

			await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => currencyConvertor.ConvertFromUserToBase(userCurrenccy, amount, pauseBetweenFailure));
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_WhenUserCurrencyLengthIsSmallerThenThreeSigns()
		{
			var httpClientMock = new Mock<HttpClient>();
			var retryHelperMock = new Mock<RetryHelper>();

			var apiRequester = new APIRequester(httpClientMock.Object, retryHelperMock.Object);

			var currencyConvertor = new CurrencyConvertor(apiRequester);

			string userCurrenccy = "UA";
			double amount = 199;
			int pauseBetweenFailure = 3;

			await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => currencyConvertor.ConvertFromUserToBase(userCurrenccy, amount, pauseBetweenFailure));
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_WhenUserCurrencyLengthIsLongerThenThreeSigns()
		{
			var httpClientMock = new Mock<HttpClient>();
			var retryHelperMock = new Mock<RetryHelper>();

			var apiRequester = new APIRequester(httpClientMock.Object, retryHelperMock.Object);

			var currencyConvertor = new CurrencyConvertor(apiRequester);

			string userCurrenccy = "BGNL";
			double amount = 199;
			int pauseBetweenFailure = 3;

			await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => currencyConvertor.ConvertFromUserToBase(userCurrenccy, amount, pauseBetweenFailure));
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_WhenUserAmountIsSmallerThenZero()
		{
			var httpClientMock = new Mock<HttpClient>();
			var retryHelperMock = new Mock<RetryHelper>();

			var apiRequester = new APIRequester(httpClientMock.Object, retryHelperMock.Object);

			var currencyConvertor = new CurrencyConvertor(apiRequester);

			string userCurrenccy = "BGN";
			double amount = -1;
			int pauseBetweenFailure = 3;

			await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => currencyConvertor.ConvertFromUserToBase(userCurrenccy, amount, pauseBetweenFailure));
		}

		[TestMethod]
		public async Task ThrowArgumentNullException_WhenPauseBetweenBreaksIsEqualToZero()
		{
			var httpClientMock = new Mock<HttpClient>();
			var retryHelperMock = new Mock<RetryHelper>();

			var apiRequester = new APIRequester(httpClientMock.Object, retryHelperMock.Object);

			var currencyConvertor = new CurrencyConvertor(apiRequester);

			string userCurrenccy = "BGN";
			double amount = 1;
			int pauseBetweenFailure = 0;

			await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => currencyConvertor.ConvertFromUserToBase(userCurrenccy, amount, pauseBetweenFailure));
		}
	}
}