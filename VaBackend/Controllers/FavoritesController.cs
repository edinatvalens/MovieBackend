using VaBackend.Data;
using VaBackend.Models;
using Microsoft.AspNetCore.Mvc;
using VaBackend.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VaBackend.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public FavoritesController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Favorites.Include(x => x.movie).ToList());
        }

        [HttpPost("{id}")]
        public bool Add(int id)
        {
            Favorites check = null;
            check= _dbContext.Favorites.FirstOrDefault(a => a.MovieID == id);
            if (check != null) return false;

            var newFav = new Favorites()
            {
                MovieID = id

            };
            _dbContext.Add(newFav);
            _dbContext.SaveChanges();
            return true;
        }



        [HttpDelete]
        public bool Delete( int idS)
        {
           
            Favorites fav = _dbContext.Favorites.FirstOrDefault(s=> s.MovieID==idS);

            if (fav == null)
                return false;

            _dbContext.Remove(fav);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
