using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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

        //TEST
        public async Task<IActionResult> History(TransactionHistoryViewModel model)
        {
           
                model.Transactions = await this.service.GetAllTransactionsTable();
              
          

            return View(model);
        }

		public async Task<IActionResult> Details(string id)
		{
			var userTransaction = await this.service.RetrieveUserTransaction(id);

            var model = new TransactionDetailsViewModel(userTransaction);

			return View(model);
		}

      
	}
}