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
    public class DiscussionForumsController : Controller
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();
        // GET: DiscussionForums
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ForumDetails(int? id)
        {
            if(id == null)
            {
                return HttpNotFound("Missing parameter");
            }
            DiscussionForum forum = ctx.Forums.Find(id);
            if(forum == null)
            {
                return HttpNotFound("Forum not found;");
            }
            ViewBag.currentusername = User.Identity.GetUserName();
            ViewBag.user = ctx.Users.Find(User.Identity.GetUserId());
            return View(forum);
        }


        [HttpPost]
        public ActionResult AddComment(int? id, string text)
        {
            if (id == null)
            {
                return HttpNotFound("Missing parameter");
            }
            DiscussionForum forum = ctx.Forums.Find(id);
            if (forum == null)
            {
                return HttpNotFound("Forum not found;");
            }
            if(text.Length == 0)
            {
                return HttpNotFound("Length must be more than 0.");
            }
            Comment commentRequest = new Comment
            {
                Text = text,
                NumberOfReports = 0,
                Author = ctx.Users.Find(User.Identity.GetUserId()),
                CreatedAt = DateTime.Now.ToString()
            };
            commentRequest.Forum = forum;
            forum.Comments.Add(commentRequest);
            ctx.Comments.Add(commentRequest);
            ctx.SaveChanges();
            return RedirectToAction("ForumDetails", "DiscussionForums", new { id });
        }

        [HttpGet]
        public ActionResult EditComment(int? id)
        {
            if (id == null)
            {
                return HttpNotFound("Missing parameter miau");
            }
            Comment comment = ctx.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound("Comment not found;");
            }
            return View(comment);
        }

        [HttpPost]
        public ActionResult EditComment(int? id, string new_text)
        {
            if (id == null)
            {
                return HttpNotFound("Missing parameter");
            }
            Comment comment = ctx.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound("Comment not found;");
            }
            comment.EditedAt = DateTime.Now.ToString();
            comment.Text = new_text;
            ctx.SaveChanges();
            return RedirectToAction("ForumDetails", "DiscussionForums", new { id = comment.Forum.DiscussionForumId });
        }

        [HttpDelete]
        public ActionResult DeleteComment(int? commentId)
        {
            if (commentId == null)
            {
                return HttpNotFound("Missing parameter");
            }
            Comment comment = ctx.Comments.Find(commentId);
            if (comment == null)
            {
                return HttpNotFound("Comment not found;");
            }
            int aux = comment.Forum.DiscussionForumId;
            ctx.Comments.Remove(comment);
            ctx.SaveChanges();
            return RedirectToAction("ForumDetails", "DiscussionForums", new { id = aux });
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
                Comment comment= ctx.Comments.Find(id);
                if (comment == null)
                {
                    return HttpNotFound("Review not found.");
                }
                else
                {
                    ReportComment reportComment = new ReportComment
                    {
                        Comment = comment,
                        Reason = "No reason specified.",
                        CommentId = comment.CommentId
                    };
                    comment.NumberOfReports++;
                    ctx.SaveChanges();
                    return View(reportComment);
                }
            }
        }

        [HttpPost]
        public ActionResult Report(ReportComment reportRequest)
        {
            ApplicationUser current_user = ctx.Users.Find(User.Identity.GetUserId());
            reportRequest.Comment = ctx.Comments.Find(reportRequest.CommentId);
            current_user.ReportedComments.Add(reportRequest.Comment);
            ctx.ReviewComments.Add(reportRequest);
            ctx.SaveChanges();
            return RedirectToAction("ForumDetails", "DiscussionForums", new { id = reportRequest.Comment.Forum.DiscussionForumId });
        }
    }
}