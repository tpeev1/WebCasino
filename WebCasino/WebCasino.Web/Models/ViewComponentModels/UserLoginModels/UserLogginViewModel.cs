using System;
using WebCasino.Entities;

namespace WebCasino.Web.Models.ViewComponentModels.UserLoginModels
{
	public class UserLogginViewModel
	{
		public UserLogginViewModel(User user)
		{
			Alias = user.Alias;
			Balance = Math.Floor(user.Wallet.DisplayBalance*100)/100;
			Currency = user.Wallet.Currency.Name;
		}

		public string Alias { get; set; }

		public double Balance { get; set; }

		public string Currency { get; set; }
	}
}