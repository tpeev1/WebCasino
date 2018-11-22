using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;
using WebCasino.Service.Exceptions;

namespace WebCasino.ServiceTests.TransactionServiceTest
{
	[TestClass]
	public class AddTransaction_Should
	{
		[TestMethod]
		public async Task ThrowArgumentNullException_When_UserIdIsNull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentNullException_When_UserIdIsNull")
				.Options;

			string userId = null;
			double originalAmount = 5;
			var bankCardMock = new Mock<BankCard>();
			int transactionTypeId = 1;
			string description = "description message";

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.AddTransaction(userId, originalAmount, bankCardMock.Object, transactionTypeId, description)
					);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_When_OriginalAmountIsLessThenNull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowArgumentOutOfRangeException_When_OriginalAmountIsLessThenNull")
				.Options;

			string userId = "id";
			double originalAmount = -1;
			var bankCardMock = new Mock<BankCard>();
			int transactionTypeId = 1;
			string description = "description message";

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context);

				await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(
					() => transactionService.AddTransaction(userId, originalAmount, bankCardMock.Object, transactionTypeId, description)
					);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentNullException_When_TransactionTypeIdIsNull()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "Throw_When_TransactionTypeIdIsNull")
				.Options;

			string userId = "id";
			double originalAmount = 1;
			var bankCardMock = new Mock<BankCard>();
			int transactionTypeId = 0;
			string description = "description message";

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.AddTransaction(userId, originalAmount, bankCardMock.Object, transactionTypeId, description)
					);
			}
		}

		[TestMethod]
		public async Task ThrowEntityNotFoundException_When_BankCardNull_InMethodParameters()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "ThrowEntityNotFoundException_When_BankCardNull_InMethodParameters")
			.Options;

			string userId = "id";
			double originalAmount = 1;
			BankCard bankCard = null;
			int transactionTypeId = 1;
			string description = "description message";

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context);

				await Assert.ThrowsExceptionAsync<EntityNotFoundException>(
					() => transactionService.AddTransaction(userId, originalAmount, bankCard, transactionTypeId, description)
					);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentOutOfRangeException_When_DescriptionIsSmallerThenTen()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "ThrowArgumentOutOfRangeException_When_DescriptionIsSmallerThenTen")
			.Options;

			string userId = "id";
			double originalAmount = 1;
			var bankCardMock = new Mock<BankCard>();
			int transactionTypeId = 1;
			string description = "123456789";

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context);

				await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(
					() => transactionService.AddTransaction(userId, originalAmount, bankCardMock.Object, transactionTypeId, description)
					);
			}
		}

		[TestMethod]
		public async Task Throw_When_DescriptionIsBiggerThen100()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
			.UseInMemoryDatabase(databaseName: "ThrowArgumentOutOfRangeException_When_DescriptionIsSmallerThenTen")
			.Options;

			string userId = "id";
			double originalAmount = 1;
			var bankCardMock = new Mock<BankCard>();
			int transactionTypeId = 1;
			string description = "123456789";

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context);

				await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(
					() => transactionService.AddTransaction(userId, originalAmount, bankCardMock.Object, transactionTypeId, description)
					);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentNullExceptionn_When_DbGotNoCards()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowEntityNotFoundException_When_CardIsNullInDb")
				.Options;

			string userId = "id";
			double originalAmount = 1;
			var bankCardMock = new Mock<BankCard>();
			int transactionTypeId = 1;
			string description = "1234567890";

			using (var context = new CasinoContext(contextOptions))
			{
				var transactionService = new TransactionService(context);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(
					() => transactionService.AddTransaction(userId, originalAmount, bankCardMock.Object, transactionTypeId, description)
					);
			}
		}

		[TestMethod]
		public async Task ThrowEntityNotFoundException_When_CardIsNullInDb()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "ThrowEntityNotFoundException_When_CardIsNullInDb")
				.Options;

			string userId = "id";
			double originalAmount = 1;

			var newBankCard = new BankCard()
			{
				   Id = "id1"
			};

			BankCard nullBankCard = null;

			int transactionTypeId = 1;
			string description = "1234567890";

			using (var context = new CasinoContext(contextOptions))
			{
				context.BankCards.Add(newBankCard);

				var transactionService = new TransactionService(context);

				await Assert.ThrowsExceptionAsync<EntityNotFoundException>(
					() => transactionService.AddTransaction(userId, originalAmount, nullBankCard, transactionTypeId, description)
					);
			}
		}

		//[TestMethod]
		//public async Task CucessfullyCreatedTransaction()
		//{
		//	var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
		//	.UseInMemoryDatabase(databaseName: "ThrowEntityNotFoundException_When_CardIsNullInDb")
		//	.Options;

		//	string userId = "id";
		//	double originalAmount = 1;

		//	var newBankCard = new BankCard()
		//	{
		//		Id = "id1",
				  
		//	};

		//	int transactionTypeId = 1;
		//	string description = "1234567890";

		//	using (var context = new CasinoContext(contextOptions))
		//	{
		//		context.BankCards.Add(newBankCard);

		//		context.SaveChanges();

		//		var transactionService = new TransactionService(context);

		//		await transactionService.AddTransaction(userId, originalAmount, newBankCard, transactionTypeId, description);

		//		 Assert.AreEqual(1, context.Transactions.CountAsync().);				
		//	}
		//}
	}
}
