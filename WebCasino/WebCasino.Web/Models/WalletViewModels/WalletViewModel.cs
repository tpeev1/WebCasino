using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Entities;

namespace WebCasino.Web.Models.WalletViewModels
{
    public class WalletViewModel
    {
        public WalletViewModel(Wallet wallet)
        {
            CurrencyId = wallet.CurrencyId;
            Wins = wallet.Wins;
            Cards = wallet.User.Cards.Select(card => new CardViewModel(card));
            CardSelect = this.Cards.Select(card => new SelectListItem() { Text = card.MaskedNumber, Value = card.CardId });
        }

        public int CurrencyId { get; set; }

        public double Wins { get; set; }

        public IEnumerable<CardViewModel> Cards { get; set; }

        public IEnumerable<SelectListItem> CardSelect { get; set; }
    }
}
