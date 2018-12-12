using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCasino.Entities;
using WebCasino.Service.DTO.Canvas;

namespace WebCasino.Service.Abstract
{
	public interface IAdminDashboard
	{
		Task<CyrrencyDaylyWinDTO> GetTransactionsCurrencyDaylyWins(int day);

		Task<MonthsTransactionsModelDTO> GetMonthsTransactions(DateTime timePeriod, string transactionType, int monthCount);

		MonthsTransactionsModelDTO FiltarByMonth(DateTime timePeriod, int monthCount, IList<Transaction> dbQuery);

		Task<MonthsTransactionsModelDTO> GetYearTransactions(DateTime timePeriod);

		Task<int> GetTotaTransactionsByTypeCount(string transactionType);
	}
}