using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service.Abstract;
using WebCasino.Service.Exceptions;
using WebCasino.Service.Utility.Validator;
using WebCasino.Web.Utilities.TableFilterUtilities;

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
            userWin.Wallet.DisplayBalance = userWin.Wallet.NormalisedBalance * bankRates[userCurrency];

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

            if (userWin.Wallet.NormalisedBalance < 0)
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
            userWin.Wallet.DisplayBalance = userWin.Wallet.NormalisedBalance * bankRates[userCurrency];

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

        public IQueryable<Transaction> GetAllTransactionsTable()
        {
            return dbContext
                .Transactions.Where(tr => !tr.IsDeleted)
                .Include(tt => tt.TransactionType)
                .Include(u => u.User.Wallet.Currency);

        }

        public async Task<IEnumerable<Transaction>> GetFiltered(DataTableModel model)
        {
            int filteredResultsCount;
            int totalResultsCount;

            var searchBy = model.search?.value ?? string.Empty;

            var take = model.length;
            var skip = model.start;

            if (take < 0)
            {
                take = 10;
            }
            if (skip < 0)
            {
                skip = 0;
            }

            var dic = new Dictionary<string, Expression<Func<Transaction, object>>>()
            {
                { "createdOn", tr => tr.CreatedOn },
                {"alias", tr => tr.User.Alias },
                {"transactionTypeName", tr => tr.TransactionType.Name },
                {"normalisedAmount", tr => tr.NormalisedAmount }
            };

            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }

            Expression<Func<Transaction, object>> exp;

            if (!dic.ContainsKey(sortBy))
            {
                exp = dic["createdOn"];
            }
            else
            {
                exp = dic[sortBy];
            }

            var collection = new List<Transaction>();
            if (sortDir)
            {
                 collection = await this.GetAllTransactionsTable()
                    .Where(tr => tr.User.Alias.Contains(searchBy) || tr.TransactionType.Name.Contains(searchBy))
                    .OrderBy(exp)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
            }

            else
            {
                 collection = await this.GetAllTransactionsTable()
                    .Where(tr => tr.User.Alias.Contains(searchBy) || tr.TransactionType.Name.Contains(searchBy) || tr.CreatedOn.ToString().Contains(searchBy))
                    .OrderByDescending(exp)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();
            }

            return collection;

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
        public async Task<IEnumerable<Transaction>> RetrieveAllUsersTransaction(string id)
        {
            var transactionsQuery = await this.dbContext
                 .Transactions
                 .Where(t => t.UserId == id && t.IsDeleted != true)
                .Include(tt => tt.TransactionType)
                .Include(u => u.User)
                .OrderByDescending(d => d.CreatedOn)
                .ToListAsync();

            return transactionsQuery;
        }

    }
}