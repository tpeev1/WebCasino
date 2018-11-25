using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebCasino.Service.Utility.APICurrencyConvertor.Configurations;

namespace WebCasino.Service.Utility.APICurrencyConvertor.RequestManager
{
	public class APIRequester
	{
		private readonly HttpClient client;

		public APIRequester(HttpClient client)
		{
			this.client = client;
		}

		public async Task<string> Request(string connections)
		{
			var pauseBetweenFailuer = TimeSpan.FromSeconds(2);
			var resultBuilder = new StringBuilder();

			await RetryHelper.RetryOnExceptionAsync<HttpRequestException>(RequestConfig.MaxRetryAttempts, pauseBetweenFailuer,
				async () =>
				{
					var response = await client.GetStringAsync(
						connections);

					resultBuilder.Append(response);
				});

			if (resultBuilder.Length == 0)
			{
				throw new ArgumentException("NOTHING IN BUILDER");
			}

			return resultBuilder.ToString();
		}
	}
}