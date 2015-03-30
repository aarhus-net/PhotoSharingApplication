using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoSharingApplication.Models;

namespace PhotoSharingApplication.Controllers
{
    public class CommentController : Controller
    {
        private IPhotoSharingContext context;

        //Constructors
        public CommentController()
        {
            context = new PhotoSharingDB();
        }

        public CommentController(IPhotoSharingContext Context)
        {
            context = Context;
        }

        //
        // GET: /Comment/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Comment comment = context.FindCommentById(id);
            ViewBag.PhotoID = comment.PhotoId;
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //
        // POST: /Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = context.FindCommentById(id);
            context.Delete<Comment>(comment);
            context.SaveChanges();
            return RedirectToAction("Display", "Photo", new { id = comment.PhotoId });
        }

        [ChildActionOnly]
        public PartialViewResult _CommentsForPhoto(int photoId)
        {
            ViewBag.PhotoId = photoId;

            return PartialView("_CommentsForPhoto", context.FindCommentsForPhotoId(photoId));
        }

        [Authorize]
        public PartialViewResult _Create(int photoId)
        {
            Comment comment = new Comment();
            comment.PhotoId = photoId;

            ViewBag.PhotoId = photoId;

            return PartialView("_CreateAComment", comment);
        }

        [HttpPost]
        [Authorize]
        public PartialViewResult _CommentsForPhoto(Comment comment, int photoId )
        {
            context.Add(comment);
            context.SaveChanges();

            ViewBag.PhotoId = photoId;

            return PartialView("_CommentsForPhoto", context.FindCommentsForPhotoId(photoId));
        }

    }
}
