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
    public class ItemCategoriesController : Controller
    {
        private OnlineShopDBEntities db = new OnlineShopDBEntities();

        // GET: ItemCategories
        public ActionResult Index()
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                return View(db.ItemCategories.ToList());
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
            
        }

        // GET: ItemCategories/Details/5
        public ActionResult Details(int? id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ItemCategory itemCategory = db.ItemCategories.Find(id);
                if (itemCategory == null)
                {
                    return HttpNotFound();
                }
                return View(itemCategory);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
            
        }

        // GET: ItemCategories/Create
        public ActionResult Create()
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                return View();
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
            
        }

        // POST: ItemCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] ItemCategory itemCategory)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (ModelState.IsValid)
                {
                    db.ItemCategories.Add(itemCategory);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(itemCategory);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");        
        }

        // GET: ItemCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ItemCategory itemCategory = db.ItemCategories.Find(id);
                if (itemCategory == null)
                {
                    return HttpNotFound();
                }
                return View(itemCategory);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
            
        }

        // POST: ItemCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] ItemCategory itemCategory)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(itemCategory).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(itemCategory);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
            
        }

        // GET: ItemCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ItemCategory itemCategory = db.ItemCategories.Find(id);
                if (itemCategory == null)
                {
                    return HttpNotFound();
                }
                return View(itemCategory);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");           
        }

        // POST: ItemCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                ItemCategory itemCategory = db.ItemCategories.Find(id);
                db.ItemCategories.Remove(itemCategory);
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
