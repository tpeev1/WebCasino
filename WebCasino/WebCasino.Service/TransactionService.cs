using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service.Abstract;
using WebCasino.Service.Exceptions;
using WebCasino.Service.Utility.Validator;

namespace WebCasino.Service
{
	public class TransactionService : ITransactionService
	{
		private readonly CasinoContext dbContext;
		private readonly ICurrencyRateApiService currencyService;

		public TransactionService(CasinoContext dbContext, ICurrencyRateApiService currencyService)
		{
			ServiceValidator.ObjectIsNotEqualNull(dbContext);

			this.dbContext = dbContext;
			this.currencyService = currencyService;
		}

		public async Task<Transaction> AddDepositTransaction(
			string userId,
			double amountInUserCurrency,
			string description)
		{
			ServiceValidator.IsInputStringEmptyOrNull(userId);
			ServiceValidator.IsInputStringEmptyOrNull(description);
			ServiceValidator.CheckStringLength(description, 10, 100);
			ServiceValidator.ValueIsBetween(amountInUserCurrency, 0, double.MaxValue);

			var userWin = await this.dbContext.Users
				.Include(w => w.Wallet)
                    .ThenInclude(wall => wall.Currency)
				.FirstOrDefaultAsync(u => u.Id == userId && u.IsDeleted != true);

			ServiceValidator.ObjectIsNotEqualNull(userWin);

			var userCurrency = userWin.Wallet.Currency.Name;
			var bankRates = await this.currencyService.GetRatesAsync();

			double normalisedCurrency = 0;
			if (bankRates.ContainsKey(userCurrency))
			{
				double normalisedUserCurrency = bankRates[userCurrency];
				normalisedCurrency = amountInUserCurrency / normalisedUserCurrency;
			}
			else
			{
				throw new EntityCurrencyNotFoundException("Unknown user currency");
			}

			var newTransaction = new Transaction()
			{
				UserId = userId,
				OriginalAmount = amountInUserCurrency,
				Description = description,
				TransactionTypeId = 3,
				NormalisedAmount = normalisedCurrency
			};

            userWin.Wallet.NormalisedBalance += normalisedCurrency;
            userWin.Wallet.DisplayBalance += amountInUserCurrency;

			await this.dbContext.Transactions.AddAsync(newTransaction);
			await this.dbContext.SaveChangesAsync();

			return newTransaction;
		}

		public async Task<Transaction> AddStakeTransaction(
			string userId,
			double amountInUserCurrency,
			string description)
		{
			ServiceValidator.IsInputStringEmptyOrNull(userId);
			ServiceValidator.IsInputStringEmptyOrNull(description);
			ServiceValidator.CheckStringLength(description, 10, 100);
			ServiceValidator.ValueIsBetween(amountInUserCurrency, 0, double.MaxValue);

			var userWin = await this.dbContext.Users
				.Include(w => w.Wallet)
				.FirstOrDefaultAsync(u => u.Id == userId && u.IsDeleted != true);

			ServiceValidator.ObjectIsNotEqualNull(userWin);

			var userCurrency = userWin.Wallet.Currency.Name;
			var bankRates = await this.currencyService.GetRatesAsync();

			double normalisedCurrency = 0;
			if (bankRates.ContainsKey(userCurrency))
			{
				double normalisedUserCurrency = bankRates[userCurrency];
				normalisedCurrency = amountInUserCurrency / normalisedUserCurrency;
			}
			else
			{
				throw new EntityCurrencyNotFoundException("Unknown user currency");
			}

			var newTransaction = new Transaction()
			{
				UserId = userId,
				OriginalAmount = amountInUserCurrency,
				Description = description,
				TransactionTypeId = 2,
				NormalisedAmount = normalisedCurrency
			};

			userWin.Wallet.NormalisedBalance -= normalisedCurrency;
            userWin.Wallet.DisplayBalance -= amountInUserCurrency;

            if(userWin.Wallet.NormalisedBalance < 0)
            {
                throw new InsufficientFundsException("Insufficient funds for the requested operation");
            }           

			await this.dbContext.Transactions.AddAsync(newTransaction);
			await this.dbContext.SaveChangesAsync();

			return newTransaction;
		}

		public async Task<Transaction> AddWinTransaction(
			string userId,
			double amountInUserCurrency,
			string description)
		{
			ServiceValidator.IsInputStringEmptyOrNull(userId);
			ServiceValidator.IsInputStringEmptyOrNull(description);
			ServiceValidator.CheckStringLength(description, 10, 100);
			ServiceValidator.ValueIsBetween(amountInUserCurrency, 0, double.MaxValue);

			var userWin = await this.dbContext.Users
				.Include(w => w.Wallet)
				.FirstOrDefaultAsync(u => u.Id == userId && u.IsDeleted != true);

			ServiceValidator.ObjectIsNotEqualNull(userWin);

			var userCurrency = userWin.Wallet.Currency.Name;
			var bankRates = await this.currencyService.GetRatesAsync();

			double normalisedCurrency = 0;
			if (bankRates.ContainsKey(userCurrency))
			{
				double normalisedUserCurrency = bankRates[userCurrency];
				normalisedCurrency = amountInUserCurrency / normalisedUserCurrency;
			}
			else
			{
				throw new EntityCurrencyNotFoundException("Unknown user currency");
			}

			var newTransaction = new Transaction()
			{
				UserId = userId,
				OriginalAmount = amountInUserCurrency,
				Description = description,
				TransactionTypeId = 1,
				NormalisedAmount = normalisedCurrency
			};

			userWin.Wallet.NormalisedBalance += normalisedCurrency;
            userWin.Wallet.DisplayBalance += amountInUserCurrency;

			await this.dbContext.Transactions.AddAsync(newTransaction);
			await this.dbContext.SaveChangesAsync();

			return newTransaction;
		}

		public async Task<Transaction> AddWithdrawTransaction(
			string userId,
			double amountInUserCurrency,
			string description)
		{
			ServiceValidator.IsInputStringEmptyOrNull(userId);
			ServiceValidator.IsInputStringEmptyOrNull(description);
			ServiceValidator.CheckStringLength(description, 10, 100);
			ServiceValidator.ValueIsBetween(amountInUserCurrency, 0, double.MaxValue);

			var userWin = await this.dbContext.Users
				.Include(w => w.Wallet)
                    .ThenInclude(w => w.Currency)
				.FirstOrDefaultAsync(u => u.Id == userId && u.IsDeleted != true);

			ServiceValidator.ObjectIsNotEqualNull(userWin);

			var userCurrency = userWin.Wallet.Currency.Name;
			var bankRates = await this.currencyService.GetRatesAsync();

			double normalisedCurrency = 0;
			if (bankRates.ContainsKey(userCurrency))
			{
				double normalisedUserCurrency = bankRates[userCurrency];
				normalisedCurrency = amountInUserCurrency / normalisedUserCurrency;
			}
			else
			{
				throw new EntityCurrencyNotFoundException("Unknown user currency");
			}

			var newTransaction = new Transaction()
			{
				UserId = userId,
				OriginalAmount = amountInUserCurrency,
				Description = description,
				TransactionTypeId = 4,
				NormalisedAmount = normalisedCurrency
			};

			userWin.Wallet.NormalisedBalance -= normalisedCurrency;
            userWin.Wallet.DisplayBalance -= amountInUserCurrency;

            if (userWin.Wallet.NormalisedBalance < 0)
            {
                throw new InsufficientFundsException("Insufficient funds for the requested operation");
            }

            else
            {
                if (userWin.Wallet.NormalisedBalance < 0.009)
                {
                    userWin.Wallet.NormalisedBalance = 0;
                }

                await this.dbContext.Transactions.AddAsync(newTransaction);
                await this.dbContext.SaveChangesAsync();

                return newTransaction;
            }

		}

		public async Task<IEnumerable<Transaction>> GetAllTransactionsTable(int page = 1, int pageSize = 10)
		{
			var transactionsQuery = await dbContext
				.Transactions.Where(tr => !tr.IsDeleted)
				.OrderByDescending(x => x.Id)
				.Include(tt => tt.TransactionType)
				.Include(u => u.User.Wallet.Currency)
				 .Skip((page - 1) * pageSize).Take(pageSize)
				.ToListAsync();

			return transactionsQuery;
		}

		public IEnumerable<Transaction> ListByContainingText(string searchText, int page = 1, int pageSize = 10)
		{
			return this.dbContext.Transactions.Where(m => m.IsDeleted == false)
				.Where(m => m.User.Alias.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
		}

		public int TotalContainingText(string searchText)
		{
			return this.dbContext.Transactions.Where(m => m.User.Alias.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)).ToList().Count();
		}

		public async Task<int> Total()
		{
			return await this.dbContext.Transactions.CountAsync();
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