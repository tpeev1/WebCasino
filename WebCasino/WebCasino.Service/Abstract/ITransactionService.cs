using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCasino.Entities;
using WebCasino.Service.DTO.Canvas;

namespace WebCasino.Service.Abstract
{
	public interface ITransactionService
    {
		Task<IEnumerable<Transaction>> GetAllTransactionsInfo();

		Task<IEnumerable<Transaction>> GetUserTransactions(string userId);

		Task<IEnumerable<Transaction>> GetTransactionByType(string transactionTypeName);
 		
		Task<Transaction> AddWinTransaction(string userId, double originalAmount, string description);

		Task<Transaction> AddStakeTransaction(string userId, double originalAmount,  string description);

		Task<Transaction> AddWithdrawTransaction(string userId, double originalAmount,  string description);

		Task<Transaction> AddDepositTransaction(string userId, double originalAmount, string description);

	}
}
