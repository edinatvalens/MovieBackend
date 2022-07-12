
using System.ComponentModel.DataAnnotations;


namespace VaBackend.Models
{
    public class MovieCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
