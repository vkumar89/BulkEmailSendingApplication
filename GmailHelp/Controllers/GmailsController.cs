using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GmailHelp.Models;

namespace GmailHelp.Controllers
{
    public class GmailsController : Controller
    {
        private MailDBContext db = new MailDBContext();

        // GET: Gmails
        public ActionResult Index()
        {
            return View(db.gmail.ToList());
        }

        // GET: Gmails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gmail gmail = db.gmail.Find(id);
            if (gmail == null)
            {
                return HttpNotFound();
            }
            return View(gmail);
        }

        // GET: Gmails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gmails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,To,From,Subject,Body,PName,CompanyName")] Gmail gmail)
        {
            if (ModelState.IsValid)
            {
                db.gmail.Add(gmail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gmail);
        }

        // GET: Gmails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gmail gmail = db.gmail.Find(id);
            if (gmail == null)
            {
                return HttpNotFound();
            }
            return View(gmail);
        }

        // POST: Gmails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,To,From,Subject,Body,PName,CompanyName")] Gmail gmail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gmail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gmail);
        }

        // GET: Gmails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gmail gmail = db.gmail.Find(id);
            if (gmail == null)
            {
                return HttpNotFound();
            }
            return View(gmail);
        }

        // POST: Gmails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gmail gmail = db.gmail.Find(id);
            db.gmail.Remove(gmail);
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
