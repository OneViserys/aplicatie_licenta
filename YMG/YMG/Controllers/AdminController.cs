using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YMG.Models;

namespace YMG.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            ApplicationUser admin = ctx.Users.Find(User.Identity.GetUserId());
            return View(admin);
        }

        [HttpGet]
        public ActionResult ManageReports()
        {
            List<ReportReview> reportedReviews = ctx.ReviewReports.OrderByDescending(m => m.Review.NumberOfReports).ToList();
            ViewBag.replist = reportedReviews;
            return View();
        }

        public ActionResult ReportReviewDetails(int? id)
        {
            if (id.HasValue)
            {
                ReportReview rev = ctx.ReviewReports.Find(id);
                if (rev != null)
                {
                    return View(rev);
                }
                return HttpNotFound("Couldn't find the reported review with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing report review id parameter!");
        }

        [HttpDelete]
        public ActionResult DeleteReview(int id)
        {
            ReportReview reprev = ctx.ReviewReports.Find(id);
            Review r = ctx.Reviews.Find(reprev.ReviewId);
            // var reportsList = ctx.ReviewReports.Where(m => m.ReportReviewId == reprev.ReportReviewId);
            if (r != null)
            {
                /*if(reportsList.Count() > 0)
                {
                    foreach (ReportReview aux in reportsList)
                    {
                        ctx.ReviewReports.Remove(aux);
                    }
                }*/
                
                    ctx.Reviews.Remove(r);
                    ctx.SaveChanges();
                    return RedirectToAction("ManageReports", "Admin");
            }
            return HttpNotFound("No review found.");

        }

        [HttpDelete]
        public ActionResult ClearReviewReport(int id)
        {
            ReportReview reprev = ctx.ReviewReports.Find(id);
            if (reprev != null)
            {
                ctx.ReviewReports.Remove(reprev);
                ctx.SaveChanges();
                return RedirectToAction("ManageReports", "Admin");
            }
            return HttpNotFound("No review found.");

        }

        [HttpDelete]
        public ActionResult ClearReviewReports(int id)
        {
            ReportReview reprev = ctx.ReviewReports.Find(id);
            Review r = reprev.Review;
            List<ReportReview> reportsList = ctx.ReviewReports.Where(m => m.ReviewId == reprev.ReviewId).ToList();
            if (r != null)
            {
                if (reportsList.Count() > 0)
                {
                    foreach (ReportReview aux in reportsList)
                    {
                        ctx.ReviewReports.Remove(aux);
                        r.NumberOfReports--;
                    }
                }

                ctx.SaveChanges();
                return RedirectToAction("ManageReports", "Admin");
            }
            return HttpNotFound("No review found.");

        }

        [HttpGet]
        public ActionResult ManageCommentReports()
        {
            List<ReportComment> reportedComments = ctx.ReviewComments.OrderByDescending(m => m.Comment.NumberOfReports).ToList();
            ViewBag.replist = reportedComments;
            return View();
        }

        public ActionResult ReportCommentDetails(int? id)
        {
            if (id.HasValue)
            {
                ReportComment rev = ctx.ReviewComments.Find(id);
                if (rev != null)
                {
                    return View(rev);
                }
                return HttpNotFound("Couldn't find the reported comment with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing report cpmment id parameter!");
        }

        [HttpDelete]
        public ActionResult DeleteComment(int id)
        {
            ReportComment reprev = ctx.ReviewComments.Find(id);
            Comment r = ctx.Comments.Find(reprev.CommentId);
            // var reportsList = ctx.ReviewReports.Where(m => m.ReportReviewId == reprev.ReportReviewId);
            if (r != null)
            {
                /*if(reportsList.Count() > 0)
                {
                    foreach (ReportReview aux in reportsList)
                    {
                        ctx.ReviewReports.Remove(aux);
                    }
                }*/

                ctx.Comments.Remove(r);
                ctx.SaveChanges();
                return RedirectToAction("ManageCommentReports", "Admin");
            }
            return HttpNotFound("No review found.");

        }

        [HttpDelete]
        public ActionResult ClearCommentReport(int id)
        {
            ReportComment reprev = ctx.ReviewComments.Find(id);
            if (reprev != null)
            {
                ctx.ReviewComments.Remove(reprev);
                ctx.SaveChanges();
                return RedirectToAction("ManageCommentReports", "Admin");
            }
            return HttpNotFound("No review found.");

        }

        [HttpDelete]
        public ActionResult ClearCommentReports(int id)
        {
            ReportComment reprev = ctx.ReviewComments.Find(id);
            Comment r = reprev.Comment;
            List<ReportComment> reportsList = ctx.ReviewComments.Where(m => m.CommentId == reprev.CommentId).ToList();
            if (r != null)
            {
                if (reportsList.Count() > 0)
                {
                    foreach (ReportComment aux in reportsList)
                    {
                        ctx.ReviewComments.Remove(aux);
                        r.NumberOfReports--;
                    }
                }

                ctx.SaveChanges();
                return RedirectToAction("ManageCommentReports", "Admin");
            }
            return HttpNotFound("No review found.");

        }

        [HttpGet]
        public ActionResult ManageMovies()
        {
            ViewBag.movies = ctx.Movies.ToList();
            return View();
        }

        [HttpGet]
        public ActionResult ManageActors()
        {
            ViewBag.actors = ctx.Actors.ToList();
            return View();
        }

        [HttpGet]
        public ActionResult ManageGenres()
        {
            ViewBag.genres = ctx.Genres.ToList();
            return View();
        }
    }
}