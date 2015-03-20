using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoSharingApplication.Controllers;
using PhotoSharingApplication.Models;
using PhotoSharingTests.Doubles;

namespace PhotoSharingTests
{
    [TestClass]
    public class PhotoControllerTests
    {
        [TestMethod]
        public void Test_Index_Return_View()
        {
            PhotoController controller = new PhotoController(new FakePhotoSharingContext());

            var result = controller.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void Test_PhotoGallery_Model_Type()
        {
            FakePhotoSharingContext fc = new FakePhotoSharingContext();
            fc.Photos = new[]
            {
                new Photo() {Id = 1, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
                new Photo() {Id = 2, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
                new Photo() {Id = 3, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
                new Photo() {Id = 4, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"}
            }.AsQueryable();

            PhotoController controller = new PhotoController(fc);

            var result = controller._PhotoGallery() as PartialViewResult;

            Assert.AreEqual( typeof(List<Photo>), result.Model.GetType() );
        }

        [TestMethod]
        public void Test_GetImage_Return_Type()
        {
            FakePhotoSharingContext fc = new FakePhotoSharingContext();
            fc.Photos = new[]
            {
                new Photo() {Id = 1, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
                new Photo() {Id = 2, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
                new Photo() {Id = 3, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
                new Photo() {Id = 4, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"}
            }.AsQueryable();

            PhotoController controller = new PhotoController(fc);

            var result = controller.GetImage(1);

            Assert.AreEqual(typeof(FileContentResult), result.GetType());
        }

        [TestMethod]
        public void Test_PhotoGallery_No_Parameter()
        {
            FakePhotoSharingContext fc = new FakePhotoSharingContext();
            fc.Photos = new[]
            {
                new Photo() {Id = 1, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
                new Photo() {Id = 2, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
                new Photo() {Id = 3, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
                new Photo() {Id = 4, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"}
            }.AsQueryable();

            PhotoController controller = new PhotoController(fc);

            var result = controller._PhotoGallery() as PartialViewResult;

            Assert.AreEqual(4, ((IEnumerable<Photo>)result.Model).Count());
        }

        [TestMethod]
        public void Test_PhotoGallery_Int_Parameter()
        {
            FakePhotoSharingContext fc = new FakePhotoSharingContext();
            fc.Photos = new[]
            {
                new Photo() {Id = 1, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
                new Photo() {Id = 2, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
                new Photo() {Id = 3, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"},
                new Photo() {Id = 4, PhotoFile = new byte[1], ImageMimeType = "image/jpeg"}
            }.AsQueryable();

            PhotoController controller = new PhotoController(fc);

            var result = controller._PhotoGallery(3) as PartialViewResult;

            Assert.AreEqual(3, ((IEnumerable<Photo>)result.Model).Count());
        }

    }
}
