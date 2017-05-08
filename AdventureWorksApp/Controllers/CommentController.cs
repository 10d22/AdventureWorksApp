using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdventureWorksApp.Models;
using System.Data.Entity;
using System.Globalization;

namespace AdventureWorksApp.Controllers
{
    public class CommentController : Controller
    {
        private IPhotoSharingContext context;

        //Constructors
        public CommentController()
        {
            context = new AdventureWorksDB();
        }

        public CommentController(IPhotoSharingContext Context)
        {
            context = Context;
        }

       // private AdventureWorksDB _adventureWorksDB = new AdventureWorksDB();

        // GET: A Partial View for displaying in the Photo Details view
        [ChildActionOnly] //This attribute means the action cannot be accessed from the brower's address bar
        public PartialViewResult _CommentsForPhoto(int PhotoId)
        {
            //The comments for a particular photo have been requested. Get those comments.
            var comments = from comment in context.Comments
                           where comment.PhotoID == PhotoId
                           select comment;
            //Save the PhotoID in the ViewBag because we'll need it in the view
            ViewBag.PhotoId = PhotoId;
            return PartialView(comments.ToList());
        }
        [HttpPost]
        public PartialViewResult _CommentsForPhoto(Comment comment, int PhotoId)
        {
            //The comment comes from the currently authenticated user
            // comment.Username = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(User.Identity.Name);
            comment.Username = "Sebastian";

            //Save the new comment
            context.Add(comment);
            context.SaveChanges();

            //Get the updated list of comments
            var comments = from c in context.Comments
                           where c.PhotoID == PhotoId
                           select c;
            //Save the PhotoID in the ViewBag because we'll need it in the view
            ViewBag.PhotoId = PhotoId;
            //Return the view with the new list of comments

            return PartialView(comments.ToList());
            //return PartialView("_CommentsForPhoto", comments.ToList());
        }

        //
        // GET: /Comment/CreateInline. A Partial View for displaying the create comment tool as a AJAX partial page update
        // [Authorize]
        public PartialViewResult _Create(int PhotoId)
        {
            //Create the new comment
            Comment newComment = new Comment();
            newComment.PhotoID = PhotoId;
            // newComment.Username = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(User.Identity.Name);
            newComment.Username = "Sebastian";

            ViewBag.PhotoID = PhotoId;
            return PartialView("_AddAComment");
        }

        // GET: /Comment/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Comment comment = context.FindCommentById(id);
            ViewBag.PhotoID = comment.PhotoID;
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //
        // POST: /Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = context.FindCommentById(id);
            context.Delete<Comment>(comment);
            context.SaveChanges();
            return RedirectToAction("Details", "Photo", new { id = comment.PhotoID });
        }
    }
}