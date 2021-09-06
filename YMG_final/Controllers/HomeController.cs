using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YMG.Models;

namespace YMG.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.Boolean = User.IsInRole("Admin");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FairUse()
        {
            return View();
        }

        public ViewResult Search(string searchQuery)
        {
            List<Movie> movies_t = new List<Movie>();
            List<Movie> movies_d = new List<Movie>();
            List<Movie> movies_de = new List<Movie>();
            List<Movie> movies_y = new List<Movie>();
            List<Movie> movies_k = new List<Movie>();
            List<Genre> genres = new List<Genre>();

            List<Actor> actors = new List<Actor>();
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.ToLower();
                movies_t = ctx.Movies.Where(m => m.Title.ToLower().Contains(searchQuery)).ToList();
                movies_d = ctx.Movies.Where(m => m.Director.ToLower().Contains(searchQuery)).ToList();
                movies_de = ctx.Movies.Where(m => m.Description.ToLower().Contains(searchQuery)).ToList();
                movies_y = ctx.Movies.Where(m => m.Year.ToString().ToLower().Contains(searchQuery)).ToList();
                genres = ctx.Genres.Where(g => g.Name.ToLower().Contains(searchQuery)).ToList();

                actors = ctx.Actors.Where(a => a.FullName.ToLower().Contains(searchQuery)).ToList();
            }

            /*foreach(Movie m in ctx.Movies)
            {
                int i = 0;
                if(m.Keywords.Contains(searchQuery))
                {
                    i++;
                    movies_k.Add(m);
                }
            }*/

            ViewBag.mov_t = movies_t;
            ViewBag.mov_d = movies_d;
            ViewBag.mov_de = movies_de;
            ViewBag.mov_y = movies_y;
            ViewBag.mov_k = movies_k;
            ViewBag.gen = genres;
            ViewBag.act = actors;
            ViewBag.q = searchQuery;
            return View();
        }

        public FileContentResult UserPhotos()
        {
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();

                if (userId == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Content/Images/default_profile_picture.png");

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");

                }
                // to get the user details to load user Image
                var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userImage = bdUsers.Users.Where(x => x.Id == userId).FirstOrDefault();
                if(userImage == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Content/Images/default_profile_picture.png");

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);
                    return File(imageData, "image/png");
                }
                if (userImage.ProfilePicture == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Content/Images/default_profile_picture.png");

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");
                }
                if (userImage.ProfilePicture.Length == 0)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Content/Images/default_profile_picture.png");

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");
                }

                return new FileContentResult(userImage.ProfilePicture, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/default_profile_picture.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");

            }
        }
    }
}