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
            this.Id = transaction.Id;
            this.UserId = transaction.User.Id;
			this.User = transaction.User.Email;
			this.CreatedOn = transaction.CreatedOn;
			this.NormalisedAmount = transaction.NormalisedAmount;
			this.Description = transaction.Description;
            this.OriginalAmount = transaction.OriginalAmount;
            this.TransactionType = transaction.TransactionType.Name;
		}

        public string Id { get; set; }

		public string User { get; set; }

        public string UserId { get; set; }

		public DateTime? CreatedOn { get; set; }

		[Range(0, double.MaxValue)]
		public double NormalisedAmount { get; set; }

        public double OriginalAmount { get; set; }

		[Range(10, 100)]
		public string Description { get; set; }

		public string TransactionType { get; set; }
	}
}