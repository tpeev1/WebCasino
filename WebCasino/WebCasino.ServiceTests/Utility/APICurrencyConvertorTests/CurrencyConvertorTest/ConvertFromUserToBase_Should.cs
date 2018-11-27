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
			

			var apiRequester = new APIRequester(httpClientMock.Object);

			var currencyConvertor = new CurrencyConvertor(apiRequester);

			string userCurrenccy = null;
			double amount = 199;
		
			await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => currencyConvertor.ConvertFromUserToBase(userCurrenccy, amount));
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_WhenUserCurrencyLengthIsSmallerThenThreeSigns()
		{
			var httpClientMock = new Mock<HttpClient>();
			

			var apiRequester = new APIRequester(httpClientMock.Object);

			var currencyConvertor = new CurrencyConvertor(apiRequester);

			string userCurrenccy = "UA";
			double amount = 199;
			
			await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => currencyConvertor.ConvertFromUserToBase(userCurrenccy, amount));
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_WhenUserCurrencyLengthIsLongerThenThreeSigns()
		{
			var httpClientMock = new Mock<HttpClient>();
			

			var apiRequester = new APIRequester(httpClientMock.Object);

			var currencyConvertor = new CurrencyConvertor(apiRequester);

			string userCurrenccy = "BGNL";
			double amount = 199;
			
			await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => currencyConvertor.ConvertFromUserToBase(userCurrenccy, amount));
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_WhenUserAmountIsSmallerThenZero()
		{
			var httpClientMock = new Mock<HttpClient>();
			

			var apiRequester = new APIRequester(httpClientMock.Object);

			var currencyConvertor = new CurrencyConvertor(apiRequester);

			string userCurrenccy = "BGN";
			double amount = -1;			

			await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => currencyConvertor.ConvertFromUserToBase(userCurrenccy, amount));
		}

	}
}