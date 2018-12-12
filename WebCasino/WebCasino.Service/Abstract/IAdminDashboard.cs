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

		Task<MonthsTransactionsModelDTO> GetMonthsTransactions( string transactionType, int monthCount);


		Task<MonthsTransactionsModelDTO> GetYearTransactions();

		Task<int> GetTotaTransactionsByTypeCount(string transactionType);
	}
}