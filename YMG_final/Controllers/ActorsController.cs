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
    public class ActorsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Actors
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Actors.OrderBy(u => u.FullName).ToList());
        }

        // GET: Actors/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Filmography = db.Movies.ToList();
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            ViewBag.favourited = false;
            if (user != null)
            {
                if (user.FavouriteActors.Contains(actor))
                {
                    ViewBag.favourited = true;
                }
                else
                {
                    ViewBag.favourited = false;
                }
                Actor y = user.FavouriteActors.Where(a => a.FullName == actor.FullName).FirstOrDefault();
                if (y != null)
                {
                    ViewBag.favourited = true;
                }
            }

            return View(actor);

        }

        [HttpGet, Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            Actor actor = new Actor
            {
                MoviesList = GetAllMovies(),
                Filmography = new List<Movie>()
            };
            return View(actor);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult Create(Actor actorRequest)
        {
            var selectedMovies = actorRequest.MoviesList.Where(b => b.Checked).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    actorRequest.Filmography = new List<Movie>();
                    for (int i = 0; i < selectedMovies.Count(); i++)
                    {
                        Movie movie = db.Movies.Find(selectedMovies[i].Id);
                        actorRequest.Filmography.Add(movie);
                    }
                    db.Actors.Add(actorRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(actorRequest);
            }
            catch (Exception)
            {
                return View(actorRequest);
            }
        }

        // GET: Actors/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            actor.MoviesList = GetAllMovies();
            foreach (Movie checkedMovie in actor.Filmography)
            {
                actor.MoviesList.FirstOrDefault(g => g.Id == checkedMovie.MovieId).Checked = true;
            }
            return View(actor);
        }

        [ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult Edit(int id, Actor actorRequest)
        {
            Actor actor = db.Actors.Find(id);
            var selectedMovies = actorRequest.MoviesList.Where(b => b.Checked).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    if (TryUpdateModel(actor))
                    {
                        actor.FullName = actorRequest.FullName;
                        actor.Filmography.Clear();
                        actor.Filmography = new List<Movie>();
                        for (int i = 0; i < selectedMovies.Count(); i++)
                        {
                            Movie movie = db.Movies.Find(selectedMovies[i].Id);
                            actor.Filmography.Add(movie);
                            if (actor.Filmography.Count == 0)
                            {
                                return HttpNotFound("pute si mai tare!!" + i.ToString());
                            }
                            movie.Cast.Add(actor);
                            ActorRole role = new ActorRole
                            {
                                ActorId = actor.ActorId,
                                MovieId = movie.MovieId,
                                Movie = movie,
                                Actor = actor,
                                CharacterName = "n/a"
                            };
                            db.ActorRoles.Add(role);
                            actor.Roles.Add(role);

                            movie.Characters.Add(role);
                        }
                        for(int i = 0; i < actor.Roles.Count; i++)
                        {
                            ActorRole ar = actor.Roles[i];
                            if (actor.Filmography.Where(a => a.MovieId == ar.MovieId).Count() == 0)
                            {
                                db.ActorRoles.Remove(ar);
                            }
                        }
                        foreach (Movie m in actor.Filmography)
                        {
                            if (actor.Roles.Where(x => x.MovieId == m.MovieId).Count() == 0)
                            {
                                ActorRole newRole = new ActorRole
                                {
                                    ActorId = actor.ActorId,
                                    MovieId = m.MovieId,
                                    CharacterName = "n/a",
                                };
                                actor.Roles.Add(newRole);
                                db.ActorRoles.Add(newRole);
                            }
                        }
                        db.SaveChanges();

                    }
                    return RedirectToAction("ManageMovies", "Admin");
                }
                return View(actorRequest);
            }
            catch
            {
                return View(actorRequest);
            }
        }

        [HttpPost]
        public ActionResult AddToFavourites(int? id)
        {
            if (id == null)
            {
                return HttpNotFound("Missing id parameter");
            }
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            Actor act = db.Actors.Find(id);
            user.FavouriteActors.Add(act);
            db.SaveChanges();
            return RedirectToAction("Details", "Actors", new { id = act.ActorId });
        }

        
        [HttpPost]
        public ActionResult RemoveFromFavourites(int? id)
        {
            if (id == null)
            {
                return HttpNotFound("Missing id parameter");
            }
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            Actor act = db.Actors.Find(id);
            user.FavouriteActors.Remove(act);
            db.SaveChanges();
            return RedirectToAction("Details", "Actors", new { id = act.ActorId });
        }

        // POST: Actors/Delete/5
        [HttpDelete, Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                Actor actor = db.Actors.Find(id);
                for (int i = 0; i < actor.Roles.Count(); i++)
                {
                    ActorRole ar = db.ActorRoles.Find(actor.Roles[i].ActorRoleId);
                    db.ActorRoles.Remove(ar);
                }
                db.Actors.Remove(actor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("no match for given id");
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
            foreach (var movie in db.Movies.OrderBy(m => m.Title).ToList())
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
