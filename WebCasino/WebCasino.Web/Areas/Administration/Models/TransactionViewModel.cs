using System;
using System.ComponentModel.DataAnnotations;
using WebCasino.Entities;

namespace WebCasino.Web.Areas.Administration.Models
{
	public class TransactionViewModel
	{
		public TransactionViewModel()
		{
		}

		public TransactionViewModel(Transaction transaction)
		{
			this.User = transaction.User.Email;
			this.Date = transaction.CreatedOn;
			this.NormalisedAmount = transaction.NormalisedAmount;
			this.Description = transaction.Description;
		}

		public string User { get; set; }

		public DateTime? Date { get; set; }

		[Range(0, double.MaxValue)]
		public double NormalisedAmount { get; set; }

		[Range(10, 100)]
		public string Description { get; set; }

		public string TransactionType { get; set; }
	}
}