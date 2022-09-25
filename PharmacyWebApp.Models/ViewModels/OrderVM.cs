using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyWebApp.Models.ViewModels
{
	public class OrderVM
	{
        public OrderDetails OrderDetails { get; set; }
        public IEnumerable<Order> Order { get; set; }
    }
}
