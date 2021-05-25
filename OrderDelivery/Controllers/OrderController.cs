using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderDelivery.Models;
using OrderDelivery.Services;
using System.Web;

namespace OrderDelivery.Controllers
{

    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        [Route("api/tracking/{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = OrderService.Get(id);
            if (order == null)
                return NotFound();
            return order;
        }

        [HttpPost]
        [Route("api/order/add")]
        public ActionResult<Order> PostOrder(Order _order)
        {
            if (!OrderService.IsProductsValid(_order.Products))
                return StatusCode(400, new { error = "Ошибка запроса: количество товаров не позволяет сформировать заказ..." });
            if (!OrderService.IsPhoneValid(_order.Phone))
                return StatusCode(400, new { error = "Ошибка запроса: формат номера телефона не соответствует маске +7ХХХ-ХХХ-ХХ-ХХ..." });
            if (!OrderService.IsPostamatValid(_order.Postamat))
                return StatusCode(400, new { error = "Ошибка запроса: формат постомата указан неверно..." });
            if (!OrderService.IsPostamatFound(_order.Postamat))
                return StatusCode(400, new { error = "Ошибка запроса: постамат не найден..." });

            OrderService.Add(_order);
            return CreatedAtAction("GetOrder", new { id = _order.Id }, _order);

        }
        
        [HttpPut]
        [Route("api/order/{id}")]
        public ActionResult PutOrder(long id, OrderUpdateDTO _order)
        {
            if (!OrderService.IsProductsValid(_order.Products))
                return StatusCode(400, new { error = "Ошибка запроса: количество товаров не позволяет обновить заказ..." });
            if (!OrderService.IsPhoneValid(_order.Phone))
                return StatusCode(400, new { error = "Ошибка запроса: формат номера телефона не соответствует маске +7ХХХ-ХХХ-ХХ-ХХ..." });
            OrderService.Update(_order);
            return CreatedAtAction("GetOrder", new { id = _order.Id }, _order);
        }
        
        [HttpDelete]
        [Route("api/order/{id}")]
        public ActionResult CancelOrder(int id)
        {
            var _order = OrderService.Get(id);
            OrderService.Cancel(id);
            return CreatedAtAction("GetOrder", new { id = id }, _order);
        }
        
        [HttpGet]
        [Route("api/postomat/{id}")]
        public ActionResult<Postamat> GetPostamat(int id)
        {
            var postomat = OrderService.GetPostamat(id);
            if (postomat == null)
                return NotFound();
            return postomat;
        }
    }


}