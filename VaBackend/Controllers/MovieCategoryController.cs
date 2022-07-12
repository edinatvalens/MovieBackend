using VaBackend.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using VaBackend.ViewModels;
using System.Collections.Generic;
using System.Linq;
using VaBackend.Models;

namespace VaBackend.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MovieCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public MovieCategoryController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_dbContext.MovieCategory.FirstOrDefault(s => s.Id == id));
        }

        [HttpPost]
        public MovieCategory Add([FromForm] MovieCategoryAddVM x)
        {

            var newCategory = new MovieCategory()
            {
                Name = x.Name,

            };


            _dbContext.Add(newCategory);
            _dbContext.SaveChanges();
            return newCategory;
        }

        [HttpPost("{id}")]
        public IActionResult Update(int id, [FromBody] MovieCategoryAddVM x)
        {

            MovieCategory katetgorija = _dbContext.MovieCategory.FirstOrDefault(s => s.Id == id);

            if (katetgorija == null)
                return BadRequest("Pogresan ID");

            katetgorija.Name = x.Name;

            _dbContext.SaveChanges();
            return Get(id);
        }
        [HttpGet]
        public ActionResult<List<MovieCategory>> GetAll(string Name)
        {

            var data = _dbContext.MovieCategory.Where(x => Name == null || (x.Name)
            .StartsWith(Name))
                .OrderByDescending(s => s.Name).ThenByDescending(s => s.Name)
                .AsQueryable();

            return data.Take(100).ToList();

        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            MovieCategory category = _dbContext.MovieCategory.Find(id);

            if (category == null)
                return BadRequest("Incorrect ID");

            _dbContext.Remove(category);
            _dbContext.SaveChanges();
            return Ok(category);
        }
    }
}
