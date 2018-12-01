using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Service.Abstract;
using WebCasino.Web.Areas.Administration.Models;
using WebCasino.Web.Areas.Administration.Models.ChartModels;

namespace WebCasino.Web.Areas.Administration.Controllers
{
	[Area("Administration")]
	[Authorize(Roles = "Administrator")]
	public class HomeController : Controller
	{
		private readonly ITransactionService transactionService;
		private readonly IUserService userService;

		public HomeController(ITransactionService transactionService, IUserService userService)
		{
			this.transactionService = transactionService ?? throw new System.ArgumentNullException(nameof(transactionService));
			this.userService = userService ?? throw new System.ArgumentNullException(nameof(userService));
		}

		public async Task<IActionResult> Index()
		{
			var allTransactionsQuery = await this.transactionService.GetAllTransactionsInfo();
			var allUsers = await this.userService.GetAllUsers();

			var totalWins = allTransactionsQuery.Where(t => t.TransactionType.Name == "Win").Count();
			var totalStakes = allTransactionsQuery.Where(t => t.TransactionType.Name == "Stake").Count();
			var totalUsers = allUsers.Count();

			var sixMonthsWins = allTransactionsQuery
				.Where(t => t.TransactionType.Name == "Win")
				.Where(d => d.CreatedOn.Value.Month >= DateTime.Now.Month - 6
								&& d.CreatedOn.Value.Month <= DateTime.Now.Month).Count();

			var sixMonthsStakes = allTransactionsQuery
				.Where(t => t.TransactionType.Name == "Stake")
				.Where(d => d.CreatedOn.Value.Month >= DateTime.Now.Month - 6
								&& d.CreatedOn.Value.Month <= DateTime.Now.Month).Count();

			var daylyTotalUsd = allTransactionsQuery
				.Where(tt => tt.TransactionType.Name == "Win")
				.Where(td => td.CreatedOn.Value.Month == DateTime.Now.Month
							&& td.CreatedOn.Value.Day == DateTime.Now.Day)
				.Select(t => t.NormalisedAmount).Sum();

			var daylyWinsBGN = allTransactionsQuery
				.Where(tt => tt.TransactionType.Name == "Win")
				.Where(c => c.User.Wallet.Currency.Name == "BGN")
				.Select(t => t.OriginalAmount).Sum();

			var daylyWinsUSD = allTransactionsQuery
				.Where(tt => tt.TransactionType.Name == "Win")
				.Where(c => c.User.Wallet.Currency.Name == "USD")
				.Select(t => t.OriginalAmount).Sum();

			var daylyWinsGBP = allTransactionsQuery
				.Where(tt => tt.TransactionType.Name == "Win")
				.Where(c => c.User.Wallet.Currency.Name == "GBP")
				.Select(t => t.OriginalAmount).Sum();

			var daylyWinsEUR = allTransactionsQuery
				.Where(tt => tt.TransactionType.Name == "Win")
				.Where(c => c.User.Wallet.Currency.Name == "EUR")
				.Select(t => t.OriginalAmount).Sum();

			var viewModel = new DashboardViewModel()
			{
				TotalWins = totalWins,
				TotalStakes = totalStakes,
				TotalUsers = totalUsers,
				SixMonthsTotalWins = sixMonthsWins,
				SixMonthsTotalStakes = sixMonthsStakes,
				DaylyWins = new DaylyWinsModel()
				{
					DaylyTotalUSD = daylyTotalUsd,
					DaylyWinsBGN = daylyWinsBGN,
					DaylyWinsEUR = daylyWinsEUR,
					DaylyWinsGBP = daylyWinsGBP,
					DaylyWinsUSD = daylyWinsUSD
				}
			};

			return this.View(viewModel);
		}
	}
}