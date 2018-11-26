using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebCasino.Service.Utility.APICurrencyConvertor.Configurations;
using WebCasino.Service.Utility.Validator;

namespace WebCasino.Service.Utility.APICurrencyConvertor.RequestManager
{
	public class APIRequester
	{
		private readonly HttpClient client;
		private readonly RetryHelper retryHelper;

		public APIRequester(HttpClient client, RetryHelper retryHelper)
		{
			this.client = client ?? throw new ArgumentNullException(nameof(client));
			this.retryHelper = retryHelper ?? throw new ArgumentNullException(nameof(retryHelper));
		}

		public async Task<string> Request(string connections)
		{
			ServiceValidator.IsInputStringEmptyOrNull(connections);

			var pauseBetweenFailuer = TimeSpan.FromSeconds(2);
			var resultBuilder = new StringBuilder();

			await retryHelper.RetryOnExceptionAsync<HttpRequestException>(RequestConfig.MaxRetryAttempts, pauseBetweenFailuer,
				async () =>
				{
					var response = await client.GetStringAsync(
						connections);

					resultBuilder.Append(response);
				});

			ServiceValidator.IsInputStringEmptyOrNull(resultBuilder.ToString());

			return resultBuilder.ToString();
		}
	}
}