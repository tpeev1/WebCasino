using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Service.Abstract;
using WebCasino.Web.Areas.Administration.Models;
using WebCasino.Web.Utilities.TableFilterUtilities;

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
        public IActionResult History()
        {
            return View();
        }

        public async Task<IActionResult> Details(string id)
        {
            var userTransaction = await this.service.RetrieveUserTransaction(id);

            var model = new TransactionDetailsViewModel(userTransaction);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FilterTable(DataTableModel model)
        {
            var result = await this.service.GetFiltered(model);
            var transModel = new TransactionHistoryViewModel() { Transactions = result.Select(r => new TransactionViewModel(r)) };
            var tableModel = new TransactionTableViewModel()
            {
                Transactions = transModel.Transactions,
                Draw = model.draw,
                RecordsTotal = 600,
                RecordsFiltered = 600
            };

            var jsonResult = JsonConvert.SerializeObject(tableModel);

            return Json(tableModel);
        }

    }
}