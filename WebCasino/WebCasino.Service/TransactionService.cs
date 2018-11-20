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

		//TODO: Chofexx - use TransactionType - type or set it up 
		public async Task<Transaction> AddTransaction(string userId, double originalAmount, 
								TransactionType transactionType, string description)
		{
			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new ArgumentNullException();
			}

			if (originalAmount < 0)
			{
				throw new ArgumentOutOfRangeException();
			}

			if (string.IsNullOrWhiteSpace(description))
			{
				throw new ArgumentNullException();
			}

			var newTransaction = new Transaction()
			{
				 UserId = userId,
				 OriginalAmount = originalAmount,
				 Description = description
			};

			await this.dbContext.Transactions.AddAsync(newTransaction);
			await this.dbContext.SaveChangesAsync();

			return newTransaction;
		}

		public async Task<IEnumerable<Transaction>> GetAllTransactions()
		{

			var transactionsQuery = await dbContext.Transactions.ToListAsync();

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

			return transactionsQuery;
		}
	}
}
