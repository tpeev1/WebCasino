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
	public class GetTransactionByType_Should
	{
		[TestMethod]
		public async Task ThrowArgumentNullException_WhenTransactionTypeNameIsNull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenTransactionTypeNameIsNull")
				.Options;

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.GetTransactionByType(null)
				);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_WhenTransactionTypeNameLengthIsLessThenThree()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentOutOfRangeException_WhenTransactionTypeNameLengthIsLessThenThree")
				.Options;

			var transactionType = "12";

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context);

				await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(
					() => transactionService.GetTransactionByType(transactionType)
				);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_WhenTransactionTypeNameBiggerThenTen()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentOutOfRangeException_WhenTransactionTypeNameBiggerThenTen")
				.Options;

			var transactionType = new string('-', 21);

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context);

				await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(
					() => transactionService.GetTransactionByType(transactionType)
				);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentNullException_WhenTransactionTypeTypeCountAreEqualZero()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_WhenTransactionTypeTypeCountAreEqualZero")
			.Options;

			var transactionType = "NoSuchTransaction";

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.GetTransactionByType(transactionType)
				);
			}
		}


		[TestMethod]
		public async Task ReturnTransactionByType()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "ReturnTransactionByType")
			.Options;

			string userId = "id";
			double originalAmount = 1;
			var newBankCard = new BankCard()
			{
				Id = "id1",
			};

			string description = "1234567890";
			var transactionTypeTest = new TransactionType() { Id = 1, Name = "Win" };
			var newTransaction = new Transaction()
			{
				UserId = userId,
				OriginalAmount = originalAmount,
				Description = description,
				TransactionType = transactionTypeTest,
				Card = newBankCard
			};

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context);
				await context.Transactions.AddAsync(newTransaction);
				context.BankCards.Add(newBankCard);
				context.SaveChanges();

				var transActionTypeCount = await transactionService
					.GetTransactionByType("Win")
					.ToAsyncEnumerable()
					.Count();

				Assert.AreEqual(1, transActionTypeCount);
			}
		}
	}
}