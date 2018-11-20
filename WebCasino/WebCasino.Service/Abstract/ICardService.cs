using System;
using System.Collections.Generic;
using System.Text;
using WebCasino.Entities;

namespace WebCasino.Service.Abstract
{
    public interface ICardService
    {
		BankCard AddCard(string cardNumber, string userId, DateTime Expiration);

		IEnumerable<BankCard> GetAllCards(int userId);

		BankCard GetCard(string cardNumber);

		double Withdraw(string cardNumber);

		BankCard RemoveCard(string cardNumber);
	}
}
