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
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext ctx = new ApplicationDbContext();
        // GET: Reviews
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Reviews = ctx.Reviews.ToList();
            return View();
        }

        public ActionResult New(int id)
        {
            Review rev = new Review
            {
                MovieId = id
            };
            return View(rev);
        }

        [HttpPost]
        public ActionResult New(Review reviewRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Movie mov = ctx.Movies.Find(reviewRequest.MovieId);
                    reviewRequest.Author = ctx.Users.Find(User.Identity.GetUserId());
                    reviewRequest.CreatedAt = DateTime.Now;
                    reviewRequest.NumberOfReports = 0;
                    reviewRequest.Movie = mov;
                    ctx.Reviews.Add(reviewRequest);
                    ApplicationUser user = ctx.Users.Find(User.Identity.GetUserId());
                    user.Rated.Add(reviewRequest.Movie);
                    user.Reviews.Add(reviewRequest);
                    ctx.SaveChanges();
                    return RedirectToAction("Index", "Movies");
                }

                return View(reviewRequest);
            }
            catch (Exception e)
            {
                _ = e.Message;
                return View(reviewRequest);
            }
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Review rev = ctx.Reviews.Find(id);
                if (rev != null)
                {
                    ViewBag.Currentusername = User.Identity.GetUserName();
                    ApplicationUser usr = ctx.Users.Find(User.Identity.GetUserId());
                    ViewBag.alreadyrep = usr.ReportedReviews.Contains(rev);
                    return View(rev);
                }
                return HttpNotFound("Couldn't find the review with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing review id parameter!");
        }

        [HttpGet]
        public ActionResult Report(int? id)
        {
            if (id == null)
            {
                return HttpNotFound("Missing id parameter.");
            }
            else
            {
                Review review = ctx.Reviews.Find(id);
                if(review == null)
                {
                    return HttpNotFound("Review not found.");
                }
                else
                {
                    ReportReview reportReview = new ReportReview
                    {
                        Review = review,
                        Reason = "No reason specified.",
                        ReviewId = review.ReviewId
                    };
                    review.NumberOfReports++;
                    ctx.SaveChanges();
                    return View(reportReview);
                }
            }
        }

        [HttpPost]
        public ActionResult Report(int? reviewId, ReportReview reportRequest)
        {
            ApplicationUser current_user = ctx.Users.Find(User.Identity.GetUserId());
            reportRequest.Review = ctx.Reviews.Find(reportRequest.ReviewId);
            current_user.ReportedReviews.Add(reportRequest.Review);
            ctx.ReviewReports.Add(reportRequest);
            ctx.SaveChanges();
            return RedirectToAction("Details", "Movies", new { id = reportRequest.Review.MovieId });
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Review review = ctx.Reviews.Find(id);
                if (review == null)
                {
                    return HttpNotFound("Coludn't find the review with id " + id.ToString() + "!");
                }
                return View(review);
            }
            return HttpNotFound("Missing review id parameter!");
        }

        [HttpPut]
        public ActionResult Edit(int id, Review reviewRequest)
        {
            Review review = ctx.Reviews.Include("Movie")
                        .SingleOrDefault(b => b.ReviewId.Equals(id));

            try
            {
                if (ModelState.IsValid)
                {
                    if (User.Identity.GetUserName() != review.Author.UserName)
                    {
                        return HttpNotFound("Only the author may delete their review.");
                    }
                    if (TryUpdateModel(review))
                    {
                        review = reviewRequest;
                        ctx.SaveChanges();
                    }
                    return RedirectToAction("Index", "Movies");
                }
                return View(reviewRequest);
            }
            catch (Exception)
            {
                return View(reviewRequest);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Review r = ctx.Reviews.Find(id);
            if (r!= null)
            {
                if (r.Author.UserName == User.Identity.GetUserName())
                {
                    var movid = r.Movie.MovieId;
                    ctx.Reviews.Remove(r);
                    ctx.SaveChanges();
                    return RedirectToAction("Index", "Movies");
                }
                return HttpNotFound("Only the author can delete their review.");
            }
            return HttpNotFound("No review found.");

        }



        [NonAction]
        public IEnumerable<SelectListItem> GetAllMovies()
        {
            var selectList = new List<SelectListItem>();
            foreach (var movie in ctx.Movies.ToList())
            {
                selectList.Add(new SelectListItem
                {
                    Value = movie.MovieId.ToString(),
                    Text = movie.Title
                });
            }
            return selectList;
        }

        
    }
}