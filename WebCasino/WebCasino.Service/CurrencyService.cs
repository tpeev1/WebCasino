using System;
using System.Threading.Tasks;
using WebCasino.Service.Utility.APICurrencyConvertor;

namespace WebCasino.Service
{
	//TODO: NOTE -> CHOFEXX -> FOR NOW THIS IS UNDER DEVELOPMENT AND TESTING
	// in current state is working
	public class CurrencyService
	{
		private readonly CurrencyConvertor convertor;

		public CurrencyService(CurrencyConvertor convertor)
		{
			this.convertor = convertor ?? throw new ArgumentNullException(nameof(convertor));
		}

		public async Task ConvertUserCurrencyFromBaseCurrency(string userCurrency, decimal amount)
		{
			//validate
			var amountInBase = await this.convertor.ConvertFromBaseToUser(userCurrency, amount);

			//Console.WriteLine($"FROM DATABASE USD CURRENCY TO {amountInBase} {userCurrency}");
			//Save in db ??
			//validate
		}

		public async Task ConvertFromUserCurrencyToBase(string currencyBase, decimal amount)
		{
			//validate
			var amountInBase = await this.convertor.ConvertFromUserToBase(currencyBase, amount);

			//Console.WriteLine($"Convert from USER {currencyBase} TO USD = {amountInBase}");
			//Save in db??
			//validate
		}
	}
}