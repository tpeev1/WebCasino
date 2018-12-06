using System.Collections.Generic;
using System.Threading.Tasks;
using WebCasino.Entities;

namespace WebCasino.Service.Abstract
{
	public interface ITransactionService
	{
		Task<IEnumerable<Transaction>> GetAllTransactionsTable(int page = 1, int pageSize = 10);

        Task<IEnumerable<Transaction>> ListByContainingText(string searchText, int page = 1, int pageSize = 10);

		Task<int> TotalContainingText(string searchText);

		Task<int> Total();

		Task<IEnumerable<Transaction>> GetUserTransactions(string userId);

		Task<IEnumerable<Transaction>> GetTransactionByType(string transactionTypeName);

		Task<Transaction> AddWinTransaction(string userId, double originalAmount, string description);

		Task<Transaction> AddStakeTransaction(string userId, double originalAmount, string description);

		Task<Transaction> AddWithdrawTransaction(string userId, double originalAmount, string description);

		Task<Transaction> AddDepositTransaction(string userId, double originalAmount, string description);
	}
}