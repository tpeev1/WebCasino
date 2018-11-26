using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Threading.Tasks;
using WebCasino.Service.Utility.APICurrencyConvertor.Exceptions;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestManager;

namespace WebCasino.ServiceTests.Utility.APICurrencyConvertorTests.RequestManagerTests.RetryHelperTests
{
	[TestClass]
	public class RetryOnExceptionAsync_Should
	{
		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_WhenTimesIsZero()
		{
			var retryHelper = new RetryHelper();

			var times = 0;
			var timeSpan = TimeSpan.FromSeconds(3);

			await Assert.ThrowsExceptionAsync<ArgumentNullException>(
				() => retryHelper.RetryOnExceptionAsync(times,
						timeSpan,
						() => null
				));
		}

		[TestMethod]
		public async Task ThrowApiServiceNotFoundException_WhenAttemptsAreEqualTimes()
		{
			var retryHelper = new RetryHelper();

			var times = 1;
			var timeSpan = TimeSpan.FromSeconds(3);

			await Assert.ThrowsExceptionAsync<ApiServiceNotFoundException>(
				() => retryHelper.RetryOnExceptionAsync(times,
						timeSpan,
						() => null
				));
		}

		[TestMethod]
		public async Task CallOperationWithSuccess()
		{
			var retryHelper = new RetryHelper();

			var times = 1;
			var timeSpan = TimeSpan.FromSeconds(3);

			var testValue = "Works";
			var result = new StringBuilder();

			await retryHelper.RetryOnExceptionAsync(times, timeSpan,
				async () =>
				{
					result.Append("Works");
				});

			Assert.IsTrue(testValue.Equals(result.ToString()));
		}
	}
}