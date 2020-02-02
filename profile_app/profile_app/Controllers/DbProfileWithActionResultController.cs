using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using profile_app.Data;
using profile_app.Model;

namespace profile_app.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
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

        // GET: api/DbProfile/GetAll
        //Attribute routing
        [HttpGet("[action]")]

        /*In post man testing turn off send no-cache header, else this method will get hit for getting latest response*/
        [ResponseCache(Duration =60,Location =ResponseCacheLocation.Any)]
        public IActionResult GetAllBySort(string sort)
        {
            IQueryable<profile> p;
            switch (sort)
            {
                case "asc":
                   p = dbProfile.Profiles.OrderBy(x => x.FullName);
                    break;
                case "dec":
                    p = dbProfile.Profiles.OrderByDescending(x => x.FullName);
                    break;
                default:
                    p = dbProfile.Profiles;
                    break;
            }
            return  Ok(p);
        }

        // GET: api/DbProfile/GetAll
        //Attribute routing
        [HttpGet("[action]")]
        public IActionResult GetAllByPageNumber(int? pageNumber,int? NumberOfRecordsPerPage)
        {
            IEnumerable<profile> p = dbProfile.Profiles;

            var currentPageNumber = pageNumber ?? 1;
            var TotalNumberOfRecordsPerPage = NumberOfRecordsPerPage ?? 5;

            /*Skip and take Algorithm for paging*/

            return Ok(p.Skip((currentPageNumber - 1)* TotalNumberOfRecordsPerPage).Take(TotalNumberOfRecordsPerPage));
          
        }


        // GET: api/DbProfile/5
        // [HttpGet("{id}", Name = "GetById")]
        [HttpGet("[action]/{id}")]
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

        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        public IActionResult GetProfileBySearch(string name)
        {
            IEnumerable<profile> Entity = dbProfile.Profiles.Where(x => x.FullName.StartsWith(name));
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

            /*Get the Use ID  token from the authorization
             
             var userId= User.Clims.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier).Value
             */

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