using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCasino.DataContext;
using WebCasino.Service.Abstract;
using WebCasino.Service.Exceptions;

namespace WebCasino.Service
{
    public class ExchangeService : IExchangeService
    {
        private readonly CasinoContext casinoContext;

        public ExchangeService(CasinoContext casinoContext)
        {
            this.casinoContext = casinoContext;
        }

        public async Task<double> ConvertFromBase(int currencyId, double amount)
        {
            var rate = await this.casinoContext.ExchangeRates.FirstOrDefaultAsync(er => er.CurrencyId == currencyId);

            if(rate == null)
            {
                throw new EntityNotFoundException("Exchange rate not found");
            }

            return rate.ExchangeRate;
        }
    }
}
