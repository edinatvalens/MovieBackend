
using System.ComponentModel.DataAnnotations.Schema;


namespace VaBackend.Models
{

    public class Favorites
    {
        public int Id { get; set; }

        [ForeignKey(nameof(movie))]
        public int MovieID { get; set; }
        public Movie movie { get; set; }

    }
}
