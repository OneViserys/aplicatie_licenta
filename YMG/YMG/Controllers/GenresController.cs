using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YMG.Models;

namespace YMG.Controllers
{
    [Authorize]
    public class GenresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Genres
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Genres.ToList());
        }

        [AllowAnonymous]
        // GET: Genres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            if(user==null)
            {
                ViewBag.favourited = false;
                return View(genre);
            }
            if(user.PreferredGenres.Contains(genre))
            {
                ViewBag.favourited = true;
            }
            else
            {
                ViewBag.favourited = false;
            }
            return View(genre);
        }

        [HttpPost]
        public ActionResult AddToFavourites(int? id)
        {
            if (id == null)
            {
                return HttpNotFound("Missing id parameter");
            }
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            Genre gen = db.Genres.Find(id);
            user.PreferredGenres.Add(gen);
            db.SaveChanges();
            return RedirectToAction("Details", "Genres", new { id = gen.GenreId });
        }

        [HttpPost]
        public ActionResult RemoveFromFavourites(int? id)
        {
            if (id == null)
            {
                return HttpNotFound("Missing id parameter");
            }
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            Genre gen = db.Genres.Find(id);
            user.PreferredGenres.Remove(gen);
            db.SaveChanges();
            return RedirectToAction("Details", "Genres", new { id = gen.GenreId });
        }

        // GET: Genres/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            Genre genre = new Genre
            {
                MoviesList = GetAllMovies(),
                Movies = new List<Movie>()
            };
            return View(genre);
        }

        // POST: Genres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public ActionResult Create(Genre genreRequest)
        {
            var selectedMovies = genreRequest.MoviesList.Where(b => b.Checked).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    genreRequest.Movies = new List<Movie>();
                    for (int i = 0; i < selectedMovies.Count(); i++)
                    {
                         
                        Movie movie = db.Movies.Find(selectedMovies[i].Id);
                        genreRequest.Movies.Add(movie);
                    }
                    db.Genres.Add(genreRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(genreRequest);
            }
            catch (Exception)
            {
                return View(genreRequest);
            }
        }

        // GET: Genres/Edit/5
        [Authorize(Roles ="Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            genre.MoviesList = GetAllMovies();
            foreach (Movie checkedMovie in genre.Movies)
            {
                genre.MoviesList.FirstOrDefault(g => g.Id == checkedMovie.MovieId).Checked = true;
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public ActionResult Edit(int id, Genre genreRequest)
        {
            Genre genre = db.Genres.SingleOrDefault(m => m.GenreId.Equals(id));
            var selectedMovies = genreRequest.MoviesList.Where(m => m.Checked).ToList();

            if (ModelState.IsValid)
            {
                if (TryUpdateModel(genre))
                {
                    genre.Name = genreRequest.Name;

                    genre.Movies.Clear();
                    genre.Movies = new List<Movie>();

                    for (int i = 0; i < selectedMovies.Count(); i++)
                    {
                        Movie movie = db.Movies.Find(selectedMovies[i].Id);
                        genre.Movies.Add(movie);
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        // GET: Genres/Delete/5
        [Authorize(Roles ="Admin"), HttpDelete]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            db.Genres.Remove(genre);
            db.SaveChanges();
            return RedirectToAction("ManageGenres", "Admin");
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Genre genre = db.Genres.Find(id);
            db.Genres.Remove(genre);
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

        [NonAction]
        public List<CheckBoxViewModel> GetAllMovies()
        {
            var checkboxList = new List<CheckBoxViewModel>();
            foreach (var movie in db.Movies.ToList())
            {
                checkboxList.Add(new CheckBoxViewModel
                {
                    Id = movie.MovieId,
                    Name = movie.Title,
                    Checked = false
                });
            }
            return checkboxList;
        }
    }
}
