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
		
		public APIRequester(HttpClient client)
		{
			this.client = client ?? throw new ArgumentNullException(nameof(client));			
		}

		public async Task<string> Request(string connections)
		{
			ServiceValidator.IsInputStringEmptyOrNull(connections);

			var response = await client.GetStringAsync(connections);

			ServiceValidator.IsInputStringEmptyOrNull(response);

			return response;
		}
	}
}