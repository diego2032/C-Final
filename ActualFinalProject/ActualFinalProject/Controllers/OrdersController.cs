using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ActualFinalProject;

namespace ActualFinalProject.Controllers
{
    public class OrdersController : Controller
    {
        private OnlineShopDBEntities db = new OnlineShopDBEntities();

        // GET: Orders
        public ActionResult Index()
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                var orders = db.Orders.Include(o => o.OrderType).Include(o => o.User);
                return View(orders.ToList());
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
            
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Order order = db.Orders.Find(id);
                if (order == null)
                {
                    return HttpNotFound();
                }
                return View(order);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                ViewBag.OrderTypeID = new SelectList(db.OrderTypes, "ID", "Type");
                ViewBag.UserID = new SelectList(db.Users, "ID", "Username");
                return View();
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");        
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,OrderDate,OrderTypeID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderTypeID = new SelectList(db.OrderTypes, "ID", "Type", order.OrderTypeID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Username", order.UserID);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Order order = db.Orders.Find(id);
                if (order == null)
                {
                    return HttpNotFound();
                }
                ViewBag.OrderTypeID = new SelectList(db.OrderTypes, "ID", "Type", order.OrderTypeID);
                ViewBag.UserID = new SelectList(db.Users, "ID", "Username", order.UserID);
                return View(order);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,OrderDate,OrderTypeID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderTypeID = new SelectList(db.OrderTypes, "ID", "Type", order.OrderTypeID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Username", order.UserID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Order order = db.Orders.Find(id);
                if (order == null)
                {
                    return HttpNotFound();
                }
                return View(order);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");         
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
