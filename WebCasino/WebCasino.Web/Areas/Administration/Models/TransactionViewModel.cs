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
			this.Alias = transaction.User.Alias;
			this.CreatedOn = transaction.CreatedOn;
			this.NormalisedAmount = transaction.NormalisedAmount;
			this.Description = transaction.Description;
            this.TransactionTypeName = transaction.TransactionType.Name;
            this.Email = transaction.User.Email;
            this.UserId = transaction.User.Id;
            this.TransactionId = transaction.Id;
		}

		public string Alias { get; set; }

        public string UserId { get; set; }

        public string TransactionId { get; set; }

        public string Email { get; set; }
		public DateTime? CreatedOn { get; set; }

		[Range(0, double.MaxValue)]
		public double NormalisedAmount { get; set; }

		[Range(10, 100)]
		public string Description { get; set; }

		public string TransactionTypeName { get; set; }
	}
}