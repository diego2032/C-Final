using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActualFinalProject.Models;

namespace ActualFinalProject.Controllers
{
    public class StoreController : Controller
    {
        private OnlineShopDBEntities db = new OnlineShopDBEntities();
        
        // GET: Store
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.ItemCategory);
            var categories = db.ItemCategories;
            StoreModel storeItems = new StoreModel();
            storeItems.Items = items.ToList();
            storeItems.Categories = categories.ToList();
            return View(storeItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(StoreModel storeItem)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 1)
            {
                if (ModelState.IsValid)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.ItemID = storeItem.Item.ID;
                    orderDetail.Quantity = storeItem.Quantity;
                    
                    int id = Int32.Parse(Request.Cookies["userInfo"]["userID"]);
                    var order = db.Orders.FirstOrDefault(o => o.UserID == id && o.OrderTypeID == 2);
                    if (order != null)
                    {
                        orderDetail.OrderID = order.ID;
                    }
                    else
                    {
                        Order newOrder = new Order();
                        newOrder.UserID = id;
                        newOrder.OrderTypeID = 2;
                        newOrder.OrderDate = DateTime.Now;
                        db.Orders.Add(newOrder);
                        db.SaveChanges();
                        var order2 = db.Orders.FirstOrDefault(o => o.UserID == id && o.OrderTypeID == 2);
                        orderDetail.OrderID = order2.ID;
                    }
                    
                    db.OrderDetails.Add(orderDetail);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View("Index");
            }
            return RedirectToAction("Index", "Login");
        }
        // GET: Store/SortBy/1
        public ActionResult SortBy(int? id)
        {
            var items = db.Items.Include(i => i.ItemCategory).Where( i => i.CategoryID == id).ToList();
            if (items.Count == 0)
            {
                return RedirectToAction("Index");
            }
            var categories = db.ItemCategories;
            StoreModel storeItems = new StoreModel();
            storeItems.Items = items;
            storeItems.Categories = categories.ToList();
            return View("Index", storeItems);
        }
    }
}