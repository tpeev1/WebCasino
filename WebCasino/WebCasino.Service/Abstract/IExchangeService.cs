using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebCasino.Service.Abstract
{
    public interface IExchangeService
    {
        Task<double> ConvertFromBase(int currencyId, double amount); 
    }
}
