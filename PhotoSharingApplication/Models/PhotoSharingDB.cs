﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhotoSharingApplication.Models
{
    public class PhotoSharingDB :DbContext, IPhotoSharingContext
    {
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }

        IQueryable<Photo> IPhotoSharingContext.Photos
        {
            get { return Photos; }
        }

        IQueryable<Comment> IPhotoSharingContext.Comments
        {
            get { return Comments; }
        }

        int IPhotoSharingContext.SaveChanges()
        {
            return SaveChanges();
        }

        T IPhotoSharingContext.Add<T>(T entity)
        {
            return Set<T>().Add(entity);
        }

        Photo IPhotoSharingContext.FindPhotoById(int id)
        {
            return Set<Photo>().Find(id);
        }

        Comment IPhotoSharingContext.FindCommentById(int id)
        {
            return Set<Comment>().Find(id);
        }

        T IPhotoSharingContext.Delete<T>(T entity)
        {
            return Set<T>().Remove(entity);
        }
    }
}