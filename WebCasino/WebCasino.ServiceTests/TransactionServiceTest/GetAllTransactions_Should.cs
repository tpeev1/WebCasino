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
	public class GetAllTransactions_Should
	{
		[TestMethod]
		public async Task ThrowEntityNotFoundException_WhenNoTransactionIsFound()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "ThrowEntityNotFoundException_WhenNoTransactionIsFound")
			.Options;

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.GetAllTransactions()
				);
			}
		}

		[TestMethod]
		public async Task ReturnTransaction()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "ReturnTransaction")
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
				context.BankCards.Add(newBankCard);

				context.SaveChanges();

				var transactionService = new TransactionService(context);

				await transactionService.AddTransaction(userId, originalAmount, newBankCard, transactionTypeId, description);

				var transactions = await transactionService.GetAllTransactions();

				Assert.AreEqual(1, transactions.Count());
			}
		}
	}
}