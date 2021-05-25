using OrderDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrderDelivery.Services
{
    public class OrderService
    {
        static List<Order> Orders { get; }
        static List<OrderStatus> Statuses { get; }
        static List<Postamat> Postamats { get; }

        static int nextId = 4;
        static OrderService()
        {
            Statuses = new List<OrderStatus>
            {
                new OrderStatus {
                    Id=1, 
                    Name="Зарегистрирован"
                },
                new OrderStatus {
                    Id=2, 
                    Name="Принят на складе"
                },
                new OrderStatus {
                    Id=3,
                    Name="Выдан курьеру"
                },
                new OrderStatus {
                    Id=4,
                    Name="Доставлен в постомат"
                },
                new OrderStatus {
                    Id=5,
                    Name="Доставлен получателю"
                },
                new OrderStatus {
                    Id=6,
                    Name="Отменен"
                }
            };
            Postamats = new List<Postamat>
            {
                new Postamat {
                    Id=1, 
                    Number="1", 
                    Address="Кутузовский проспект", 
                    IsOnline=true
                },
                new Postamat {
                    Id=2, 
                    Number="2", 
                    Address="Ленинский проспект", 
                    IsOnline=true
                }
            };
            Orders = new List<Order>
            {
                new Order {
                    Id=1, 
                    Number=1, 
                    Status=Statuses.FirstOrDefault(x=>x.Id==1), 
                    Products= new List<string> { "book", "game" }, 
                    Cost=1, 
                    Postamat=Postamats.FirstOrDefault(x=>x.Id==1), 
                    Phone="+7(900)3332201", 
                    Recipient="fio1"
                },
                new Order {
                    Id=2,
                    Number=2,
                    Status=Statuses.FirstOrDefault(x=>x.Id==1),
                    Products= new List<string> { "bike"},
                    Cost=5,
                    Postamat=Postamats.FirstOrDefault(x=>x.Id==1),
                    Phone="+7(900)3332202",
                    Recipient="fio2"
                },
                new Order {
                    Id=3,
                    Number=3,
                    Status=Statuses.FirstOrDefault(x=>x.Id==2),
                    Products= new List<string> { "board", "mouse", "speakers" },
                    Cost=3,
                    Postamat=Postamats.FirstOrDefault(x=>x.Id==2),
                    Phone="+7(900)3332203",
                    Recipient="fio3"
                }
            };
            
        }
        public static Order Get(int Id) => Orders.FirstOrDefault(x => x.Id == Id);
        public static void Add(Order order)
        {
            order.Id = nextId++;
            Orders.Add(order);
        }
        public static void Update(OrderUpdateDTO  _order)
        {
            var index = Orders.FindIndex(x => x.Id == _order.Id); // .FirstOrDefault(x => x.Id == _order.Id);
            if (index >= 0)
            {
                Orders[index].Number = _order.Number;
                Orders[index].Cost = _order.Cost;
                Orders[index].Products = _order.Products;
                Orders[index].Phone = _order.Phone;
                Orders[index].Recipient = _order.Recipient;
            }
        }
        public static bool Cancel(int Id)
        {
            bool result = false;
            var cancelStatus = Statuses.FirstOrDefault(x => x.Id == 6);
            var index = Orders.FindIndex(x => x.Id == Id);
            if (index >= 0)
            {
                Orders[index].Status = cancelStatus;
                result = true;
            }
            return result;
        }

        public static Postamat GetPostamat(int Id) => Postamats.FirstOrDefault(x => x.Id == Id);

        public static bool IsProductsValid(List<string> products)
        {
            bool result = true;
            if (products != null && products.Count > 10)
                result = false;
            return result;
        }
        public static bool IsPhoneValid(string _phone)
        {
            bool result = true;
            Regex regex = new Regex(@"^\+7\d{3}-\d{3}-\d{2}-\d{2}$", RegexOptions.IgnoreCase);
            if (_phone!=null && !regex.IsMatch(_phone))
                result = false;
            return result;
        }
        public static bool IsPostamatValid(Postamat _postamat)
        {
            bool result = true;
            if (_postamat != null && !Int32.TryParse(_postamat.Number, out int i))
                result = false;
            return result;
        }
        public static bool IsPostamatFound(Postamat _postamat)
        {
            bool result = true;
            if (_postamat != null && Postamats.FindIndex(x=>x.Number.ToLower().Trim() == _postamat.Number.ToLower().Trim() ) < 0 )
                result = false;
            return result;
        }
        
    }
}
