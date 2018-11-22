using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service.Abstract;
using WebCasino.Service.Utility.Validator;

namespace WebCasino.Service
{
	public class TransactionService : ITransactionService
	{
		private readonly CasinoContext dbContext;

		public TransactionService(CasinoContext dbContext)
		{
			ServiceValidator.ObjectIsNotEqualNull(dbContext);

			this.dbContext = dbContext;
		}

		public async Task<Transaction> AddTransaction(string userId,
								double originalAmount,
								BankCard bankCard,
								int transactionTypeId,
								string description)
		{
			ServiceValidator.IsInputStringEmptyOrNull(userId);
			ServiceValidator.IsInputStringEmptyOrNull(description);
			ServiceValidator.CheckStringLength(description, 10, 100);
			ServiceValidator.ValueNotEqualZero(transactionTypeId);
			ServiceValidator.ObjectIsNotEqualNull(bankCard);
			//TODO: CHOFEXX - WHAT IS MAX VALUE
			ServiceValidator.ValueIsBetween(originalAmount, 0, double.MaxValue);

			var allCards = this.dbContext.BankCards;

			//TODO: CHOFEXX - IF THER ISN'T A BANK CARD IN DB ?
			ServiceValidator.ValueNotEqualZero(allCards.Count());
			var card = allCards.Select(c => c).Where(c => c.Id == bankCard.Id).First();

			ServiceValidator.ObjectIsNotEqualNull(card);

			var newTransaction = new Transaction()
			{
				UserId = userId,
				OriginalAmount = originalAmount,
				Description = description,
				TransactionTypeId = transactionTypeId,
				Card = card
			};

			await this.dbContext.Transactions.AddAsync(newTransaction);
			await this.dbContext.SaveChangesAsync();

			return newTransaction;
		}

		public async Task<IEnumerable<Transaction>> GetAllTransactions()
		{
			var transactionsQuery = await dbContext.Transactions.ToListAsync();
			//TODO: 0 transaction ? this check is useless?
			ServiceValidator.ValueNotEqualZero(transactionsQuery.Count());

			return transactionsQuery;
		}

		public async Task<IEnumerable<Transaction>> GetTransactionByType(string transactionTypeName)
		{
			ServiceValidator.IsInputStringEmptyOrNull(transactionTypeName);
			ServiceValidator.CheckStringLength(transactionTypeName, 3, 20);

			var transactionsQuery = await this.dbContext
				.Transactions
				.Where(t => t.TransactionType.Name == transactionTypeName)
				.ToListAsync();

			ServiceValidator.ObjectIsNotEqualNull(transactionsQuery);

			return transactionsQuery;
		}

		public async Task<IEnumerable<Transaction>> GetUserTransactions(string userId)
		{
			ServiceValidator.IsInputStringEmptyOrNull(userId);

			var transactionsQuery = await this.dbContext
				.Transactions
				.Where(t => t.UserId == userId)
				.ToListAsync();

			ServiceValidator.ObjectIsNotEqualNull(transactionsQuery);

			return transactionsQuery;
		}
	}
}