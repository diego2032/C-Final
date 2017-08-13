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
    public class OrderTypesController : Controller
    {
        private OnlineShopDBEntities db = new OnlineShopDBEntities();

        // GET: OrderTypes
        public ActionResult Index()
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                return View(db.OrderTypes.ToList());
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");         
        }

        // GET: OrderTypes/Details/5
        public ActionResult Details(int? id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                OrderType orderType = db.OrderTypes.Find(id);
                if (orderType == null)
                {
                    return HttpNotFound();
                }
                return View(orderType);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");          
        }

        // GET: OrderTypes/Create
        public ActionResult Create()
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                return View();
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");         
        }

        // POST: OrderTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Type")] OrderType orderType)
        {
            if (ModelState.IsValid)
            {
                db.OrderTypes.Add(orderType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderType);
        }

        // GET: OrderTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                OrderType orderType = db.OrderTypes.Find(id);
                if (orderType == null)
                {
                    return HttpNotFound();
                }
                return View(orderType);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");        
        }

        // POST: OrderTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Type")] OrderType orderType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderType);
        }

        // GET: OrderTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                OrderType orderType = db.OrderTypes.Find(id);
                if (orderType == null)
                {
                    return HttpNotFound();
                }
                return View(orderType);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");         
        }

        // POST: OrderTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderType orderType = db.OrderTypes.Find(id);
            db.OrderTypes.Remove(orderType);
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
