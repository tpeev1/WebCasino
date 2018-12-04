using System.Collections.Generic;
using System.Linq;
using WebCasino.Entities;

namespace WebCasino.Service.DTO.Canvas
{
    public  class MonthsTransactionsModel
    {
        public MonthsTransactionsModel()
        {
            this.ValuesByMonth = new List<MonthVallueModel>();
        }

        public IList<MonthVallueModel> ValuesByMonth { get; set; }
    }
}
