using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderDelivery.Models
{
    public class Order
    {
        public long Id { get; set; }
        public int Number { get; set; }
        public OrderStatus Status { get; set; }
        public List<string> Products { get; set; }
        public decimal Cost { get; set; }
        public Postamat Postamat { get; set; }
        public string Phone { get; set; }
        public string Recipient { get; set; }
    }

    public class OrderUpdateDTO
    {
        public long Id { get; set; }
        public int Number { get; set; }
        public List<string> Products { get; set; }
        public decimal Cost { get; set; }
        public string Phone { get; set; }
        public string Recipient { get; set; }

    }
}
