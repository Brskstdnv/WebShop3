using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopApp.Core.Contracts
{
    public interface IStaticService
    {

        int CountProducts();
        int CountClients();
        int CountOrders();

        decimal SumOrders();
    }
}
