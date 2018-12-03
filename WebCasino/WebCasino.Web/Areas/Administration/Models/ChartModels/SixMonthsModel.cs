using System.Collections.Generic;
using WebCasino.Service.DTO.Canvas;

namespace WebCasino.Web.Areas.Administration.Models.ChartModels
{
	public class SixMonthsModel
	{
		public Dictionary<string, int> monthValue = new Dictionary<string, int>();

		public SixMonthsModel(IList<MonthVallueModel> sixMontsTransactions)
		{
			foreach (var item in sixMontsTransactions)
			{
				monthValue.Add(item.MonthValue, item.Value);
			}
		}
	}
}