using System;
using System.Collections.Generic;
using System.Linq;

namespace WebCasino.Web.Areas.Administration.Models
{
	public class TransactionHistoryViewModel
	{
		public readonly IEnumerable<TransactionViewModel> transactionHistory;

		public TransactionHistoryViewModel(IEnumerable<Entities.Transaction> transactionHistory)
		{
			this.transactionHistory = transactionHistory.Select(trh => new TransactionViewModel()
			{
				Date = (DateTime)trh.CreatedOn,
				Description = trh.Description,
				NormalisedAmount = trh.NormalisedAmount,
				TransactionType = trh.TransactionType.Name,
				User = trh.User.Email
			});
		}
	}
}