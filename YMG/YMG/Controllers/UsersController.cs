using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YMG.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace YMG.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {

        private ApplicationDbContext ctx = new ApplicationDbContext();
        // GET: Users
        public ActionResult Index()
        {
            List<ApplicationUser> users = ctx.Users.OrderBy(u => u.UserName).ToList();
            ViewBag.Users = users;
            return View();
        }

        public ActionResult Details(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return HttpNotFound("Missing the ID parameter!");
            }
            ApplicationUser user = ctx.Users.Include("Roles").FirstOrDefault(u => u.Id.Equals(id));
            if (user != null)
            {
                ViewBag.UserRole = ctx.Roles.Find(user.Roles.First().RoleId).Name;
                return View(user);
            }
            return HttpNotFound("Could not find the user with the given id!");
        }

        public ActionResult Edit(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return HttpNotFound("Missing the ID parameter!");
            }

            UserViewModel uvm = new UserViewModel
            {
                User = ctx.Users.Find(id)
            };
            IdentityRole userRole = ctx.Roles.Find(uvm.User.Roles.First().RoleId);
            uvm.RoleName = userRole.Name;
            return View(uvm);
        }


        [HttpPut]
        public ActionResult Edit(string id, UserViewModel uvm)
        {
            ApplicationUser user = ctx.Users.Find(id);
            try
            {
                if (TryUpdateModel(user))
                {
                    var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));

                    foreach (var r in ctx.Roles.ToList())
                    {
                        um.RemoveFromRole(user.Id, r.Name);
                    }
                    um.AddToRole(user.Id, uvm.RoleName);

                    user.UserName = uvm.User.Email;
                    user.Email = uvm.User.Email;
                    ctx.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(uvm);
            }
        }

        public ActionResult Delete(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return HttpNotFound("Missing id parameter.");
            }
            ApplicationUser user = ctx.Users.Find(id);
            if(user!=null)
            {
                ctx.Users.Remove(user);
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Couldn't find user with specified id.");
        }
    }
    }