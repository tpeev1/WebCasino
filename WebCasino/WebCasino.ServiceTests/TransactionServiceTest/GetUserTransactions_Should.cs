using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;

namespace WebCasino.ServiceTests.TransactionServiceTest
{
	[TestClass]
	public class GetUserTransactions_Should
	{
		[TestMethod]
		public async Task ThrowArgumentNullException_WhenUserIdIsNull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenUserIdIsNull")
				.Options;

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.GetUserTransactions(null)
				);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentNullException_WhenNoSuchUserInDb()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenNoSuchUserInDb")
			.Options;

			var userId = "noId";

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.GetUserTransactions(userId)
				);
			}
		}

		[TestMethod]
		public async Task ReturnUserTransaction()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "ReturnUserTransaction")
			.Options;

			string userId = "id";
			double originalAmount = 1;
			var newBankCard = new BankCard()
			{
				Id = "id1",
			};

			int transactionTypeId = 1;
			string description = "1234567890";

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context);
				context.BankCards.Add(newBankCard);
				context.SaveChanges();

				await transactionService.AddTransaction(userId, originalAmount, newBankCard, transactionTypeId, description);

				var userTransactiont = await transactionService
					.GetUserTransactions(userId).ToAsyncEnumerable().Count();

				Assert.AreEqual(1, userTransactiont);
			}
		}
	}
}