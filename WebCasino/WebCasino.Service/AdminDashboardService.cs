using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service.Abstract;
using WebCasino.Service.DTO.Canvas;

namespace WebCasino.Service
{
	public class AdminDashboardService : IAdminDashboard
	{
		private readonly CasinoContext dbContext;

		public AdminDashboardService(CasinoContext dbContext)
		{
			this.dbContext = dbContext ?? throw new NullReferenceException();
		}

		public async Task<MonthsTransactionsModelDTO> GetMonthsTransactions(DateTime timePeriod,
			string transactionType,
			int monthCount)
		{
            var dbQuery = this.dbContext.Transactions
                .Include(tt => tt.TransactionType)
                .Where(t => t.TransactionType.Name == transactionType);
				

			var resultModel = await FiltarByMonth(timePeriod, monthCount, dbQuery);

			//CHECK FOR MONTH NUMBER !!

			return resultModel;
		}

		public async Task<MonthsTransactionsModelDTO> GetYearTransactions(DateTime timePeriod)
		{
            var dbQuery = this.dbContext.Transactions;

			var resultModel = await FiltarByMonth(timePeriod, 11, dbQuery);

			//CHECK FOR MONTH NUMBER !!

			return resultModel;
		}

		public async Task<MonthsTransactionsModelDTO> FiltarByMonth(DateTime timePeriod, int monthCount, IQueryable<Transaction> dbQuery)
		{
			var resultModel = new MonthsTransactionsModelDTO();

			for (int i = timePeriod.Month - monthCount; i <= timePeriod.Month; i++)
			{
				var monthly = new MonthVallueModelDTO();

				var valueFilter = await dbQuery
				.Where(d => d.CreatedOn.Value.Month == i).CountAsync();

				monthly.MonthValue = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3).ToUpper();
				monthly.Value = valueFilter;

				resultModel.ValuesByMonth.Add(monthly);
			}

			return resultModel;
		}

		public async Task<int> GetTotaTransactionsByTypeCount(string transactionType)
		{
			var totalWins = await this.dbContext
				.Transactions.Include(tt => tt.TransactionType)
				.Where(t => t.TransactionType.Name == transactionType)
				.CountAsync();

			return totalWins;
		}

		public async Task<CyrrencyDaylyWinDTO> GetTransactionsCurrencyDaylyWins(int day)
		{
			var allTransactionsQuery = await this.dbContext
				.Transactions
				.Include(tt => tt.TransactionType)
				.Include(u => u.User.Wallet.Currency)
				.ToListAsync();

			var daylyTotalUsd = allTransactionsQuery
				.Where(tt => tt.TransactionType.Name == "Win")
				.Where(td => td.CreatedOn.Value.Month == DateTime.Now.Month
							&& td.CreatedOn.Value.Day == day)
				.Select(t => t.NormalisedAmount).Sum();

			var daylyWinsBGN = allTransactionsQuery
				.Where(tt => tt.TransactionType.Name == "Win")
				.Where(c => c.User.Wallet.Currency.Name == "BGN")
				.Where(td => td.CreatedOn.Value.Month == DateTime.Now.Month
							&& td.CreatedOn.Value.Day == day)
				.Select(t => t.OriginalAmount).Sum();

			var daylyWinsUSD = allTransactionsQuery
				.Where(tt => tt.TransactionType.Name == "Win")
				.Where(c => c.User.Wallet.Currency.Name == "USD")
				.Where(td => td.CreatedOn.Value.Month == DateTime.Now.Month
							&& td.CreatedOn.Value.Day == DateTime.Now.Day)
				.Select(t => t.OriginalAmount).Sum();

			var daylyWinsGBP = allTransactionsQuery
				.Where(tt => tt.TransactionType.Name == "Win")
				.Where(c => c.User.Wallet.Currency.Name == "GBP")
				.Where(td => td.CreatedOn.Value.Month == DateTime.Now.Month
							&& td.CreatedOn.Value.Day == day)
				.Select(t => t.OriginalAmount).Sum();

			var daylyWinsEUR = allTransactionsQuery
				.Where(tt => tt.TransactionType.Name == "Win")
				.Where(c => c.User.Wallet.Currency.Name == "EUR")
				.Where(td => td.CreatedOn.Value.Month == DateTime.Now.Month
							&& td.CreatedOn.Value.Day == day)
				.Select(t => t.OriginalAmount).Sum();

			var resultModel = new CyrrencyDaylyWinDTO()
			{
				DaylyTotalUSD = daylyTotalUsd,
				DaylyWinsBGN = daylyWinsBGN,
				DaylyWinsEUR = daylyWinsEUR,
				DaylyWinsGBP = daylyWinsGBP,
				DaylyWinsUSD = daylyWinsUSD
			};

			return resultModel;
		}
	}
}