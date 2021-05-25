using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using OrderDelivery.Services;
using OrderDelivery.Models;

namespace OrderDelivery.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public void IsPhoneValidTest1()
        {
            var orderService = new OrderService();
            bool result = OrderService.IsPhoneValid("+7900-333-22-11");
            Assert.True(result, "phone number +7900-333-22-11 is valid");
        }
        [Fact]
        public void IsPhoneValidTest2()
        {
            var orderService = new OrderService();
            bool result = OrderService.IsPhoneValid("+7-900-333-22-11");
            Assert.False(result, "phone number +7-900-333-22-11 is not valid");
        }
    }
}
