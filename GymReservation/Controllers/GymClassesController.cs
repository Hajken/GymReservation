﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GymReservation.Models;

namespace GymReservation.Controllers
{
    [Authorize]
    public class GymClassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GymClasses
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.GymClasses.ToList());
        }

        // GET: GymClasses/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GymClass gymClass = db.GymClasses.Find(id);
            if (gymClass == null)
            {
                return HttpNotFound();
            }
            return View(gymClass);
        }

        // GET: GymClasses/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: GymClasses1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ID,Name,StartTime,Duration,Description")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                db.GymClasses.Add(gymClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gymClass);
        }

        // GET: GymClasses1/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GymClass gymClass = db.GymClasses.Find(id);
            if (gymClass == null)
            {
                return HttpNotFound();
            }
            return View(gymClass);
        }

        // POST: GymClasses1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID,Name,StartTime,Duration,Description")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gymClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gymClass);
        }

        // GET: GymClasses1/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GymClass gymClass = db.GymClasses.Find(id);
            if (gymClass == null)
            {
                return HttpNotFound();
            }
            return View(gymClass);
        }

        // POST: GymClasses1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            GymClass gymClass = db.GymClasses.Find(id);
            db.GymClasses.Remove(gymClass);
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
        
        public ActionResult ReservationToggle(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            GymClass currentClass = db.GymClasses.FirstOrDefault(g => g.ID == id);
            ApplicationUser currentUser = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (currentClass != null)
            {


                if (currentClass.AttendingMembers.Contains(currentUser))
                {
                    currentClass.AttendingMembers.Remove(currentUser);
                    db.SaveChanges();
                }
                else
                {
                    currentClass.AttendingMembers.Add(currentUser);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult History()
        {
            return View(db.GymClasses.ToList());

        }
        public ActionResult Reservation()
        {
            ApplicationUser currentUser = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            List<GymClass> gymClassList = new List<GymClass>();
            foreach (var gymItem in db.GymClasses.ToList())
            {
                if (gymItem.AttendingMembers.Contains(currentUser))
                {
                    gymClassList.Add(gymItem);
                }
            }
            return View(gymClassList);

        }
    }
}
