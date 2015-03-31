using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PhotoSharingApplication.Models;

namespace PhotoSharingApplication.Controllers
{
    [ValueReporter]
    [HandleError(View = "Error")]
    public class PhotoController : Controller
    {

        private IPhotoSharingContext context;

        public PhotoController()
        {
            context = new PhotoSharingDB();
        }

        public PhotoController(IPhotoSharingContext context)
        {
            this.context = context;
        }

        // GET: Photo
        [OutputCache(Duration = 600, Location = OutputCacheLocation.Server, VaryByParam = "none")]
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Display(int id)
        {
            Photo photo = context.FindPhotoById(id);

            if (photo != null)
            {
                return View("Display", photo);
            }

            return HttpNotFound();
        }

        public ActionResult DisplayByTitle(string title)
        {
            Photo photo = context.FindPhotoByTitle(title);

            if (photo != null)
            {
                return View("Display", photo);
            }

            return HttpNotFound();
        }

        [Authorize]
        public ActionResult Create()
        {
            Photo photo = new Photo();
            photo.CreatedDate = DateTime.Now;

            return View("Create", photo);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Photo photo, HttpPostedFileBase image)
        {
            photo.CreatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    photo.ImageMimeType = image.ContentType;
                    photo.PhotoFile = new byte[image.ContentLength];

                    image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                    context.Add(photo);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View("Create", photo);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            Photo photo = context.FindPhotoById(id);

            if (photo != null)
            {
                return View("Delete", photo);
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = context.FindPhotoById(id);
            context.Delete(photo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [OutputCache(Duration = 600, Location = OutputCacheLocation.Server, VaryByParam = "id")]
        public FileContentResult GetImage(int id)
        {
            Photo photo = context.FindPhotoById(id);

            if (photo != null)
            {
                return File(photo.PhotoFile, photo.ImageMimeType);
            }

            return null;
        }

        [ChildActionOnly]
        public ActionResult _PhotoGallery(int number = 0)
        {
            List<Photo> photos = number != 0
                ? (from p in context.Photos
                   orderby p.CreatedDate descending
                   select p).Take(number).ToList()
                : context.Photos.ToList();
            return PartialView("_PhotoGallery", photos);
        }

        public ActionResult SlideShow()
        {
            return View("SlideShow", context.Photos.ToList());
        }

        public ActionResult FavoritesSlideShow()
        {
            List<Photo> favPhotos = new List<Photo>();
            List<int> favoriteIds = Session["Favorites"] as List<int>;

            if (favoriteIds == null)
            {
                favoriteIds = new List<int>();
            }

            Photo currentPhoto;

            foreach (int favId in favoriteIds)
            {
                currentPhoto = context.FindPhotoById(favId);
                if (currentPhoto != null)
                {
                    favPhotos.Add(currentPhoto);
                }
            }

            return View("SlideShow", favPhotos);
        }

        public ContentResult AddFavorite(int photoId)
        {
            List<int> favoriteIds = Session["Favorites"] as List<int>;


            if (favoriteIds == null)
            {
                favoriteIds = new List<int>();
            }

            favoriteIds.Add(photoId);

            Session["Favorites"] = favoriteIds;

            return Content("The picture has been added to your favorites", "text/plain", System.Text.Encoding.Default);
        }



    }
}