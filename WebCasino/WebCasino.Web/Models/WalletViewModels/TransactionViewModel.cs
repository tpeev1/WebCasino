using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Entities;

namespace WebCasino.Web.Models.WalletViewModels
{
    public class TransactionViewModel
    {
        public TransactionViewModel(Transaction transaction)
        {
            Amount = transaction.OriginalAmount;
            TransactionTypeId = transaction.TransactionTypeId;
        }

        public double Amount { get; set; }

        public int TransactionTypeId { get; set; }
    }
}
