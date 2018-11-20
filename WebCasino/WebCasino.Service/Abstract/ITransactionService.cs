using System.Collections.Generic;
using WebCasino.Entities;

namespace WebCasino.Service.Abstract
{
	public interface ITransactionService
    {
		 IEnumerable<Transaction> GetAllTransactions();

		IEnumerable<Transaction> GetUserTransactions(string userId);

		IEnumerable<Transaction> GetTransactionByType(TransactionType transactionType);

		Transaction AddTransaction(string userId, double originalAmount, TransactionType transactionType,string description);
    }
}
