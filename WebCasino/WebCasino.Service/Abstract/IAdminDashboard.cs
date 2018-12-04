using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCasino.Entities;
using WebCasino.Service.DTO.Canvas;

namespace WebCasino.Service.Abstract
{
	public interface IAdminDashboard
	{
		Task<CyrrencyDaylyWin> GetTransactionsCurrencyDaylyWins(int day);

		Task<MonthsTransactionsModel> GetMonthsTransactions(DateTime timePeriod, string transactionType, int monthCount);

		MonthsTransactionsModel FiltarByMonth(DateTime timePeriod, int monthCount, IList<Transaction> dbQuery);

		Task<MonthsTransactionsModel> GetYearTransactions(DateTime timePeriod);

		Task<int> GetTotaTransactionsByTypeCount(string transactionType);
	}
}