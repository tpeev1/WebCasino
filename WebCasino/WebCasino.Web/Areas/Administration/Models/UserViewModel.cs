﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebCasino.Entities;

namespace WebCasino.Web.Areas.Administration.Models
{
	public class UserViewModel
	{
		public UserViewModel(User model)
		{
			this.Alias = model.Alias;
			this.Transactions = model.Transactions;
			this.CreatedOn = model.CreatedOn;
			this.Wallet = model.Wallet;
		}

		[Required]
		[MinLength(3)]
		[MaxLength(10)]
		public string Alias { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime DateOfBirth { get; set; }

		public bool Locked { get; set; }

		public bool IsDeleted { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? DeletedOn { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? CreatedOn { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? ModifiedOn { get; set; }

		public Wallet Wallet { get; set; }

		public IEnumerable<BankCard> Cards { get; set; }

		public IEnumerable<LoginLog> Logs { get; set; }

		public IEnumerable<Transaction> Transactions { get; set; }
	}
}