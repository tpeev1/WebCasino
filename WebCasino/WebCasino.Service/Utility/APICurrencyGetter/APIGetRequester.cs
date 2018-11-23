using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebCasino.Service.Utility.APICurrencyGetter.Configurations;
using WebCasino.Service.Utility.APICurrencyGetter.Models;

namespace WebCasino.Service.Utility.APICurrencyGetter
{
	public class APIGetRequester
	{
		//TODO: NOTE: Note here that we have just one instance of HttpClient shared 
		//for the entire application
		private readonly HttpClient client;

		public APIGetRequester(HttpClient client)
		{
			this.client = client ?? throw new ArgumentNullException(nameof(client));
		}

		//TODO: NOTE THIS COMMENTS FOR NEW MODEL?
		//public DateTime LastUse { get; set; }
		//public string Base { get; set; }

		 //TODO: CHOFEXX -> Think about different query params and the way to act on each,
		 //bottom implementation is basic idea
		public async Task GetCurrency(string queryParameters = RequestConfig.QueryParameters.LATEST_CURRENCY)
		{
			//Validate queryParameters
			await this.Process(queryParameters);
		}

		private async Task<CurrencyBindModel> Process(string queryParameters)
		{
			var streamTask = await client.GetStringAsync(RequestConfig.REQUEST_URI + queryParameters);
			//validate getSting
			var apiRequestResult = JsonConvert.DeserializeObject<CurrencyBindModel>(streamTask);
			//validate apiRequestResult
			if (apiRequestResult.Error != null)
			{
				//TODO: IDEA: If this api throws exception call Process again with 2-nd API, 
				//if this doesn't work again call database saved logs
				throw new ArgumentNullException(apiRequestResult.Error);
			}
			return apiRequestResult;
		}
	}
}
