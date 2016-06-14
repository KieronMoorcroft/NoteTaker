using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NoteTaker.Models;
using Microsoft.AspNet.Identity;

namespace NoteTaker.Controllers
{
    public class NotesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Notes
        public async Task<ActionResult> Index()
        {

            var userId = User.Identity.GetUserId();

            var userNotes =
                await
                    db.Users.Where(u => u.Id == userId)
                        .Include(u => u.Notes)                       
                        .ToListAsync();

            return View(userNotes);

        }

        // GET: Notes/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userId = User.Identity.GetUserId();

            Notes notes = db.Notes.Find(id);
            if (notes == null)
            {
                return HttpNotFound();
            }
            return View(notes);
        }

        // GET: Notes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Text,CreatedAt")] Notes notes)
        {
            if (ModelState.IsValid)
            {
                db.Notes.Add(notes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(notes);
        }

        // GET: Notes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notes notes = await db.Notes.FindAsync(id);
            if (notes == null)
            {
                return HttpNotFound();
            }
            return View(notes);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Text,CreatedAt")] Notes notes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(notes);
        }

        // GET: Notes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notes notes = await db.Notes.FindAsync(id);
            if (notes == null)
            {
                return HttpNotFound();
            }
            return View(notes);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Notes notes = await db.Notes.FindAsync(id);
            db.Notes.Remove(notes);
            await db.SaveChangesAsync();
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
