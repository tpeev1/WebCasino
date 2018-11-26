using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Threading.Tasks;
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
			Func<Task> operation = () => Task.CompletedTask;

			await Assert.ThrowsExceptionAsync<ArgumentNullException>(
				() => retryHelper.RetryOnExceptionAsync(times,
						timeSpan, operation));
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_WhenDileyIsZeroOreLessThenZero()
		{
			var retryHelper = new RetryHelper();

			var times = 1;
			TimeSpan delay = TimeSpan.FromSeconds(0);
			Func<Task> operation = () => Task.CompletedTask;

			await Assert.ThrowsExceptionAsync<ArgumentNullException>(
				() => retryHelper.RetryOnExceptionAsync(times,
						delay, operation
				));
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_WhenOpperationIsNull()
		{
			var retryHelper = new RetryHelper();

			var times = 1;
			TimeSpan delay = TimeSpan.FromSeconds(0);
			Func<Task> operation = null;

			await Assert.ThrowsExceptionAsync<ArgumentNullException>(
				() => retryHelper.RetryOnExceptionAsync(times,
						delay, operation
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