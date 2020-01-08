using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetProject
{
    public class Video
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ThumbnailUrl { get; set; }

        //public Video(string id, string title, string description, string thumbnailUrl)
        //{
        //    this.Id = id;
        //    this.Title = title;
        //    this.Description = description;
        //    this.ThumbnailUrl = thumbnailUrl;
        //}
    }
}
