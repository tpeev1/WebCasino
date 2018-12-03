using WebCasino.Service.DTO.Canvas;
using WebCasino.Web.Areas.Administration.Models.ChartModels;

namespace WebCasino.Web.Areas.Administration.Models
{
	public class DashboardViewModel
	{
		public DashboardViewModel()
		{
		}

		public int TotalWins { get; set; }
		public int TotalStakes { get; set; }
		public int TotalUsers { get; set; }

		public MonthsTransactionsModel SixMonthsTotalWins { get; set; }
		public MonthsTransactionsModel SixMonthsTotalStakes { get; set; }

		public DaylyWinsModel DaylyWins { get; set; }
	}
}