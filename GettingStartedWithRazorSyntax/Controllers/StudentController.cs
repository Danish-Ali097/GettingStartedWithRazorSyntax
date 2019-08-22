using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GettingStartedWithRazorSyntax.Models;

namespace GettingStartedWithRazorSyntax.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        //Add record
        [HttpPost]
        public ActionResult Index(Student student)
        {
            if (ModelState.IsValid)
            {
                try 
                {
                    StudentEntities db = new StudentEntities();
                    db.Students.Add(student);
                    db.SaveChanges();
                    ViewBag.Message = "Record Inserted successfully";
                } catch (Exception ex) {
                    throw;
                }
            }
            return View();
        }

        //View Record
        public ActionResult ViewAll()
        {
            StudentEntities db = new StudentEntities();
            return View(db.Students.ToList<Student>());
        }

        //Get record by id
        public ActionResult Edit(int id)
        {
            StudentEntities db = new StudentEntities();
            var result = from x in db.Students.ToList() where x.Id == id select x;
            Student std = new Student();
            foreach (var x in result)
            {
                std = x;
            }
            return View(std);
        }

        //Edit record
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            StudentEntities db = new StudentEntities();
            if (ModelState.IsValid) {
                var temp = db.Students.Find(student.Id);
                temp.Id = student.Id;
                temp.Name = student.Name;
                temp.Age = student.Age;
                temp.Address = student.Address;
                db.Entry(temp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewAll");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        //Delete record
        public ActionResult Delete(int id)
        {
            StudentEntities db = new StudentEntities();
            var std = db.Students.Find(id);
            if (std != null)
            {
                db.Students.Remove(std);
                db.SaveChanges();
                return RedirectToAction("ViewAll");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }

        //About
        public ActionResult About()
        {
            return View();
        }

        //Contact
        public ActionResult Contact()
        {
            return View();
        }
    }
}