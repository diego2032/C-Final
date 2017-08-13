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
    public class ImagesController : Controller
    {
        private OnlineShopDBEntities db = new OnlineShopDBEntities();

        // GET: Images
        public ActionResult Index()
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                return View(db.Images.ToList());
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
        }

        // GET: Images/Details/5
        public ActionResult Details(int? id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Image image = db.Images.Find(id);
                if (image == null)
                {
                    return HttpNotFound();
                }
                return View(image);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                return View();
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
        }


        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ImageLocation")] Image image)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (ModelState.IsValid)
                {
                    db.Images.Add(image);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(image);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
        }

        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Image image = db.Images.Find(id);
                if (image == null)
                {
                    return HttpNotFound();
                }
                return View(image);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ImageLocation")] Image image)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(image).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(image);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
        }

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Image image = db.Images.Find(id);
                if (image == null)
                {
                    return HttpNotFound();
                }
                return View(image);
            }
            return RedirectToAction("UnauthorizedAction", "Unauthorized");
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int authenticate = this.AuthenticateUser(Request);
            if (authenticate == 2)
            {
                Image image = db.Images.Find(id);
                db.Images.Remove(image);
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