using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using aaCHAT.Models;

namespace aaCHAT.Controllers
{
    public class MessageSessionsController : Controller
    {
        aaCHAT.Models.aaCHAT db = new Models.aaCHAT();

        public ActionResult Messages(int id)
        {
            ViewBag.SessionID = id;
            return View(db.MessageSessions.Where(m => m.ChatSessionID == id));
        }

        public JsonResult AddNewMsg(string body, string from, int chatSessionID)
        {
            db.MessageSessions.Add(new MessageSession { Body = body, From = from, ChatSessionID = chatSessionID, SentAt = DateTime.Now });
            int r;
            try { r = db.SaveChanges(); }
            catch { r = 0; }
            if (r >0)
                return Json(new { state = "True" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { state = "False" }, JsonRequestBehavior.AllowGet);
        }

        // GET: MessageSessions
        public ActionResult Index()
        {
            var messageSessions = db.MessageSessions.Include(m => m.ChatSession);
            return View(messageSessions.ToList());
        }

        // GET: MessageSessions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageSession messageSession = db.MessageSessions.Find(id);
            if (messageSession == null)
            {
                return HttpNotFound();
            }
            return View(messageSession);
        }

        // GET: MessageSessions/Create
        public ActionResult Create()
        {
            ViewBag.ChatSessionID = new SelectList(db.ChatSessions, "ID", "ID");
            return View();
        }

        // POST: MessageSessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Body,From,SentAt,ChatSessionID")] MessageSession messageSession)
        {
            if (ModelState.IsValid)
            {
                db.MessageSessions.Add(messageSession);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChatSessionID = new SelectList(db.ChatSessions, "ID", "ID", messageSession.ChatSessionID);
            return View(messageSession);
        }

        // GET: MessageSessions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageSession messageSession = db.MessageSessions.Find(id);
            if (messageSession == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChatSessionID = new SelectList(db.ChatSessions, "ID", "ID", messageSession.ChatSessionID);
            return View(messageSession);
        }

        // POST: MessageSessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Body,From,SentAt,ChatSessionID")] MessageSession messageSession)
        {
            if (ModelState.IsValid)
            {
                db.Entry(messageSession).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChatSessionID = new SelectList(db.ChatSessions, "ID", "ID", messageSession.ChatSessionID);
            return View(messageSession);
        }

        // GET: MessageSessions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageSession messageSession = db.MessageSessions.Find(id);
            if (messageSession == null)
            {
                return HttpNotFound();
            }
            return View(messageSession);
        }

        // POST: MessageSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MessageSession messageSession = db.MessageSessions.Find(id);
            db.MessageSessions.Remove(messageSession);
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
