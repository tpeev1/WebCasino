using System;
using System.Collections.Generic;
using System.Linq;
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
			this.dbContext = dbContext;
		}

		//TODO: CHOFEXX- Fix Validation
		//TODO: CHOFEXX - MAKE IT ASYNK
		public Transaction AddTransaction(string userId, double originalAmount, 
								TransactionType transactionType, string description)
		{
			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new NotImplementedException();
			}

			if (string.IsNullOrWhiteSpace(description))
			{
				throw new NotImplementedException();
			}

			throw new System.NotImplementedException();
		}

		public IEnumerable<Transaction> GetAllTransactions()
		{
			var transactionsQuery = this.dbContext.Transactions;

			return transactionsQuery;
		}

		public IEnumerable<Transaction> GetTransactionByType(string transactionTypeName)
		{
			if (string.IsNullOrWhiteSpace(transactionTypeName))
			{
				throw new NotImplementedException();
			}

			var transactionsQuery = this.dbContext
				.Transactions
				.Where(t => t.TransactionType.Name == transactionTypeName);
				

			return transactionsQuery;
		}

		public IEnumerable<Transaction> GetUserTransactions(string userId)
		{
			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new NotImplementedException();
			}

			var transactionsQuery = this.dbContext
				.Transactions
				.Where(t => t.UserId == userId);

			return transactionsQuery;
		}
	}
}
