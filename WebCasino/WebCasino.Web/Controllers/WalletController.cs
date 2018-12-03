using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCasino.Service.Abstract;
using WebCasino.Web.Models.WalletViewModels;
using WebCasino.Web.Utilities.Wrappers;

namespace WebCasino.Web.Controllers
{
    [Authorize(Roles = "Player")]
    public class WalletController : Controller
    {
        private readonly IWalletService walletService;
        private readonly IUserWrapper userWrapper;
        private readonly ICardService cardService;

        public WalletController(IWalletService walletService, IUserWrapper userWrapper, ICardService cardService)
        {
            this.walletService = walletService;
            this.userWrapper = userWrapper;
            this.cardService = cardService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.userWrapper.GetUserId(HttpContext.User);
            var wallet = await this.walletService.RetrieveWallet(userId);

            var model = new WalletViewModel(wallet);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCard(CardViewModel model)
        {
            //TO DO: DA PITAM ZA VALIDACIITE TUK
            if (this.ModelState.IsValid)
            {
                var userId = this.userWrapper.GetUserId(HttpContext.User);

                var date = DateTime.ParseExact(model.ExpirationDate.Replace(" ", string.Empty), "MM/yyyy", CultureInfo.InvariantCulture);
                await this.cardService.AddCard(model.RealNumber.Replace(" ",string.Empty), userId, date);
                TempData["CardAdded"] = "Succesfulyy added new card";
                return this.RedirectToAction("index", "wallet");
            }

            TempData["CardAddedFail"] = "Failed to add card. Please try again with a different card";
            return this.RedirectToAction("index", "wallet");
        }
    }

}