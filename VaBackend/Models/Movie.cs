using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace VaBackend.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string MovieDescription { get; set; }
        public string MovieUrl { get; set; }
        public string MovieLenght { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime AddedDate { get; set; }
        public string MoviePicture { get; set; }
        [ForeignKey(nameof(moviecategory))]
        public int Movie_Category_id { get; set; }
        public MovieCategory moviecategory { get; set; }
       
    }
}
