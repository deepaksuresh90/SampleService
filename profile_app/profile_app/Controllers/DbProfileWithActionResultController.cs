using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using profile_app.Data;
using profile_app.Model;

namespace profile_app.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DbProfileWithActionResultController : ControllerBase
    {

        ProfileDbContext dbProfile;

        /*
         * Ok(message) ==> 200 ==> StatusCode(number)==>StatusCode(StatusCodes.Status200OK)
         * NotFound() ==>  404==>
         * BadRequest()==> 400--
         * */

        public DbProfileWithActionResultController(ProfileDbContext dbProf)
        {
            dbProfile = dbProf;
        }

        // GET: api/DbProfile
        [HttpGet]
        public IActionResult GetAll()
        {
            return  Ok(dbProfile.Profiles);
        }

        // GET: api/DbProfile/5
        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetProfile(int id)
        {
            var Entity = dbProfile.Profiles.Find(id);
            if (Entity == null)
            {
                return NotFound("Unable to find the Item");
                
            }
            else
            {

                return Ok(Entity);
            }
        }

        // POST: api/DbProfile
        [HttpPost]
        public IActionResult PostProfile([FromBody] profile profile_info)
        {

            dbProfile.Profiles.Add(profile_info);
            dbProfile.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);

        }

        // PUT: api/DbProfile/5
        [HttpPut("{id}")]
        public IActionResult PutProfile(int id, [FromBody] profile profile_info)
        {
            var Entity = dbProfile.Profiles.Find(id);

            if (Entity == null)
            {
                return BadRequest("Invaid Record");
            }
            else
            {
                Entity.FullName = profile_info.FullName;
                Entity.Age = profile_info.Age;
                Entity.Dob = profile_info.Dob;
                Entity.City = profile_info.City;

                dbProfile.SaveChanges();
                return Ok("Data Updated Successfully");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Entity = dbProfile.Profiles.Find(id);
            if (Entity == null)
            {
                return NotFound("Invaid Record");
            }
            else
            {
                dbProfile.Profiles.Remove(Entity);
                dbProfile.SaveChanges();
                return Ok("Data Deleted Successfully");
            }
        }
    }
}