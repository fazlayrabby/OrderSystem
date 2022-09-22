using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSystem.Models
{
    public class Order
    {
      
        public int OrderId { get; set; }
        public string State  { get; set; }
        public string Product { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<OrderProduct> OrderProduct { get; set; }

    }
}
