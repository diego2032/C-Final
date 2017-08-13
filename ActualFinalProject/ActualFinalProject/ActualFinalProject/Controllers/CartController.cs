using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ActualFinalProject.CalculateTotalsReference;
using ActualFinalProject.Models;

namespace ActualFinalProject.Controllers
{
    public class CartController : Controller
    {
        private OnlineShopDBEntities db = new OnlineShopDBEntities();
        // GET: Cart
        public ActionResult Index()
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 1)
            {
                int id = Int32.Parse(Request.Cookies["userInfo"]["userID"]);
                var order = db.Orders.FirstOrDefault(o => o.UserID == id && o.OrderTypeID == 2);
                if (order != null)
                {
                    List<OrderDetail> items = db.OrderDetails.Where(i => i.OrderID == order.ID).Include(i => i.Item).ToList();
                    CartModel cart = new CartModel();
                    cart.OrderID = order.ID;
                    cart.UserID = order.UserID;
                    cart.OrderDate = order.OrderDate;
                    cart.OrderTypeID = order.OrderTypeID;
                    cart.CartItems = items;
                    return View(cart);
                }
                Order newOrder = new Order();
                newOrder.UserID = id;
                newOrder.OrderTypeID = 2;
                newOrder.OrderDate = DateTime.Now;
                db.Orders.Add(newOrder);
                db.SaveChanges();

                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        // GET: Cart/Edit/5
        public ActionResult Edit(int? id, int? id2)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 1)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                OrderDetail orderDetail = db.OrderDetails.Find(id, id2);
                if (orderDetail == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ItemID = new SelectList(db.Items, "ID", "ItemName", orderDetail.ItemID);
                ViewBag.OrderID = new SelectList(db.Orders, "ID", "ID", orderDetail.OrderID);
                return View(orderDetail);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,ItemID,Quantity")] OrderDetail orderDetail)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 1)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(orderDetail).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ItemID = new SelectList(db.Items, "ID", "ItemName", orderDetail.ItemID);
                ViewBag.OrderID = new SelectList(db.Orders, "ID", "ID", orderDetail.OrderID);
                return View(orderDetail);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
        }

        // GET: Cart/Delete/5
        public ActionResult Delete(int? id, int? id2)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 1)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                OrderDetail orderDetail = db.OrderDetails.Find(id, id2);
                if (orderDetail == null)
                {
                    return HttpNotFound();
                }
                return View(orderDetail);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int id2)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 1)
            {
                OrderDetail orderDetail = db.OrderDetails.Find(id, id2);
                db.OrderDetails.Remove(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
        }


        // GET: Cart/GetOrders
        public ActionResult GetOrders()
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 1)
            {
                int id = Int32.Parse(Request.Cookies["userInfo"]["userID"]);
                var orders = db.Orders.Where(o => o.UserID == id && o.OrderTypeID == 1).ToList();
                List<CartModel> fOrders = new List<CartModel>();
                foreach (var item in orders)
                {
                    List<OrderDetail> items = db.OrderDetails.Where(i => i.OrderID == item.ID).Include(i => i.Item).ToList();
                    CartModel cart = new CartModel();
                    cart.OrderID = item.ID;
                    cart.UserID = item.UserID;
                    cart.OrderDate = item.OrderDate;
                    cart.OrderTypeID = item.OrderTypeID;
                    cart.CartItems = items;
                    fOrders.Add(cart);
                }
                return View(fOrders);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
        }

        // GET: Cart/Checkout
        public ActionResult Checkout(int orderID, double subtotal)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 1)
            {
                CalculateTotalsSoap service = new CalculateTotalsSoapClient();
                double total = service.CalculateTotal(subtotal);
                CheckoutModel checkout = new CheckoutModel();
                checkout.OrderId = orderID;
                checkout.Total = total;
                return View(checkout);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");       
        }

        // GET: Cart/CompleteOrder
        public ActionResult CompleteOrder(int id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 1)
            {
                var order = db.Orders.Find(id);
                if (order != null)
                {
                    order.OrderTypeID = 1;
                    db.SaveChanges();
                    return View();
                }
                ViewBag.Message = "There was an error finalizing your order";
                return View();
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");          
        }
    }
}