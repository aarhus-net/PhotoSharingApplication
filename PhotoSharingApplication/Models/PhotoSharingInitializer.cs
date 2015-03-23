﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace PhotoSharingApplication.Models
{
    public class PhotoSharingInitializer : DropCreateDatabaseAlways<PhotoSharingDB>
    {
        protected override void Seed(PhotoSharingDB context)
        {
            List<Photo> photos = new List<Photo>()
            {
                new Photo()
                {
                    Title = "Sample Photo 1",
                    Description = "This is a test photo",
                    UserName = "NaokiSato",
                    PhotoFile = getFileBytes("\\Images\\flower.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Now
                },
                new Photo {
                    Title = "Sample Photo 2",
                    Description = "It's the bees knees!",
                    UserName = "Fred",
                    PhotoFile = getFileBytes("\\Images\\orchard.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today
                },
                new Photo {
                    Title = "Sample Photo 3",
                    Description = "I took this photo just before we started over my handle bars.",
                    UserName = "Sue",
                    PhotoFile = getFileBytes("\\Images\\path.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today
                },
                new Photo {
                    Title = "Sample Photo 4",
                    Description = "This is the forth sample photo in the Adventure Works photo application",
                    UserName = "JimCorbin",
                    PhotoFile = getFileBytes("\\Images\\fungi.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today.AddDays(-2)
                },
                new Photo {
                    Title = "Sample Photo 5",
                    Description = "This is the fifth sample photo in the Adventure Works photo application",
                    UserName = "JamieStark",
                    PhotoFile = getFileBytes("\\Images\\pinkflower.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today.AddDays(-1)
                }
            };
            photos.ForEach(s => context.Photos.Add(s));
            context.SaveChanges();

            //Create some comments
            var comments = new List<Comment>
            {
                new Comment {
                    PhotoId = 1,
                    UserName = "Bert",
                    Subject = "A Big Mountain",
                    Body = "That looks like a very high mountain you have climbed"
                },
                new Comment {
                    PhotoId = 1,
                    UserName = "Sue",
                    Subject = "So?",
                    Body = "I climbed a mountain that high before breakfast everyday"
                },
                new Comment {
                    PhotoId = 2,
                    UserName = "Fred",
                    Subject = "Jealous",
                    Body = "Wow, that new bike looks great!"
                }
            };
            comments.ForEach(s => context.Comments.Add(s));
            context.SaveChanges();
        }


        private byte[] getFileBytes(string path)
        {
            FileStream fileOnDisk = new FileStream(HttpRuntime.AppDomainAppPath + path, FileMode.Open);
            byte[] fileBytes;
            using (BinaryReader br = new BinaryReader(fileOnDisk))
            {
                fileBytes = br.ReadBytes((int) fileOnDisk.Length);
            }
            return fileBytes;
        }
    }
}