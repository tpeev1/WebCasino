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
		private readonly IAdminDashboard adminDashboardService;
		private readonly IUserService userService;

		public HomeController(ITransactionService transactionService, IUserService userService, IAdminDashboard adminDashboardService)
		{
			this.transactionService = transactionService ?? throw new System.ArgumentNullException(nameof(transactionService));
			this.userService = userService ?? throw new System.ArgumentNullException(nameof(userService));
			this.adminDashboardService = adminDashboardService ?? throw new ArgumentNullException(nameof(adminDashboardService));
		}

		public async Task<IActionResult> Index()
		{
			var totalWins = await adminDashboardService.GetTotaTransactionsByTypeCount("Win");
			var totalStakes = await adminDashboardService.GetTotaTransactionsByTypeCount("Stake");

			var allUsers = await this.userService.GetAllUsers();
			var totalUsers = allUsers.Count();

			var allCurrencyDaylyWins = await this.adminDashboardService
				.GetTransactionsCurrencyDaylyWins(DateTime.Now.Day);

			var sixMonthsTotalWins = await this.adminDashboardService.GetMonthsTransactions(DateTime.Now, "Win", 5);
			var sixMonthsTotalStakes = await this.adminDashboardService.GetMonthsTransactions(DateTime.Now, "Stake", 5);

			//TODO: Find a way to incorporate this two in to model
			var oneYearTransactions = await this.transactionService.GetAllTransactionsInfo();
			var oneYearUsersCount = await this.userService.GetAllUsers();

			var oneYearWinsCount = await this.adminDashboardService.GetMonthsTransactions(DateTime.Now, "Win", 11);

			var sixMonthsWins = sixMonthsTotalWins.ValuesByMonth;

			var viewModel = new DashboardViewModel()
			{
				TotalWins = totalWins,
				TotalStakes = totalStakes,
				TotalUsers = totalUsers,
				SixMonthsTotalWins = sixMonthsTotalWins.ValuesByMonth.Where(v => v.Value > 0).Count(),
				SixMonthsTotalStakes = sixMonthsTotalStakes.ValuesByMonth.Where(v => v.Value > 0).Count(),
				SixMonthsWins = new SixMonthsModel(sixMonthsTotalWins.ValuesByMonth),
				SixMonthsStakes = new SixMonthsModel(sixMonthsTotalStakes.ValuesByMonth),

				OneYearWins = new SixMonthsModel(oneYearWinsCount.ValuesByMonth),
				DaylyWins = new DaylyWinsModel()
				{
					DaylyTotalUSD = allCurrencyDaylyWins.DaylyTotalUSD,
					DaylyWinsBGN = allCurrencyDaylyWins.DaylyWinsBGN,
					DaylyWinsEUR = allCurrencyDaylyWins.DaylyWinsEUR,
					DaylyWinsGBP = allCurrencyDaylyWins.DaylyWinsGBP,
					DaylyWinsUSD = allCurrencyDaylyWins.DaylyWinsUSD
				}
			};

			return this.View(viewModel);
		}
	}
}