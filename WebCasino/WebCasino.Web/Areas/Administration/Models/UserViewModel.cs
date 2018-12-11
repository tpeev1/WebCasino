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

        public string Id { get; set; }
  
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