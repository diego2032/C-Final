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
    public class ItemsController : Controller
    {
        private OnlineShopDBEntities db = new OnlineShopDBEntities();

        // GET: Items
        public ActionResult Index()
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                var items = db.Items.Include(i => i.Image).Include(i => i.ItemCategory);
                return View(items.ToList());
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");           
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Item item = db.Items.Find(id);
                if (item == null)
                {
                    return HttpNotFound();
                }
                return View(item);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");         
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                ViewBag.ImageID = new SelectList(db.Images, "ID", "ImageLocation");
                ViewBag.CategoryID = new SelectList(db.ItemCategories, "ID", "Name");
                return View();
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");         
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CategoryID,ItemName,ItemDescription,ItemPrice,ImageID")] Item item)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (ModelState.IsValid)
                {
                    db.Items.Add(item);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ImageID = new SelectList(db.Images, "ID", "ImageLocation", item.ImageID);
                ViewBag.CategoryID = new SelectList(db.ItemCategories, "ID", "Name", item.CategoryID);
                return View(item);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");           
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Item item = db.Items.Find(id);
                if (item == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ImageID = new SelectList(db.Images, "ID", "ImageLocation", item.ImageID);
                ViewBag.CategoryID = new SelectList(db.ItemCategories, "ID", "Name", item.CategoryID);
                return View(item);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");          
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CategoryID,ItemName,ItemDescription,ItemPrice,ImageID")] Item item)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ImageID = new SelectList(db.Images, "ID", "ImageLocation", item.ImageID);
                ViewBag.CategoryID = new SelectList(db.ItemCategories, "ID", "Name", item.CategoryID);
                return View(item);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");           
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Item item = db.Items.Find(id);
                if (item == null)
                {
                    return HttpNotFound();
                }
                return View(item);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");           
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                Item item = db.Items.Find(id);
                db.Items.Remove(item);
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
