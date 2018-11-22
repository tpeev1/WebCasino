using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public async Task<BankCard> AddCard(string cardNumber, string userId, DateTime expiration)
		{
			ServiceValidator.IsInputStringEmptyOrNull(cardNumber);
			ServiceValidator.CheckStringLength(cardNumber, 16, 16);
			ServiceValidator.IsInputStringEmptyOrNull(userId);

			//TODO: CHOFEXX (1)- Add custom exception for bank card exp.
			if (expiration.Year <= DateTime.Now.Year &&
			expiration.Day <= DateTime.Now.Day)
			{
				throw new ArgumentException();
			}

			//TODO: CHOFEXX - ADD CREATED ON -> AND TRANSACTION
			var bankCard = new BankCard()
			{
				 CardNumber = cardNumber,
				  UserId = userId,
				   Expiration = expiration,
				   IsDeleted = false,			  
			};

			await this.dbContext.BankCards.AddAsync(bankCard);
			await this.dbContext.SaveChangesAsync();

			return bankCard;
		}

		//TODO: CHOFEXX - FIx validator method ValueNotEqualZero to return <= 0
		public async Task<IEnumerable<BankCard>> GetAllCards(string userId)
		{
			ServiceValidator.IsInputStringEmptyOrNull(userId);

			var bankCardQuery = await this.dbContext.BankCards
				.Where(u => u.UserId == userId)
				.ToListAsync();

			ServiceValidator.ObjectIsNotEqualNull(bankCardQuery);

			return bankCardQuery;
		}

		//TODO: Create ValidateCardNumber in validator with regex
		public async Task<BankCard> GetCard(string cardNumber)
		{
			ServiceValidator.IsInputStringEmptyOrNull(cardNumber);

			var bankCardQuery = await this.dbContext.BankCards
				.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
				
			ServiceValidator.ObjectIsNotEqualNull(bankCardQuery);

			return bankCardQuery;
		}

		//TODO: CHOFEXX - When card is removed - create transaction to note this remove ?
		public async Task<BankCard> RemoveCard(string cardNumber)
		{
			ServiceValidator.IsInputStringEmptyOrNull(cardNumber);

			var bankCardQuery = await this.dbContext.BankCards
				.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);

			ServiceValidator.ObjectIsNotEqualNull(bankCardQuery);

			bankCardQuery.IsDeleted = true;

			await this.dbContext.SaveChangesAsync();

			return bankCardQuery;
		}

		//TODO: CHOFEXX - What currency to Withdraw
		public async Task<double> Withdraw(string cardNumber, double amount)
		{
			ServiceValidator.IsInputStringEmptyOrNull(cardNumber);

			var bankCardQuery = await this.dbContext.BankCards
				.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
				
			ServiceValidator.ObjectIsNotEqualNull(bankCardQuery);

			if (bankCardQuery.Expiration.Year >= DateTime.Now.Year &&
				bankCardQuery.Expiration.Day >= DateTime.Now.Day)
			{
				bankCardQuery.MoneyRetrieved += amount;
				await this.dbContext.SaveChangesAsync();
			}
			else
			{
				//TODO: CHOFEXX (1)- Add custom exception for bank card exp.
				throw new ArgumentNullException();
			}

			return amount;
		}

		
	}
}
