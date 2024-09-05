using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Introduction.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private static List<Club> _clubs = new List<Club>();   //ne public jer nijedna druga baza nece citati
                                                               //potrazi zasto _clubs

        [HttpGet]
        public IActionResult GetAllClubs()
        {
            if (_clubs.Count == 0)
                return NotFound();
            else
                return Ok(_clubs);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetClub(int id)
        {
            Club? club = _clubs.FirstOrDefault(x => x.Id == id);
           
            if (club == null)
                return NotFound();
            else
                return Ok(club);

        }

        [HttpGet]
        [Route("getByFilter")]
        public IActionResult GetClubByFilters(string? name = null, string? sport = null, DateOnly? dateofEstablishment = null, int? numberOfMembers = 0)
        {
            
            List<Club> filteredClubs = _clubs;
            
            filteredClubs = _clubs.Where(c => (string.IsNullOrEmpty(name) || string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase))
                                  && (string.IsNullOrEmpty(sport) || string.Equals(c.Sport, sport, StringComparison.OrdinalIgnoreCase))
                                  && (!dateofEstablishment.HasValue || c.DateOfEstablishment.Equals(dateofEstablishment))
                                  && (numberOfMembers==0 || c.NumberOfMembers == numberOfMembers))
                .ToList();

            return Ok(filteredClubs);
            
        }

        [HttpPost]
        public IActionResult PostClub(Club club)
        {
            if (_clubs.Any(c => c.Id == club.Id) || club.Id < 1)
                return BadRequest("Invalid request.");
            else 
                _clubs.Add(club);

            return Ok(_clubs);
        }

        [HttpPut]
        [Route("{id}")]

        public IActionResult UpdateClub(int id,[FromBody] Club club) //equals, ignore case
        {
            Club? clubToUpdate = _clubs.FirstOrDefault(c => c.Id == id);
            if (clubToUpdate == null)
                return NotFound();
            
            if(!string.IsNullOrEmpty(club.Name))
                clubToUpdate.Name = club.Name;

            if(!string.IsNullOrEmpty(club.Sport))
                clubToUpdate.Sport = club.Sport;

            if(club.DateOfEstablishment.HasValue)
                clubToUpdate.DateOfEstablishment = club.DateOfEstablishment;

            if(club.NumberOfMembers > 0)
                clubToUpdate.NumberOfMembers = club.NumberOfMembers;
                
            return Ok(_clubs);

        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteClub(int id)
        {
            Club? club = _clubs.FirstOrDefault(c => c.Id == id);
            if (club != null)
                _clubs.Remove(club);
            else 
                return NotFound();
            return NoContent();
        }


        

    }
}
