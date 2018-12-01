using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebCasino.Service.Abstract;
using WebCasino.Web.Areas.Administration.Models;

namespace WebCasino.Web.Areas.Administration.Controllers
{
	[Area("Administration")]
	public class TransactionsController : Controller
	{
		private readonly ITransactionService service;

		public TransactionsController(ITransactionService service)
		{
			this.service = service ?? throw new System.ArgumentNullException(nameof(service));
		}

		public async Task<IActionResult> History()
		{
			var allTransactionsQuery = await this.service.GetAllTransactionsInfo();

			var viewModel = new TransactionHistoryViewModel(allTransactionsQuery);

			return View(viewModel);
		}
	}
}