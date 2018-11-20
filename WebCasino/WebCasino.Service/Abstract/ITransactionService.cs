using System.Collections.Generic;
using WebCasino.Entities;

namespace WebCasino.Service.Abstract
{
	//TODO:CHOFEXX- FIX TO ASYNC
	public interface ITransactionService
    {
		 IEnumerable<Transaction> GetAllTransactions();

		IEnumerable<Transaction> GetUserTransactions(string userId);

		IEnumerable<Transaction> GetTransactionByType(string transactionTypeName);

		Transaction AddTransaction(string userId, double originalAmount, TransactionType transactionType,string description);
    }
}
