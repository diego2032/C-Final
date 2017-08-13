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
    public class OrderDetailsController : Controller
    {
        private OnlineShopDBEntities db = new OnlineShopDBEntities();

        // GET: OrderDetails
        public ActionResult Index()
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate != 0)
            {
                var orderDetails = db.OrderDetails.Include(o => o.Item).Include(o => o.Order);
                return View(orderDetails.ToList());
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");           
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id, int? id2)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate != 0)
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

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate != 0)
            {
                ViewBag.ItemID = new SelectList(db.Items, "ID", "ItemName");
                ViewBag.OrderID = new SelectList(db.Orders, "ID", "ID");
                return View();
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");          
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,ItemID,Quantity")] OrderDetail orderDetail)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate != 0)
            {
                if (ModelState.IsValid)
                {
                    db.OrderDetails.Add(orderDetail);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ItemID = new SelectList(db.Items, "ID", "ItemName", orderDetail.ItemID);
                ViewBag.OrderID = new SelectList(db.Orders, "ID", "ID", orderDetail.OrderID);
                return View(orderDetail);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");           
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id, int? id2)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate != 0)
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

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,ItemID,Quantity")] OrderDetail orderDetail)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate != 0)
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

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id, int? id2)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate != 0)
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

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int id2)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate != 0)
            {
                OrderDetail orderDetail = db.OrderDetails.Find(id, id2);
                db.OrderDetails.Remove(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
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
