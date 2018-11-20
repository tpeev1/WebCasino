using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service.Abstract;

namespace WebCasino.Service
{
	
	public class TransactionService : ITransactionService
	{
		private readonly CasinoContext dbContext;

		public TransactionService(CasinoContext dbContext)
		{
			this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public async Task<Transaction> AddTransaction(string userId, double originalAmount, BankCard bankCard,
								int transactionTypeId, string description)
		{
			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new ArgumentNullException();
			}
			
			if (originalAmount < 0)
			{
				throw new ArgumentOutOfRangeException();
			}

			if (transactionTypeId <= 0)
			{
				throw new ArgumentOutOfRangeException();
			}

			if (string.IsNullOrWhiteSpace(description))
			{
				throw new ArgumentNullException();
			}

			var card = this.dbContext.BankCards.Where(c => c.Id == bankCard.Id).First();

			if (card == null)
			{
				throw new ArgumentNullException();
			}

			var newTransaction = new Transaction()
			{
				 UserId = userId,
				 OriginalAmount = originalAmount,
				 Description = description,
				 TransactionTypeId  = transactionTypeId,
				 Card = card	  
			};

			await this.dbContext.Transactions.AddAsync(newTransaction);
			await this.dbContext.SaveChangesAsync();

			return newTransaction;
		}

		public async Task<IEnumerable<Transaction>> GetAllTransactions()
		{
			var transactionsQuery = await dbContext.Transactions.ToListAsync();

			if (transactionsQuery == null)
			{
				throw new ArgumentNullException();
			}

			return transactionsQuery;
		}

		public async Task<IEnumerable<Transaction>> GetTransactionByType(string transactionTypeName)
		{
			if (string.IsNullOrWhiteSpace(transactionTypeName))
			{
				throw new ArgumentNullException();
			}

			var transactionsQuery = await this.dbContext
				.Transactions
				.Where(t => t.TransactionType.Name == transactionTypeName)
				.ToListAsync();

			if (transactionsQuery == null)
			{
				throw new ArgumentNullException();
			}

			return transactionsQuery;
		}

		public async Task<IEnumerable<Transaction>> GetUserTransactions(string userId)
		{
			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new ArgumentNullException();
			}

			var transactionsQuery = await this.dbContext
				.Transactions
				.Where(t => t.UserId == userId)
				.ToListAsync();

			if (transactionsQuery == null)
			{
				throw new ArgumentNullException();
			}

			return transactionsQuery;
		}
	}
}
