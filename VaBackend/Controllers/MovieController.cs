using VaBackend.Data;
using VaBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using VaBackend.ViewModels;
using System.IO;
using System;

namespace VaBackend.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly IWebHostEnvironment webHostEnvironment;
        
        private IHttpContextAccessor httpContextAccessor;

        public MovieController(ApplicationDbContext dbContext, IWebHostEnvironment hostEnvironment, IHttpContextAccessor _httpContextAccessor)
        {

            this._dbContext = dbContext;
            webHostEnvironment = hostEnvironment;
            httpContextAccessor = _httpContextAccessor;

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_dbContext.Movie.Include(kp => kp.moviecategory)
               .FirstOrDefault(s => s.Id == id));
        }

        [HttpPost]
        public Movie Add([FromForm] MovieAddVM x)
        {


            var newMovie = new Movie()
            {
                MovieName=x.MovieName,
                MovieUrl = x.MovieUrl,
                MovieLenght = x.MovieLenght,
                AddedDate=x.AddedDate,
                ReleaseDate=x.ReleaseDate,
                Movie_Category_id = x.Movie_Category_id
            };

            if (x.MoviePicture != null)
            {
                string ekstenzija = Path.GetExtension(x.MoviePicture.FileName);

                var filename = $"{Guid.NewGuid()}{ekstenzija}";

                x.MoviePicture.CopyTo(new FileStream("wwwroot/uploads/" + filename, FileMode.Create));

                newMovie.MoviePicture = "https://localhost:44308/" + "uploads/" + filename;

            }

       
            _dbContext.Movie.Add(newMovie);
            _dbContext.SaveChanges();
            return newMovie;
        }
        [HttpPost("{id}")]
        public bool EditMovie(int id, [FromBody] MovieAddVM x )
        {

            Movie movie = _dbContext.Movie.FirstOrDefault(s => s.Id == id);

            if (movie == null)
                return true;

            movie.MovieName = x.MovieName;
            movie.MovieUrl = x.MovieUrl;
            movie.MovieLenght = x.MovieLenght;
            movie.AddedDate = x.AddedDate;
            movie.MovieDescription = x.MovieDescription;
            movie.ReleaseDate = x.ReleaseDate;
            movie.Movie_Category_id = x.Movie_Category_id;

            _dbContext.SaveChanges();
            return false;
        }

        [HttpGet]
        public ActionResult<List<Movie>> GetAll()
        {

            var data = _dbContext.Movie.Include(kp => kp.moviecategory).ToList();
            return data;

        }
        [HttpDelete("{id}")]

        public ActionResult Delete(int id)
        {

            Movie song = _dbContext.Movie.Find(id);

            if (song == null)
             return BadRequest("Incorrect ID");

            _dbContext.Remove(song);
            _dbContext.SaveChanges();
            return Ok(song);

        }
        [HttpGet]
        public IActionResult GetByName(string name)
        {
            return Ok(_dbContext.Movie.Include(kp => kp.moviecategory).FirstOrDefault(kp => kp.MovieName == name));
        }

        [HttpGet]
        public IActionResult GetByCategoryName(string name)
        {
            var id = _dbContext.MovieCategory.FirstOrDefault(x => x.Name == name).Id;

            return Ok(_dbContext.Movie.Include(kp => kp.moviecategory).Where(kp => kp.Movie_Category_id == id).ToList());
        }

        [HttpGet]
        public IActionResult Sort(string by)
        {
            if(by=="name")
                return Ok(_dbContext.Movie.Include(kp => kp.moviecategory).OrderByDescending(s => s.MovieName).ThenByDescending(s => s.MovieName).ToList());
            if (by == "year")
                return Ok(_dbContext.Movie.Include(kp => kp.moviecategory).OrderBy(x=>x.ReleaseDate).ToList());
            else return Ok(_dbContext.Movie.Include(kp => kp.moviecategory).Where(x=> x.MovieName.Contains(by)).ToList());
        }

        public class CategoryAddVM
        {
            public int id { get; set; }
            public string year { get; set; }
            public string name { get; set; }

        }



       
    }
}
