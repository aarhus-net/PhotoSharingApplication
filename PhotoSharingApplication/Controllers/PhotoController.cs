﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoSharingApplication.Models;

namespace PhotoSharingApplication.Controllers
{
    public class PhotoController : Controller
    {

        private PhotoSharingDB context = new PhotoSharingDB();

        // GET: Photo
        public ActionResult Index()
        {
            return View("Index", context.Photos.ToList());
        }

        public ActionResult Display(int id)
        {
            Photo photo = context.Photos.Find(id);

            if (photo != null)
            {
                return View("Display", photo);
            }

            return HttpNotFound();
        }

        public ActionResult Create()
        {
            Photo photo = new Photo();
            photo.CreatedDate = DateTime.Now;

            return View("Create", photo);
        }

        [HttpPost]
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
                    context.Photos.Add(photo);
                    return RedirectToAction("Index");
                }
            }

            return View("Create", photo);
        }

        public ActionResult Delete(int id)
        {
            Photo photo = context.Photos.Find(id);

            if (photo != null)
            {
                return View("Delete", photo);
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = context.Photos.Find(id);
            context.Photos.Remove(photo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileContentResult GetImage(int id)
        {
            Photo photo = context.Photos.Find(id);

            if (photo != null)
            {
                return File(photo.PhotoFile, photo.ImageMimeType);
            }

            return null;
        }
    }
}