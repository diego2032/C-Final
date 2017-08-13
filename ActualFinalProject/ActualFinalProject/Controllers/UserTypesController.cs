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
    public class UserTypesController : Controller
    {
        private OnlineShopDBEntities db = new OnlineShopDBEntities();

        // GET: UserTypes
        public ActionResult Index()
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                return View(db.UserTypes.ToList());
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");         
        }

        // GET: UserTypes/Details/5
        public ActionResult Details(int? id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UserType userType = db.UserTypes.Find(id);
                if (userType == null)
                {
                    return HttpNotFound();
                }
                return View(userType);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");           
        }

        // GET: UserTypes/Create
        public ActionResult Create()
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                return View();
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");         
        }

        // POST: UserTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Type")] UserType userType)
        {
            if (ModelState.IsValid)
            {
                db.UserTypes.Add(userType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userType);
        }

        // GET: UserTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UserType userType = db.UserTypes.Find(id);
                if (userType == null)
                {
                    return HttpNotFound();
                }
                return View(userType);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");          
        }

        // POST: UserTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Type")] UserType userType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userType);
        }

        // GET: UserTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UserType userType = db.UserTypes.Find(id);
                if (userType == null)
                {
                    return HttpNotFound();
                }
                return View(userType);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");          
        }

        // POST: UserTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserType userType = db.UserTypes.Find(id);
            db.UserTypes.Remove(userType);
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
