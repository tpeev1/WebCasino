using System;
using System.Threading.Tasks;
using WebCasino.Service.DTO.Canvas;

namespace WebCasino.Service.Abstract
{
	public interface IAdminDashboard
	{
		Task<CyrrencyDaylyWin> GetTransactionsCurrencyDaylyWins(int day);

		Task<MonthsTransactionsModel> GetMonthsTransactions(DateTime timePeriod, string transactionType, int monthCount);

		Task<int> GetTotaTransactionsByTypeCount(string transactionType);
	}
}