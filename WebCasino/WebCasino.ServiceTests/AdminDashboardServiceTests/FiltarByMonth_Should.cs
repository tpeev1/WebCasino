using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;

namespace WebCasino.ServiceTests.AdminDashboardServiceTests
{
    [TestClass]
   public class FiltarByMonth_Should
    {
        [TestMethod]
        public void ThrowExceptionArgumentOutOfRangeException_WhenParameterMonthCountIsLessThenOne()
        {
            var casinoContextMock = new Mock<CasinoContext>();

            IEnumerable<Transaction>  transactions=  new List<Transaction>();
            DateTime date = DateTime.Parse("12/12/2018");
            int monthCount = 0;

            var adminDashboardService = new AdminDashboardService(casinoContextMock.Object);

            Assert.ThrowsException<ArgumentOutOfRangeException>( () => adminDashboardService.FiltarByMonth(date, monthCount, transactions));

        }

        [TestMethod]
        public void ThrowExceptionArgumentOutOfRangeException_WhenParameterMonthCountIsMoreThen12()
        {
            var casinoContextMock = new Mock<CasinoContext>();


            IEnumerable<Transaction> transactions = new List<Transaction>();
            DateTime date = DateTime.Parse("12/12/2018");
            int monthCount = 13;

            var adminDashboardService = new AdminDashboardService(casinoContextMock.Object);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => adminDashboardService.FiltarByMonth(date, monthCount, transactions));

        }

    }
}
