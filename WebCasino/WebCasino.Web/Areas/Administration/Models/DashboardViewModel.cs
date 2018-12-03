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

		public int SixMonthsTotalWins { get; set; }
		public SixMonthsModel SixMonthsWins { get; set; }

		public int SixMonthsTotalStakes { get; set; }
		public SixMonthsModel SixMonthsStakes { get; set; }

		//TODO : CREATE NEW MODEL OR RENAME SixMonthsModel
		public SixMonthsModel OneYearWins { get; set; }

		public SixMonthsModel OneYearTransactions { get; set; }

		public DaylyWinsModel DaylyWins { get; set; }
	}
}