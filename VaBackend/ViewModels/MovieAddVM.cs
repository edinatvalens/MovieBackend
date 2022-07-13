using Microsoft.AspNetCore.Http;
using System;

namespace VaBackend.ViewModels
{
    public class MovieAddVM
    {
        public string MovieName { get; set; }
        public string MovieUrl { get; set; }
        public string MovieLenght { get; set; }
        public string MovieDescription { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IFormFile MoviePicture { get; set; }
        public int Movie_Category_id { get; set; }
    }
}
