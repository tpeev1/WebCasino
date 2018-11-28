using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Entities;

namespace WebCasino.Web.Models.WalletViewModels
{
    public class CardViewModel
    {
        public CardViewModel(BankCard card)
        {
            CardId = card.Id;
            MaskedNumber = (card.CardNumber.Substring(12)).PadLeft(12,'*');
            MoneyAdded = card.MoneyAdded;
            MoneyRetrieved = card.MoneyRetrieved;
            Transactions = card.Transcations.Select(tr => new TransactionViewModel(tr));
        }

        public string CardId { get; set; }
        public string MaskedNumber { get; set; }

        public double MoneyAdded { get; set; }

        public double MoneyRetrieved { get; set; }

        public IEnumerable<TransactionViewModel> Transactions {get;set;}
    }
}
