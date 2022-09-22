using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderSystem.DbData;
using OrderSystem.Models;
using OrderSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSystem.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orderList = _context.Orders.ToList();
            return View(orderList);
        }

        public IActionResult Create()
        {
            var orderList = _context.Orders.ToList();
            return View(orderList);
        }

        [AllowAnonymous]
        public IActionResult CreateForm()
        {
            var model = new Order();
            ViewBag.ProductDw = new SelectList(_context.Products, "ProductId", "ProductName");
            return PartialView("_CreateForm", model);
        }

        [AllowAnonymous]
        public async Task<JsonResult> CreateOrder(OrderViewModel model)
        {

            var product = _context.Products.Where(p => p.ProductId == model.ProductId).FirstOrDefault();
            var order = new Order();
            order.FirstName = model.FirstName;
            order.LastName = model.LastName;
            order.Product = model.Product;
            order.State = model.State;
            order.Date = model.Date;
            var orderProduct = new OrderProduct();
            orderProduct.Order = order;
            orderProduct.Product = product;
            
            _context.Add(orderProduct);
            await _context.SaveChangesAsync();

            var result = "Success!";

            return Json(new { result = result });
        }

       
        public IActionResult EditForm(int orderId)
        {
            var order = _context.Orders.Where(x=>x.OrderId==orderId).FirstOrDefault();
            var orderProduct = _context.OrderProducts.Where(op=>op.OrderId==orderId).FirstOrDefault();
            var product = _context.Products.Where(p=>p.ProductId==orderProduct.ProductId).FirstOrDefault();
            ViewBag.ProductList= new SelectList(_context.Products, "ProductId", "ProductName",product.ProductId.ToString());
            return PartialView("_EditForm", order);
        }


        public async Task<JsonResult> EditOrderComplete(OrderViewModel model)
        {
            var product = _context.Products.Where(p => p.ProductId == model.ProductId).FirstOrDefault();
            var orderProductRemove = _context.OrderProducts.Where(op => op.OrderId == model.OrderId).FirstOrDefault();
            var order = _context.Orders.Where(o=>o.OrderId==model.OrderId).FirstOrDefault();

            order.FirstName = model.FirstName;
            order.LastName = model.LastName;
            order.Product = model.Product;
            order.State = model.State;
            order.Date = model.Date;

            var Orpr = new List<OrderProduct>();
            Orpr.Add(new OrderProduct { OrderId = order.OrderId, ProductId = product.ProductId });
            order.OrderProduct = Orpr;

            _context.Update(order);
            await _context.SaveChangesAsync();
           
            var result = "Success!";

            return Json(new { result = result });
        }

        public IActionResult AddedList()
        {
            var orderList = _context.Orders.ToList();
            
            return PartialView("_AddedList", orderList);
        }

        public async Task<JsonResult> Delete(int Id)
        {
            var order = _context.Orders.Where(o=>o.OrderId==Id).FirstOrDefault();
            
            _context.Remove(order);
            await _context.SaveChangesAsync();
            var result = "Success";

            return Json(new { result = result });
        }


    }
}
