using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCasino.Entities;

namespace WebCasino.Service.Abstract
{
	//TODO:CHOFEXX- FIX TO ASYNC
	public interface ICardService
    {
		Task<BankCard> AddCard(string cardNumber, string userId, DateTime Expiration);

		Task<IEnumerable<BankCard>> GetAllCards(int userId);

		Task<BankCard> GetCard(string cardNumber);

		Task<double> Withdraw(string cardNumber);

		Task<BankCard> RemoveCard(string cardNumber);
	}
}
