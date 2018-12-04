using System;
using System.ComponentModel.DataAnnotations;

namespace WebCasino.Web.Areas.Administration.Models
{
	public class TransactionViewModel
	{
		public string User { get; set; }

		public DateTime Date { get; set; }

		[Range(0, double.MaxValue)]
		public double NormalisedAmount { get; set; }

		[Range(10, 100)]
		public string Description { get; set; }

		public string TransactionType { get; set; }
	}
}