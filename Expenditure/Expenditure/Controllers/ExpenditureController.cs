using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Expenditure.Models;

namespace Expenditure.Controllers
{
    public class ExpenditureController : Controller
    {
        private ExpenditureEntities db = new ExpenditureEntities();

        // GET: /Expenditure/
        public ActionResult Index()
        {
            var model = db.Expends.ToList();
            return View(model);
        }

        // GET: /Expenditure/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expend expend = db.Expends.Find(id);
            if (expend == null)
            {
                return HttpNotFound();
            }
            return View(expend);
        }

        // GET: /Expenditure/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Expenditure/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Expend expend)
        {
            if (expend.Amount < 1)
                ModelState.AddModelError("Amount", "Số tiền quá ít!");
            if (ModelState.IsValid)
            {
                expend.Date = DateTime.Now;
                db.Expends.Add(expend);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expend);
        }

        private void ValidateExpenditure(Expend model)
        {
            if (model.Amount <= 0)
                ModelState.AddModelError("Amount", "Số tiền quá ít!");
        }

        // GET: /Expenditure/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expend expend = db.Expends.Find(id);
            if (expend == null)
            {
                return HttpNotFound();
            }
            return View(expend);
        }

        // POST: /Expenditure/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Date,Amount,Note")] Expend expend)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expend).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expend);
        }

        // GET: /Expenditure/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expend expend = db.Expends.Find(id);
            if (expend == null)
            {
                return HttpNotFound();
            }
            return View(expend);
        }

        // POST: /Expenditure/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expend expend = db.Expends.Find(id);
            db.Expends.Remove(expend);
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
