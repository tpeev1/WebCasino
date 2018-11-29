using System;
using System.Collections.Generic;
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

        public WalletController(IWalletService walletService, IUserWrapper userWrapper)
        {
            this.walletService = walletService;
            this.userWrapper = userWrapper;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.userWrapper.GetUserId(HttpContext.User);
            var wallet = await this.walletService.RetrieveWallet(userId);

            var model = new WalletViewModel(wallet);

            return View(model);
        }
    }
}