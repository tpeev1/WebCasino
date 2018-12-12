using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Service;

namespace WebCasino.ServiceTests.AdminDashboardServiceTests
{
    [TestClass]
    public class GetYearTransaction_Should
    {
        [TestMethod]
        public async Task ReturnOneYearMontsValueByMonthDTO()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ReturnOneYearMontsValueByMonthDTO")
                .Options;

            using (var context = new CasinoContext(contextOptions))
            {
                var adminService = new AdminDashboardService(context);

                var result = await adminService.GetYearTransactions();

                Assert.AreEqual(12, result.ValuesByMonth.Count);

            }
        }

    }
}
