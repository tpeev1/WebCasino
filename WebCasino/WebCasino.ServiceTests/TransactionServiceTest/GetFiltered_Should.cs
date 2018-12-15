using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Entities;
using WebCasino.Service;
using WebCasino.Service.Abstract;
using WebCasino.Service.Utility.TableFilterUtilities;

namespace WebCasino.ServiceTests.TransactionServiceTest
{
    [TestClass]
    public class GetFiltered_Should
    {
        [TestMethod]
        public async Task ReturnCorrectResultDTO()
        {
            var contextOptions = new DbContextOptionsBuilder<CasinoContext>()
                .UseInMemoryDatabase(databaseName: "ReturnCorrectResultDTO")
                .Options;

            var currencyServiceMock = new Mock<ICurrencyRateApiService>();
            var dataTableModel = new DataTableModel()
            {
                start = 1,
                order = new List<Order>()
                 {
                     new Order()
                     {
                           column = 0,
                           dir = "asc"
                     }
                 },
                columns = new List<Column>()
                {
                    new Column()
                    {
                         data = "test-data"
                    }
                }
                

            };

            var userId = "user-id";
            var type = "Win";
            var currency = "USD";

            var transactionsData = new Transaction()
            {
                Id = userId,
                UserId = userId,
                User = new User()
                {
                    Id = userId,
                    Wallet = new Wallet()
                    {
                        Currency = new Currency()
                        {
                            Name = currency
                        }
                    },
                },
                TransactionType = new TransactionType()
                {
                    Name = type
                }
            };

            using (var context = new CasinoContext(contextOptions))
            {
                var transactionService = new TransactionService(context, currencyServiceMock.Object);
                context.Transactions.Add(transactionsData);

                await context.SaveChangesAsync();

                var getFiltered = await transactionService.GetFiltered(dataTableModel);

                var transactionsFromDTO = getFiltered.Transactions;

                Assert.IsInstanceOfType(transactionsFromDTO, typeof(IEnumerable<Transaction>));
              

            }
        }
    }
}
