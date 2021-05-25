using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderDelivery.Models
{
    public class Postamat
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public bool IsOnline { get; set; }
    }
}
