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
            DisplayBalance = wallet.DisplayBalance;
            NormalizedBalance = wallet.NormalisedBalance;
            Cards = wallet.User.Cards.Select(card => new CardViewModel(card));
            
        }

        public int CurrencyId { get; set; }

        public double Wins { get; set; }

        public double DisplayBalance { get; set; }
        public double NormalizedBalance { get; private set; }
        public IEnumerable<CardViewModel> Cards { get; set; }   

    }
}
