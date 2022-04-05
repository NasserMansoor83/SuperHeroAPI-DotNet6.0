using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
            {
                new SuperHero { ID = 1,Name = "spider Man1", FirstName = "Peter1", LastName = "Paker1", Place = "New York1" },
                new SuperHero { ID = 2,Name = "spider Man2", FirstName = "Peter2", LastName = "Paker2", Place = "New York2" },
                new SuperHero { ID = 3,Name = "spider Man3", FirstName = "Peter3", LastName = "Paker3", Place = "New York3" }
            };
        private readonly DataContext _context;

        public SuperHeroController( DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {           
            return Ok(await _context.SuperHeroes.ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {
            var hero=await  _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not Found");
            return Ok(hero);

        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero p_hero)
        {
            var dbhero =await _context.SuperHeroes.FindAsync(p_hero.ID);

            if (dbhero == null) return BadRequest("hero no found");                        
            
            dbhero.LastName = p_hero.LastName;
            dbhero.Place = p_hero.Place;
            dbhero.Name = p_hero.Name;
            dbhero.FirstName = p_hero.FirstName;

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            if (id == 0) return BadRequest("Please enter id");

            var hero =await _context.SuperHeroes.FindAsync(id);
            if (hero == null) return  BadRequest("hero not found ");
              _context.SuperHeroes.Remove(hero);

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync() );

        }



    }
}
