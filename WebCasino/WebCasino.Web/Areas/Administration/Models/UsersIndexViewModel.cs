using System.Collections.Generic;
using System.Linq;
using WebCasino.Entities;

namespace WebCasino.Web.Areas.Administration.Models
{
	public class UsersIndexViewModel
	{
		public readonly IEnumerable<UserViewModel> users;

		public UsersIndexViewModel(IEnumerable<User> users)
		{
			this.users = users.Select(u => new UserViewModel()
			{
				Alias = u.Alias,
				Wallet = u.Wallet,
				Transactions = u.Transactions
			});
		}
	}
}