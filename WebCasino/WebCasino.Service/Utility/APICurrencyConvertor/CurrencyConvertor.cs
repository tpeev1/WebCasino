using System;
using System.Threading.Tasks;
using WebCasino.Service.Utility.APICurrencyConvertor.Configurations;
using WebCasino.Service.Utility.APICurrencyConvertor.Exceptions;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestConverter;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestConverter.Models;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestManager;
using WebCasino.Service.Utility.Validator;

namespace WebCasino.Service.Utility.APICurrencyConvertor
{
	public class CurrencyConvertor
	{
		private readonly APIRequester requester;

		public CurrencyConvertor(APIRequester requester)
		{
			this.requester = requester ?? throw new ArgumentNullException(nameof(requester));
		}

		/// <summary>
		/// Make call to api with base USD to transform from database to user currency
		/// </summary>
		/// <param name="userCurrency">User currency to find</param>
		/// <param name="amount">amount of money</param>
		/// <returns></returns>
		public async Task<double> ConvertFromBaseToUser(string userCurrency, double amount)
		{
			ServiceValidator.IsInputStringEmptyOrNull(userCurrency);
			ServiceValidator.CheckStringLength(userCurrency, 3, 3);
			ServiceValidator.ValueIsBetween(amount, 0, double.MaxValue);
		

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
		public async Task<double> ConvertFromUserToBase(string userCurrencyBase, double amount)
		{
			ServiceValidator.IsInputStringEmptyOrNull(userCurrencyBase);
			ServiceValidator.CheckStringLength(userCurrencyBase, 3, 3);
			ServiceValidator.ValueIsBetween(amount, 0, double.MaxValue);
		

			CurrencyRequestBindModel currencyModel = await CallApiWithCurrencyBase( userCurrencyBase);

			if (currencyModel.Rates.ContainsKey(RequestConfig.BaseCurrency))
			{
				return amount * currencyModel.Rates[RequestConfig.BaseCurrency];
			}

			throw new ArgumentException();
		}

		public async Task<CurrencyRequestBindModel> CallApiWithCurrencyBase(string currencyBase = RequestConfig.BaseCurrency)
		{
			ServiceValidator.IsInputStringEmptyOrNull(currencyBase);
			ServiceValidator.CheckStringLength(currencyBase, 3, 3);


			var queryString = RequestConfig.API_URI + RequestConfig.QueryParameters.API_QUERY_LATEST_BASE + currencyBase;

			string callResult;

			
			callResult = await this.requester.Request(queryString);
			

			var jsonCreator = new JsonModelCreator();

			CurrencyRequestBindModel result = jsonCreator.JsonToModelDeserializer(callResult);

			if (result.Error == null)
			{
				return result;
			}

			throw new ArgumentNullException(result.Error);
		}
	}
}