using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebCasino.Entities;

namespace WebCasino.Web.Areas.Administration.Models
{
	public class UserViewModel
	{
        public UserViewModel()
        {

        }

		//public UserViewModel(IEnumerable<Transaction> transaction)
		//{
  //          this.Transactions = transaction;
		
		
		//	this.CreatedOn = model.CreatedOn;
		//	this.Wallet = model.Wallet;
		//}

        public string Id { get; set; }
  //      public string Alias { get; set; }

	
		//public DateTime DateOfBirth { get; set; }

		//public bool Locked { get; set; }

		//public bool IsDeleted { get; set; }

	
		//public DateTime? DeletedOn { get; set; }

		
		//public DateTime? CreatedOn { get; set; }

		
		//public DateTime? ModifiedOn { get; set; }

		//public Wallet Wallet { get; set; }	
        public User User { get; set; }

		public IEnumerable<Transaction> Transactions { get; set; }

        public int TotalPages { get; set; }

        public int Page { get; set; } = 1;

        public int PreviousPage => this.Page ==
            1 ? 1 : this.Page - 1;

        public int NextPage => this.Page ==
            this.TotalPages ? this.TotalPages : this.Page + 1;

        public string SearchText { get; set; } = string.Empty;
    }
}