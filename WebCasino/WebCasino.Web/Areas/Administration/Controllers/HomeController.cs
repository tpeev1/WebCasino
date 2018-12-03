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

		public HomeController(ITransactionService transactionService, IUserService userService)
		{
			this.transactionService = transactionService ?? throw new System.ArgumentNullException(nameof(transactionService));
			this.userService = userService ?? throw new System.ArgumentNullException(nameof(userService));
		}

		public async Task<IActionResult> Index()
		{
			var allTransactionsQuery = await this.transactionService.GetAllTransactionsInfo();
			
            var totalWins = await adminDashboardService.GetTotalWinsCount();
            var totalStakes = await adminDashboardService.GetTotalStakesCount();

            var allUsers = await this.userService.GetAllUsers();
            var totalUsers = allUsers.Count();

            var allCurrencyDaylyWins = await this.adminDashboardService
                .GetTransactionsCurrencyDaylyWins(DateTime.Now.Day);

            var sixMonthsWins = await this.adminDashboardService.GetMonthsTransactions(DateTime.Now, "Win",6);
            var sixMonthsStakes = await this.adminDashboardService.GetMonthsTransactions(DateTime.Now, "Stake",6);

            var viewModel = new DashboardViewModel()
			{
				TotalWins = totalWins,
				TotalStakes = totalStakes,
				TotalUsers = totalUsers,
				SixMonthsTotalWins = sixMonthsWins,
				SixMonthsTotalStakes = sixMonthsStakes,
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