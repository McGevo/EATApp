using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EATApp.Models;
using EATApp.Participants;

namespace EATApp.Controllers
{
    public class attendanceController : Controller
    {
        private TafeDBEntities db = new TafeDBEntities();

        // GET: attendance
        public ActionResult Index()
        {
            var studentsessions = db.studentsessions.Include(s => s.session).Include(s => s.student);
            return View(studentsessions.ToList());
        }

        // GET: attendance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentsession studentsession = db.studentsessions.Find(id);
            if (studentsession == null)
            {
                return HttpNotFound();
            }
            return View(studentsession);
        }

        // GET: attendance/Create
        public ActionResult Create()
        {
            ViewBag.session_sessionID = new SelectList(db.sessions, "sessionID", "Date");
            ViewBag.student_StudentID = new SelectList(db.students, "StudentID", "GivenName");
            return View();
        }

        // POST: attendance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string code)
        {
            if (ModelState.IsValid)
            {
               
                string studentID = "100100100";
                session result = null;
                bool check = false;
                studentsession old = null;
                DateTime now = DateTime.Now;
                
                foreach (var item in db.sessions)
                {
                    if (item.Code.Equals(code))
                    {
                        result = item;
                    }
                }
                
                foreach (var ss in db.studentsessions)
                {
                    if (ss.student_StudentID.Equals(studentID) && ss.session_sessionID.Equals(result.sessionID)){
                        check = true;
                        old = ss;
                    }
                }
                if (check)
                {
                    
                    if (old.SignOut.Equals(null))
                    {
                        
                        old.SignOut = now.TimeOfDay;
                        db.Entry(old).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    studentsession record = new studentsession(studentID, result.sessionID, signIn: now.TimeOfDay);

                    db.studentsessions.Add(record);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }

            
            return View();
        }

        // GET: attendance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentsession studentsession = db.studentsessions.Find(id);
            if (studentsession == null)
            {
                return HttpNotFound();
            }
            ViewBag.session_sessionID = new SelectList(db.sessions, "sessionID", "Date", studentsession.session_sessionID);
            ViewBag.student_StudentID = new SelectList(db.students, "StudentID", "GivenName", studentsession.student_StudentID);
            return View(studentsession);
        }

        // POST: attendance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SignIn,SignOut,session_sessionID,student_StudentID")] studentsession studentsession)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentsession).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.session_sessionID = new SelectList(db.sessions, "sessionID", "Date", studentsession.session_sessionID);
            ViewBag.student_StudentID = new SelectList(db.students, "StudentID", "GivenName", studentsession.student_StudentID);
            return View(studentsession);
        }

        // GET: attendance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentsession studentsession = db.studentsessions.Find(id);
            if (studentsession == null)
            {
                return HttpNotFound();
            }
            return View(studentsession);
        }

        // POST: attendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            studentsession studentsession = db.studentsessions.Find(id);
            db.studentsessions.Remove(studentsession);
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
