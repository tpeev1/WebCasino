using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Service;

namespace WebCasino.ServiceTests.CardServiceTests
{
	[TestClass]
	public class AddCard_Should
	{
		[TestMethod]
		public async Task ThrowArgumentNullException_WhenCardNumberIsNull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenTransactionTypeNameIsNull")
				.Options;

			string cardNumber = null;
			string userId = "userId";
			DateTime expiration = new DateTime();

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new CardService(context);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.AddCard(cardNumber, userId, expiration)
				);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentNullException_WhenUserIdIsNull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenTransactionTypeNameIsNull")
				.Options;

			string cardNumber = new string('-',16);
			string userId = null;

			DateTime expiration = new DateTime();

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new CardService(context);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.AddCard(cardNumber, userId, expiration)
				);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentException_WhenDateIsInvalid()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenTransactionTypeNameIsNull")
				.Options;

			string cardNumber = new string('-', 16);
			string userId = "userId";

			DateTime expiration = new DateTime(2017,11,10);

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new CardService(context);

				await Assert.ThrowsExceptionAsync<ArgumentException>(
					() => transactionService.AddCard(cardNumber, userId, expiration)
				);
			}
		}

		[TestMethod]
		public async Task AddCardSucessfull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "AddCardSucessfull")
			.Options;

			string cardNumber = new string('-', 16);
			string userId = "userId";

			DateTime expiration = new DateTime(2019, 11, 10);

			using (var context = new CasinoContext(contextOptions))
			{
				var cardService = new CardService(context);
				
				await cardService.AddCard(cardNumber, userId,expiration);

				var card = await context.BankCards
					.Where(c => c.CardNumber == cardNumber )
					.FirstAsync();
					

				Assert.AreEqual(cardNumber, card.CardNumber);
			}
		}

	}
}
