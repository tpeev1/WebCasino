using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCasino.DataContext;
using WebCasino.Service;
using WebCasino.Service.Exceptions;

namespace WebCasino.ServiceTests.TransactionServiceTest
{
	[TestClass]
	public class Constructor_Should
	{
		[TestMethod]
		public void ThrowEntityNotFoundException_WhenNullParameterIsPassed()
		{
			Assert.ThrowsException<EntityNotFoundException>(() => new TransactionService(null));
		}

		[TestMethod]
		public void CreateInstance_WhenCorrectParametersArePassed()
		{
			var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
				.UseInMemoryDatabase(databaseName: "CreateInstance_WhenCorrectParametersArePassed")
				.Options;

			using (var context = new CasinoContext(contextOptions))
			{
				var service = new TransactionService(context);

				Assert.IsInstanceOfType(service, typeof(TransactionService));
			}
		}
	}
}