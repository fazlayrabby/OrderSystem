using OrderSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSystem.ViewModel
{
    public class OrderViewModel 
    {
        public int OrderId { get; set; }
        public string State { get; set; }
        public string Product { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
    }
}
