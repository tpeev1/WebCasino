using System.Collections.Generic;
using System.Threading.Tasks;
using WebCasino.Entities;

namespace WebCasino.Service.Abstract
{
	public interface ITransactionService
    {
		Task<IEnumerable<Transaction>> GetAllTransactions();

		Task<IEnumerable<Transaction>> GetUserTransactions(string userId);

		Task<IEnumerable<Transaction>> GetTransactionByType(string transactionTypeName);

		Task<Transaction> AddTransaction(string userId, double originalAmount, TransactionType transactionType,string description);
    }
}
