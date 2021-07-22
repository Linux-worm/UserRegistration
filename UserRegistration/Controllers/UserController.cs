using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using UserRegistration.Entity;
using UserRegistration.Models;

namespace UserRegistration.Controllers
{
    public class UserController : Controller
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(UserModel user)
        {
            if(ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            return View("success");
        }

        public IActionResult ViewUser()
        {
            return View(_context.Users.ToList());
        }

        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(int id, UserModel userModel)
        {
            var user = _context.Users.Find(id);
            if(user != null)
            {
                user.FullName = userModel.FullName;
                user.UserName = userModel.UserName;
                user.Gender = userModel.Gender;
                user.Password = userModel.Password;
                _context.SaveChanges();                
            }
            return View("success");                
        }
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return View("viewuser"); ;
        }
    }
}
