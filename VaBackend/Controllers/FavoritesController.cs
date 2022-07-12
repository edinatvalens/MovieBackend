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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_dbContext.Favorites.ToList());
        }

        [HttpPost]
        public bool Add([FromBody] FavoritesAddVM x)
        {
            Favorites check = null;
            check= _dbContext.Favorites.FirstOrDefault(a => a.MovieID == x.MovieID);
            if (check != null) return false;

            var newFav = new Favorites()
            {
                MovieID = x.MovieID
                
            };
            _dbContext.Add(newFav);
            _dbContext.SaveChanges();
            return true;
        }



        [HttpDelete]
        public ActionResult Delete( int idS)
        {
           
            Favorites fav = _dbContext.Favorites.FirstOrDefault(s=> s.MovieID==idS);

            if (fav == null)
                return BadRequest("Incorrect ID");

            _dbContext.Remove(fav);
            _dbContext.SaveChanges();
            return Ok(fav);
        }
    }
}
