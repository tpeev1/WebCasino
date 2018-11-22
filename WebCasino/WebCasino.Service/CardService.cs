using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service.Abstract;
using WebCasino.Service.Utility.Validator;

namespace WebCasino.Service
{
	public class CardService : ICardService
	{
		private readonly CasinoContext dbContext;

		public CardService(CasinoContext dbContext)
		{
			ServiceValidator.ObjectIsNotEqualNull(dbContext);

			this.dbContext = dbContext;

		}

		public Task<BankCard> AddCard(string cardNumber, string userId, DateTime Expiration)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<BankCard>> GetAllCards(int userId)
		{
			throw new NotImplementedException();
		}

		public Task<BankCard> GetCard(string cardNumber)
		{
			throw new NotImplementedException();
		}

		public Task<BankCard> RemoveCard(string cardNumber)
		{
			throw new NotImplementedException();
		}

		public Task<double> Withdraw(string cardNumber)
		{
			throw new NotImplementedException();
		}
	}
}
