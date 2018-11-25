using System;
using System.Threading.Tasks;
using WebCasino.Service.Utility.APICurrencyConvertor.Configurations;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestConverter;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestConverter.Models;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestManager;

namespace WebCasino.Service.Utility.APICurrencyConvertor
{
	public class CurrencyConvertor
	{
		private readonly APIRequester requester;

		public CurrencyConvertor(APIRequester requester)
		{
			this.requester = requester;
		}

		/// <summary>
		/// Make call to api with base USD to transform from database to user currency
		/// </summary>
		/// <param name="userCurrency">User currency to find</param>
		/// <param name="amount">amount of money</param>
		/// <returns></returns>
		public async Task<decimal> ConvertFromBaseToUser(string userCurrency, decimal amount)
		{
			CurrencyRequestBindModel currencyModel = await CallApiWithCurrencyBase();

			if (currencyModel.Rates.ContainsKey(userCurrency))
			{
				return amount * currencyModel.Rates[userCurrency];
			}

			throw new ArgumentException();
		}

		/// <summary>
		/// Make call to api with user base, find user currency and * by rate
		/// </summary>
		/// <param name="currencyBase">user currency to transform in USD</param>
		/// <param name="amount">amount of user money</param>
		/// <returns></returns>
		public async Task<decimal> ConvertFromUserToBase(string userCurrencyBase, decimal amount)
		{
			CurrencyRequestBindModel currencyModel = await CallApiWithCurrencyBase(userCurrencyBase);

			if (currencyModel.Rates.ContainsKey(RequestConfig.BaseCurrency))
			{
				return amount * currencyModel.Rates[RequestConfig.BaseCurrency];
			}

			throw new ArgumentException();
		}

		private async Task<CurrencyRequestBindModel> CallApiWithCurrencyBase(string currencyBase = RequestConfig.BaseCurrency)
		{
			var queryString = RequestConfig.API_URI + RequestConfig.QueryParameters.API_QUERY_LATEST_BASE + currencyBase;

			var callResult = await this.requester.Request(queryString);
			var jsonCreator = new JsonModelCreator();

			CurrencyRequestBindModel result = jsonCreator.JsonToModelDeserializer(callResult);

			if (result.Error == null)
			{
				return result;
			}

			throw new ArgumentNullException();
		}
	}
}