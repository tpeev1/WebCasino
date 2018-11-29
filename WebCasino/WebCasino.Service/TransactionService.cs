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

		public async Task<Transaction> AddDepositTransaction(
			string userId,
			double originalAmount,
			int transactionTypeId,
			string description)
		{
			ServiceValidator.IsInputStringEmptyOrNull(userId);
			ServiceValidator.IsInputStringEmptyOrNull(description);
			ServiceValidator.CheckStringLength(description, 10, 100);
			ServiceValidator.ValueNotEqualZero(transactionTypeId);
			ServiceValidator.ValueIsBetween(originalAmount, 0, double.MaxValue);

			var newTransaction = new Transaction()
			{
				UserId = userId,
				OriginalAmount = originalAmount,
				Description = description,
				TransactionTypeId = transactionTypeId,
			};


			var userWin = await this.dbContext.Users
				.Include(w => w.Wallet)
				.FirstOrDefaultAsync(u => u.Id == userId && u.IsDeleted != true);

			ServiceValidator.ObjectIsEqualNull(userWin);

			 userWin.Wallet.NormalisedBalance += originalAmount;

			await this.dbContext.Transactions.AddAsync(newTransaction);
			await this.dbContext.SaveChangesAsync();

			return newTransaction;
		}

		public async Task<Transaction> AddStakeTransaction(
			string userId, 
			double originalAmount,
			int transactionTypeId, 
			string description)
		{
			ServiceValidator.IsInputStringEmptyOrNull(userId);
			ServiceValidator.IsInputStringEmptyOrNull(description);
			ServiceValidator.CheckStringLength(description, 10, 100);
			ServiceValidator.ValueNotEqualZero(transactionTypeId);
			ServiceValidator.ValueIsBetween(originalAmount, 0, double.MaxValue);

			var newTransaction = new Transaction()
			{
				UserId = userId,
				OriginalAmount = originalAmount,
				Description = description,
				TransactionTypeId = transactionTypeId,
			};


			var userWin =await this.dbContext.Users
				.Include(w => w.Wallet)
				.FirstOrDefaultAsync(u => u.Id == userId && u.IsDeleted != true);
				
			ServiceValidator.ObjectIsEqualNull(userWin);

			userWin.Wallet.NormalisedBalance -= originalAmount;

			await this.dbContext.Transactions.AddAsync(newTransaction);
			await this.dbContext.SaveChangesAsync();

			return newTransaction;
		}

		public async Task<Transaction> AddWinTransaction(
			string userId,
			double originalAmount, 
			int transactionTypeId,
			string description)
		{
			ServiceValidator.IsInputStringEmptyOrNull(userId);
			ServiceValidator.IsInputStringEmptyOrNull(description);
			ServiceValidator.CheckStringLength(description, 10, 100);
			ServiceValidator.ValueNotEqualZero(transactionTypeId);
			ServiceValidator.ValueIsBetween(originalAmount, 0, double.MaxValue);

			var newTransaction = new Transaction()
			{
				UserId = userId,
				OriginalAmount = originalAmount,
				Description = description,
				TransactionTypeId = transactionTypeId,				
			};

			var userWin = await this.dbContext.Users
				.Include(w => w.Wallet)
				.FirstOrDefaultAsync(u => u.Id == userId && u.IsDeleted != true);

			ServiceValidator.ObjectIsEqualNull(userWin);

			userWin.Wallet.NormalisedBalance += originalAmount;

			await this.dbContext.Transactions.AddAsync(newTransaction);
			await this.dbContext.SaveChangesAsync();

			return newTransaction;
		}

		public async Task<Transaction> AddWithdrawTransaction(
			string userId, 
			double originalAmount, 
			int transactionTypeId,
			string description)
		{
			ServiceValidator.IsInputStringEmptyOrNull(userId);
			ServiceValidator.IsInputStringEmptyOrNull(description);
			ServiceValidator.CheckStringLength(description, 10, 100);
			ServiceValidator.ValueNotEqualZero(transactionTypeId);
			ServiceValidator.ValueIsBetween(originalAmount, 0, double.MaxValue);

			var newTransaction = new Transaction()
			{
				UserId = userId,
				OriginalAmount = originalAmount,
				Description = description,
				TransactionTypeId = transactionTypeId,
			};


			var userWin = await this.dbContext.Users
				.Include(w => w.Wallet)
				.FirstOrDefaultAsync(u => u.Id == userId && u.IsDeleted != true);

			ServiceValidator.ObjectIsEqualNull(userWin);

			userWin.Wallet.NormalisedBalance -= originalAmount;

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
			ServiceValidator.IsInputStringEmptyOrNull(transactionTypeName);
			ServiceValidator.CheckStringLength(transactionTypeName, 3, 20);

			var transactionsQuery = await this.dbContext
				.Transactions
				.Where(t => t.TransactionType.Name == transactionTypeName && t.IsDeleted != true)
				.ToListAsync();
				
			ServiceValidator.ValueNotEqualZero(transactionsQuery.Count());

			return transactionsQuery;
		}

		public async Task<IEnumerable<Transaction>> GetUserTransactions(string userId)
		{
			ServiceValidator.IsInputStringEmptyOrNull(userId);

			var transactionsQuery = await this.dbContext
				.Transactions
				.Where(t => t.UserId == userId && t.IsDeleted != true)
				.ToListAsync();
				
			ServiceValidator.ValueNotEqualZero(transactionsQuery.Count);

			return transactionsQuery;
		}
	}
}