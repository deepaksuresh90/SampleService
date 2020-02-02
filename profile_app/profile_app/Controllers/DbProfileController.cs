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
    [Route("api/[controller]")]
    [ApiController]
    public class DbProfileController : ControllerBase
    {
        ProfileDbContext dbProfile;//= new ProfileDbContext();

        public DbProfileController(ProfileDbContext dbProf)
        {
            dbProfile = dbProf;
        }

        // GET: api/DbProfile
        [HttpGet( Name = "GetAppByIdV1")]
        public IEnumerable<profile> Get()
        {
            return dbProfile.Profiles;
        }

        // GET: api/DbProfile/5
        [HttpGet("{id}", Name = "Get")]
        public profile Get(int id)
        {
            return dbProfile.Profiles.Find(id);
        }

        // POST: api/DbProfile
        [HttpPost]
        public void Post([FromBody] profile profile_info)
        {

            dbProfile.Profiles.Add(profile_info);
            dbProfile.SaveChanges();

        }

        // PUT: api/DbProfile/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] profile profile_info)
        {
            var Entity = dbProfile.Profiles.Find(id);
            Entity.FullName = profile_info.FullName;
            Entity.Age = profile_info.Age;
            Entity.Dob = profile_info.Dob;
            Entity.City = profile_info.City;

            dbProfile.SaveChanges();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var Entity = dbProfile.Profiles.Find(id);
            dbProfile.Profiles.Remove(Entity);
            dbProfile.SaveChanges();
        }
    }
}
