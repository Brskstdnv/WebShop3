using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace WebShopApp.Models.Statistic
{
    public class StatisticVM
    {
        [Display(Name = "Count Clients")]
        public int CountClients { get; set; }

        [Display(Name = "Count Products")]
        public int CountProduct {  get; set; }

        [Display(Name = "Count Orders")]
        public int CountOrders { get; set; }

        [Display(Name = "Total Sum Orders")]
        public decimal SumOrders {  get; set; }





    }
}
