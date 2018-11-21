using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
			var db = new Mock<CasinoContext>();
			var service = new TransactionService(db.Object);

			Assert.IsInstanceOfType(service, typeof(TransactionService));
		}
	}
}
