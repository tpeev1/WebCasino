using System;
using System.Threading.Tasks;

namespace WebCasino.Service.Utility.APICurrencyConvertor.RequestManager.Contracts
{
	public interface IRetryHelper
	{
		Task RetryOnExceptionAsync(
			int times, TimeSpan delay, Func<Task> operation);

		Task RetryOnExceptionAsync<TException>(
			int times, TimeSpan delay, Func<Task> operation) where TException : Exception;
	}
}