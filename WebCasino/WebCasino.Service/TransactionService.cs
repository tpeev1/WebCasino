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
            string cardId,
			double amountInUserCurrency,
			string description)
		{
			ServiceValidator.IsInputStringEmptyOrNull(userId);
            ServiceValidator.IsInputStringEmptyOrNull(cardId);
            ServiceValidator.IsInputStringEmptyOrNull(description);
			ServiceValidator.CheckStringLength(description, 10, 100);
			ServiceValidator.ValueIsBetween(amountInUserCurrency, 0, double.MaxValue);

			var userWin = await this.dbContext.Users
				.Include(w => w.Wallet)
                    .ThenInclude(wall => wall.Currency)
				.FirstOrDefaultAsync(u => u.Id == userId && u.IsDeleted != true);

            var bankCard = await this.dbContext.BankCards.FirstOrDefaultAsync(bc => bc.Id == cardId);

			ServiceValidator.ObjectIsNotEqualNull(userWin);
            ServiceValidator.ObjectIsNotEqualNull(bankCard);

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
				NormalisedAmount = normalisedCurrency,
                CardId = bankCard.Id
			};

            userWin.Wallet.NormalisedBalance += normalisedCurrency;
            userWin.Wallet.DisplayBalance = userWin.Wallet.NormalisedBalance * bankRates[userCurrency];
            bankCard.MoneyAdded += amountInUserCurrency;

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
				TransactionTypeId = 2,
				NormalisedAmount = normalisedCurrency
			};

			userWin.Wallet.NormalisedBalance -= normalisedCurrency;
            userWin.Wallet.DisplayBalance = userWin.Wallet.NormalisedBalance * bankRates[userCurrency];

            if(userWin.Wallet.NormalisedBalance < 0)
            {
                throw new InsufficientFundsException("Insufficient funds for the requested operation");
            }
            else
            {
                await this.dbContext.Transactions.AddAsync(newTransaction);
                await this.dbContext.SaveChangesAsync();

                return newTransaction;
            }


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
				TransactionTypeId = 1,
				NormalisedAmount = normalisedCurrency
			};

			userWin.Wallet.NormalisedBalance += normalisedCurrency;
            userWin.Wallet.DisplayBalance = userWin.Wallet.NormalisedBalance * bankRates[userCurrency];

			await this.dbContext.Transactions.AddAsync(newTransaction);
			await this.dbContext.SaveChangesAsync();

			return newTransaction;
		}

		public async Task<Transaction> AddWithdrawTransaction(
			string userId,
            string cardId,
			double amountInUserCurrency,
			string description)
		{
			ServiceValidator.IsInputStringEmptyOrNull(userId);
            ServiceValidator.IsInputStringEmptyOrNull(cardId);
            ServiceValidator.IsInputStringEmptyOrNull(description);
			ServiceValidator.CheckStringLength(description, 10, 100);
			ServiceValidator.ValueIsBetween(amountInUserCurrency, 0, double.MaxValue);

			var userWin = await this.dbContext.Users
				.Include(w => w.Wallet)
                    .ThenInclude(w => w.Currency)
				.FirstOrDefaultAsync(u => u.Id == userId && u.IsDeleted != true);

            var card = await this.dbContext.BankCards.FirstOrDefaultAsync(bc => bc.Id == cardId);

			ServiceValidator.ObjectIsNotEqualNull(userWin);
            ServiceValidator.ObjectIsNotEqualNull(card);

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
				NormalisedAmount = normalisedCurrency,
                CardId = card.Id
			};
            
			userWin.Wallet.NormalisedBalance -= normalisedCurrency;
            userWin.Wallet.DisplayBalance = userWin.Wallet.NormalisedBalance*bankRates[userCurrency];
            card.MoneyRetrieved += amountInUserCurrency;

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

		public async Task<IEnumerable<Transaction>> GetAllTransactionsTable()
		{
			var transactionsQuery = await dbContext
				.Transactions.Where(tr => !tr.IsDeleted)				
				.Include(tt => tt.TransactionType)
				.Include(u => u.User.Wallet.Currency)               
                 
                 .OrderByDescending(d => d.CreatedOn)
                .ToListAsync();

			return transactionsQuery;
		}

		public async Task<IEnumerable<Transaction>> ListByContainingText(string searchText, int page = 1, int pageSize = 10)
		{
			return await this.dbContext.Transactions.Where(m => m.IsDeleted == false)               
                .Include(tt => tt.TransactionType)    
                .Include(u => u.User)
                .Where(m => m.User.Alias.Contains(searchText, StringComparison.InvariantCultureIgnoreCase) ||
                m.Description.Contains(searchText, StringComparison.InvariantCultureIgnoreCase) ||
                m.User.Email.Contains(searchText, StringComparison.InvariantCultureIgnoreCase) ||
                m.TransactionType.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))                    
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderByDescending(d => d.CreatedOn)
                .ToListAsync();
		}

        public async Task<int> TotalContainingText(string searchText)
        {
            var total = await this.dbContext.Transactions
                .Where(m => m.User.Alias.Contains(searchText, StringComparison.InvariantCultureIgnoreCase) ||
               m.Description.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
                .ToListAsync();

            return total.Count;

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

        public async Task<Transaction> RetrieveUserTransaction(string id)
        {
            var user = await this.dbContext.Transactions
                .Include(u => u.User)
                .Include(tt => tt.TransactionType)
                .Include(uc => uc.User.Wallet.Currency)
                .FirstOrDefaultAsync(t => t.Id == id);
                              
            ServiceValidator.ObjectIsNotEqualNull(user);

            return user;
        }

        /// <summary>
        /// Use for Table in User Transactions Details
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Transaction>> RetrieveUserTransaction(string id, int page = 1, int pageSize = 10)
        {
            var transactionsQuery = await this.dbContext
                 .Transactions
                 .Where(t => t.UserId == id && t.IsDeleted != true)
                .Include(tt => tt.TransactionType)
                .Include(u => u.User)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderByDescending(d => d.CreatedOn)
                .ToListAsync();

            return transactionsQuery;
        }

      //TODO: ADD SEARCH FILTERS      
        public async Task<IEnumerable<Transaction>> RetrieveUserSearchTransaction(string searchText, string id, int page = 1, int pageSize = 10)
        {
            var transactionsQueryer = await this.dbContext.Transactions
                .Where(t => t.UserId == id && t.IsDeleted != true)
                 .Include(tt => tt.TransactionType)
                .Include(u => u.User)
                .Where(m => m.User.Alias.Contains(searchText, StringComparison.InvariantCultureIgnoreCase) ||
                m.Description.Contains(searchText, StringComparison.InvariantCultureIgnoreCase) ||
                m.User.Email.Contains(searchText, StringComparison.InvariantCultureIgnoreCase) ||
                m.TransactionType.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderByDescending(d => d.CreatedOn)
                .ToListAsync();

            return transactionsQueryer;
        }

      
    }
}