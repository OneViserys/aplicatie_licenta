using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YMG.Models;

namespace YMG.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();
        // GET: Movies
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Movies = ctx.Movies.Where(m => m.Released == true).OrderBy(u => u.Title).ToList();
            return View();
        }

        [AllowAnonymous]
        public ActionResult ComingSoon()
        {
            ViewBag.ComingSoon = ctx.Movies.Where(m => m.Released == false).OrderBy(m => m.Year).ToList();
            return View();
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            Movie movie = new Movie
            {
                GenresList = GetAllGenres(),
                ActorsList = GetAllActors(),
                Genres = new List<Genre>(),
                Cast = new List<Actor>()
            };
            return View(movie);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult Create(Movie movieRequest)
        {
            var selectedGenres = movieRequest.GenresList.Where(b => b.Checked).ToList();
            var selectedActors = movieRequest.ActorsList.Where(b => b.Checked).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    movieRequest.Genres = new List<Genre>();
                    for (int i = 0; i < selectedGenres.Count(); i++)
                    {
                        Genre genre = ctx.Genres.Find(selectedGenres[i].Id);
                        movieRequest.Genres.Add(genre);
                    }
                    movieRequest.Cast = new List<Actor>();
                    for (int i = 0; i < selectedActors.Count(); i++)
                    {
                        Actor actor = ctx.Actors.Find(selectedActors[i].Id);
                        movieRequest.Cast.Add(actor);
                    }
                    movieRequest.Forum = new DiscussionForum();
                    ctx.Movies.Add(movieRequest);
                    ctx.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(movieRequest);
            }
            catch (Exception)
            {
                return View(movieRequest);
            }
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Movie mov = ctx.Movies.Find(id);
                if (mov != null)
                {
                    Double counter = 0;
                    Double sum = 0;
                    if (mov.Reviews != null)
                    {
                        foreach (var m in mov.Reviews)
                        {
                            var r = int.Parse(m.Rating);
                            sum += r;
                            counter++;
                        }
                        if (counter == 0)
                        {
                            ViewBag.app_rating = "This movie has not been reviewed yet.";
                        }
                        else
                        {
                            Double approx_rating = sum / counter;
                            approx_rating = Math.Round(approx_rating, 2);
                            ViewBag.app_rating = approx_rating;
                        }

                    }
                    else
                    {
                        ViewBag.app_rating = "This movie has not been reviewed yet.";
                    }
                    ApplicationUser user = ctx.Users.Find(User.Identity.GetUserId());
                    ViewBag.revs = false;
                    ViewBag.favmov = false;
                    ViewBag.watchlist = false;
                    ViewBag.seen = false;
                    if (user != null)
                    {
                        ViewBag.revs = user.Rated.Contains(mov);
                        ViewBag.favmov = user.FavouriteMovies.Contains(mov);
                        ViewBag.watchlist = user.Watchlist.Contains(mov);
                        ViewBag.seen = user.Seen.Contains(mov);
                    }

                    return View(mov);
                }
                return HttpNotFound("No movie found for id " + id.ToString());
            }
            return HttpNotFound("Missing id parameter");
        }
        [HttpPost]
        public ActionResult AddToFavourites(int? movieId)
        {
            if (movieId == null)
            {
                return HttpNotFound("Missing id parameter");
            }
            ApplicationUser user = ctx.Users.Find(User.Identity.GetUserId());
            Movie mov = ctx.Movies.Find(movieId);
            user.FavouriteMovies.Add(mov);
            ctx.SaveChanges();
            return RedirectToAction("Details", "Movies", new { id = mov.MovieId });
        }
        [HttpPost]
        public ActionResult AddToWatchlist(int? movieId)
        {
            if (movieId == null)
            {
                return HttpNotFound("Missing id parameter");
            }
            ApplicationUser user = ctx.Users.Find(User.Identity.GetUserId());
            Movie mov = ctx.Movies.Find(movieId);
            user.Watchlist.Add(mov);
            ctx.SaveChanges();
            return RedirectToAction("Details", "Movies", new { id = mov.MovieId });
        }
        [HttpPost]
        public ActionResult AddToSeen(int? movieId)
        {
            if (movieId == null)
            {
                return HttpNotFound("Missing id parameter");
            }
            ApplicationUser user = ctx.Users.Find(User.Identity.GetUserId());
            Movie mov = ctx.Movies.Find(movieId);
            user.Seen.Add(mov);
            ctx.SaveChanges();
            return RedirectToAction("Details", "Movies", new { id = mov.MovieId });
        }
        [HttpPost]
        public ActionResult RemoveFromFavourites(int? movieId)
        {
            if (movieId == null)
            {
                return HttpNotFound("Missing id parameter");
            }
            ApplicationUser user = ctx.Users.Find(User.Identity.GetUserId());
            Movie mov = ctx.Movies.Find(movieId);
            user.FavouriteMovies.Remove(mov);
            ctx.SaveChanges();
            return RedirectToAction("Details", "Movies", new { id = mov.MovieId });
        }
        [HttpPost]
        public ActionResult RemoveFromWatchlist(int? movieId)
        {
            if (movieId == null)
            {
                return HttpNotFound("Missing id parameter");
            }
            ApplicationUser user = ctx.Users.Find(User.Identity.GetUserId());
            Movie mov = ctx.Movies.Find(movieId);
            user.Watchlist.Remove(mov);
            ctx.SaveChanges();
            return RedirectToAction("Details", "Movies", new { id = mov.MovieId });
        }
        [HttpPost]
        public ActionResult RemoveFromSeen(int? movieId)
        {
            if (movieId == null)
            {
                return HttpNotFound("Missing id parameter");
            }
            ApplicationUser user = ctx.Users.Find(User.Identity.GetUserId());
            Movie mov = ctx.Movies.Find(movieId);
            user.Seen.Remove(mov);
            ctx.SaveChanges();
            return RedirectToAction("Details", "Movies", new { id = mov.MovieId });
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult AddRoles(int? id)
        {
            if (id == null)
            {
                return HttpNotFound("Missing id parameter!");
            }
            Movie mov = ctx.Movies.Find(id);
            if (mov == null)
            {
                return HttpNotFound("Movie not found!");
            }
            List<ActorRole> ar = new List<ActorRole>();
            foreach (Actor a in mov.Cast)
            {
                ActorRole y = new ActorRole { ActorId = a.ActorId, Actor = a, MovieId = mov.MovieId, Movie = mov };
                ar.Add(y);
            }
            mov.Characters.Clear();
            mov.Characters = ar;
            return View(mov);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult AddRoles(int? id, Movie movieRequest)
        {
            if (id == null)
            {
                return HttpNotFound("Missing id parameter!");
            }
            Movie mov = ctx.Movies.Find(id);
            if (mov == null)
            {
                return HttpNotFound("Movie not found!");
            }

            mov.Characters.Clear();
            mov.Characters = movieRequest.Characters;
            ctx.ActorRoles.AddRange(mov.Characters);
            ctx.SaveChanges();
            return RedirectToAction("Index", "Admin");

        }

        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult EditRoles(int? id)
        {
            if (id == null)
            {
                return HttpNotFound("Missing id parameter!");
            }
            Movie mov = ctx.Movies.Find(id);
            if (mov == null)
            {
                return HttpNotFound("Movie not found!");
            }
            return View(mov);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult EditRoles(int? id, Movie movieRequest)
        {
            if (id == null)
            {
                return HttpNotFound("Missing id parameter!");
            }
            Movie mov = ctx.Movies.Find(id);
            if (mov == null)
            {
                return HttpNotFound("Movie not found!");
            }
            for (int i = 0; i < mov.Characters.Count; i++)
            {
                ActorRole actrole = ctx.ActorRoles.Find(mov.Characters[i].ActorRoleId);
                actrole.CharacterName = movieRequest.Characters[i].CharacterName;
            }
            ctx.SaveChanges();
            return RedirectToAction("Index", "Admin");

        }

        [HttpDelete, Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                Movie movie = ctx.Movies.Find(id);
                if (movie != null)
                {
                    if (movie.Reviews.Count()!=0)
                    {
                        foreach (var review in movie.Reviews.ToList())
                        {
                            ctx.Reviews.Remove(review);
                            
                        }
                    }
                    foreach(ActorRole ar in movie.Characters.ToList())
                    {
                        ctx.ActorRoles.Remove(ar);
                    }
                   
                    ctx.Forums.Remove(movie.Forum);
                    ctx.Movies.Remove(movie);
                    ctx.SaveChanges();
                    return RedirectToAction("Index", "Admin");
                }
                return HttpNotFound("No movie found with the id " + id.ToString());
            }
            return HttpNotFound("missing the id parameter");
        }

        [NonAction]
        public List<CheckBoxViewModel> GetAllActors()
        {
            var checkboxList = new List<CheckBoxViewModel>();
            foreach (Actor actor in ctx.Actors.ToList().OrderBy(a => a.FullName))
            {
                checkboxList.Add(new CheckBoxViewModel
                {
                    Id = actor.ActorId,
                    Name = actor.FullName,
                    Checked = false
                });
            }
            return checkboxList;
        }

        [NonAction]
        public List<CheckBoxViewModel> GetAllGenres()
        {
            var checkboxList = new List<CheckBoxViewModel>();
            foreach (var genre in ctx.Genres.ToList().OrderBy(g => g.Name))
            {
                checkboxList.Add(new CheckBoxViewModel
                {
                    Id = genre.GenreId,
                    Name = genre.Name,
                    Checked = false
                });
            }
            return checkboxList;
        }


        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Movie movie = ctx.Movies.Find(id);
                movie.ActorsList = GetAllActors();
                movie.GenresList = GetAllGenres();

                foreach (Genre checkedGenre in movie.Genres)
                {   // iteram prin genurile care erau atribuite filmului inainte de momentul accesarii formularului
                    // si le selectam/bifam  in lista de checkbox-uri
                    movie.GenresList.FirstOrDefault(g => g.Id == checkedGenre.GenreId).Checked = true;
                }
                foreach (Actor checkedActor in movie.Cast)
                {
                    movie.ActorsList.FirstOrDefault(a => a.Id == checkedActor.ActorId).Checked = true;
                }
                if (movie == null)
                {
                    return HttpNotFound("Coludn't find the movie with id " + id.ToString() + "!");
                }
                return View(movie);
            }
            return HttpNotFound("Missing movie id parameter!");
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, Movie movieRequest)
        {
            Movie movie = ctx.Movies.SingleOrDefault(m => m.MovieId.Equals(id));

            var selectedGenres = movieRequest.GenresList.Where(m => m.Checked).ToList();
            var selectedActors = movieRequest.ActorsList.Where(m => m.Checked).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    if (TryUpdateModel(movie))
                    {
                        movie.Title = movieRequest.Title;
                        movie.Director = movieRequest.Director;
                        movie.Description = movieRequest.Description;
                        movie.Year = movieRequest.Year;
                        movie.CopyrightMessage = movieRequest.CopyrightMessage;
                        movie.PosterUrl = movieRequest.PosterUrl;
                        movie.Released = movieRequest.Released;
                        movie.TrailerUrl = movieRequest.TrailerUrl;
                        movie.Genres.Clear();
                        movie.Genres = new List<Genre>();

                        movie.Cast.Clear();
                        movie.Cast = new List<Actor>();

                        for (int i = 0; i < selectedGenres.Count(); i++)
                        {
                            Genre genre = ctx.Genres.Find(selectedGenres[i].Id);
                            movie.Genres.Add(genre);
                        }
                        for (int i = 0; i < selectedActors.Count(); i++)
                        {
                            Actor actor = ctx.Actors.Find(selectedActors[i].Id);
                            movie.Cast.Add(actor);
                            if (movie.Characters.Where(z => z.ActorId == actor.ActorId).Count() == 0)
                            {
                                ActorRole newRole = new ActorRole
                                {
                                    ActorId = actor.ActorId,
                                    CharacterName = "n/a",
                                    MovieId = movie.MovieId
                                };
                                movie.Characters.Add(newRole);
                                ctx.ActorRoles.Add(newRole);
                            }
                        }                        
                        for (int i = 0; i < movie.Characters.Count; i++)
                        {
                            if (movie.Cast.Where(x => x.ActorId == movie.Characters[i].ActorId).Count() == 0)
                            {
                                ActorRole acrole = ctx.ActorRoles.Find(movie.Characters[i].ActorRoleId);
                                ctx.ActorRoles.Remove(acrole);
                            }
                        }
                        try
                        {
                            ctx.SaveChanges();
                        }
                        catch(Exception e)
                        {
                            ViewBag.y = e.Message;
                            return View(movieRequest);
                        }
                    }
                    return RedirectToAction("Index", "Admin");


                }
                List<ModelError> errs = new List<ModelError>();
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {

                        errs.Add(error);
                    }
                }
                ViewBag.y = errs;
                return View(movieRequest);
            }
            catch (Exception)
            {

                List<ModelError> errs = new List<ModelError>();
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        
                        errs.Add(error);
                    }
                }
                ViewBag.y = errs;
                return View(movieRequest);
            }
        }

    }
}