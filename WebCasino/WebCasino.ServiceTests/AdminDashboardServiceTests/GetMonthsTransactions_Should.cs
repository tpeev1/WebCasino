using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Service;

namespace WebCasino.ServiceTests.AdminDashboardServiceTests
{
    [TestClass]
    public class GetMonthsTransactions_Should
    {
        [TestMethod]
        public async Task ThrowExceptionArgumentNullException_WhenParameterTypeIsNull()
        {
            var casinoContextMock = new Mock<CasinoContext>();

            
            string transactionType = null;
            int monthCount = 1;

            var adminDashboardService = new AdminDashboardService(casinoContextMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => adminDashboardService.GetMonthsTransactions(transactionType, monthCount));

        }

        [TestMethod]
        public async Task ThrowExceptionArgumentOutOfRangeException_WhenParameterMonthCountIsLessThenOne()
        {
            var casinoContextMock = new Mock<CasinoContext>();

            string transactionType = "Win";
            int monthCount = 0;

            var adminDashboardService = new AdminDashboardService(casinoContextMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => adminDashboardService.GetMonthsTransactions( transactionType, monthCount));

        }

        [TestMethod]
        public async Task ThrowExceptionArgumentOutOfRangeException_WhenParameterMonthCountIsMoreThen12()
        {
            var casinoContextMock = new Mock<CasinoContext>();

          
            string transactionType = "Win";
            int monthCount = 13;

            var adminDashboardService = new AdminDashboardService(casinoContextMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => adminDashboardService.GetMonthsTransactions( transactionType, monthCount));

        }

        [TestMethod]
       
        public async Task ReturnThreeMontsValueByMonthDTO()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ReturnZeroValueByMonthDTO")
                .Options;

            using (var context = new CasinoContext(contextOptions))
            {
                var adminService = new AdminDashboardService(context);

                var transactionType = "Win";
                var month = 2;

                var result = await adminService.GetMonthsTransactions(transactionType, month);

                Assert.AreEqual(3, result.ValuesByMonth.Count);

            }
        }

    }
}
