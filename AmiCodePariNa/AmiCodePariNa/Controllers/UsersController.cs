using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AmiCodePariNa.Data;
using AmiCodePariNa.Models.Khoj;
using AmiCodePariNa.Models.Users;
using AmiCodePariNa.ViewModel;

namespace AmiCodePariNa.Controllers
{
    public class UsersController : Controller
    {
        private AmiCodePariNaContext db;
        public UsersController()
        {
            db = new AmiCodePariNaContext();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserRegistration user)
        {
            if (ModelState.IsValid) // check if the ModelState is valid
            {
                var foundUser = db.Users.FirstOrDefault(u => u.Email == user.Email); // if email already exists then we will show a message
                if (foundUser != null)
                {
                    ViewBag.message = "Email Already Exists!";
                    return View();
                }
                else // if email does not exists and ModelState is valid we store a user in the User table
                {
                    User newUser = new User 
                    { 
                        Name = user.Name,
                        Phone = user.Phone,
                        Email = user.Email,
                        Password = user.Password
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Users"); // Redirect to the login page
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAuthentication user)
        {
            if (ModelState.IsValid)  
            {
                // Compares email and password for login
                var foundUser = db.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
                if (foundUser == null)
                {
                    ViewBag.message = "No such user";
                    return View();
                }
                else
                {
                    // store the UserId for Khoj Action
                    Session["User_id"] = Convert.ToString(foundUser.Id);
                    return RedirectToAction("Khoj", "Users");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Khoj()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Khoj(KhojTheSearch khojNumber)
        {
            if (ModelState.IsValid)
            {
                int user_id = Convert.ToInt32(Session["User_id"] as string);

                // we accept the list of number as a string from user, so we are converting it to a list of integers
                List<int> numberList = khojNumber.listOfNumbers
                .Split(',')
                .Select(num => int.Parse(num.Trim()))
                .ToList();

                // sort the list in descending order
                numberList = numberList.OrderByDescending(num => num).ToList();

                //convert the list back to string for storing in database
                string commaSeparatedString = string.Join(",", numberList);
                Khoj obj = new Khoj 
                {
                    listOfNumbers = commaSeparatedString,
                    date = DateTime.Now,
                    user = db.Users.FirstOrDefault(u => u.Id == user_id),
                    Id = user_id
                };
                db.khoj.Add(obj);
                db.SaveChanges();


                // fetch the list of numbers from database and converting it to a integer array
                int[] numbersInDatabase = db.khoj
                                          .AsEnumerable() // Switch to LINQ to Objects
                                          .Where(k => !string.IsNullOrEmpty(k.listOfNumbers) && k.Id == user_id && k.date == DateTime.Now) // Filter out empty or null lists
                                          .SelectMany(k => k.listOfNumbers.Split(','))
                                          .Select(num => int.Parse(num.Trim()))
                                          .ToArray();

                // if the number id found we show the result True
                if (numbersInDatabase.Contains(khojNumber.findNumber))
                {
                    ViewBag.found = "True";
                }
                else // if not then Output False
                {
                    ViewBag.found = "False";
                }
            }
            return View();
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
