using System;
using System.Threading.Tasks;
using WebCasino.Service.Utility.APICurrencyConvertor.Exceptions;

namespace WebCasino.Service.Utility.APICurrencyConvertor.RequestManager
{
	public class RetryHelper
	{
		public async Task RetryOnExceptionAsync(
			int times, TimeSpan delay, Func<Task> operation)
		{
			await RetryOnExceptionAsync<Exception>(times, delay, operation);
		}

		public async Task RetryOnExceptionAsync<TException>(
			int times, TimeSpan delay, Func<Task> operation)
			where TException : Exception
		{
			if (times <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(times));
			}

			var attempts = 0;

			do
			{
				try
				{
					attempts++;
					await operation();
					break;
				}
				catch (TException ex)
				{
					if (attempts == times)
					{
						throw new ApiServiceNotFoundException("Service not found - " + ex.Message);
					}
				}
			} while (true);
		}
	}
}