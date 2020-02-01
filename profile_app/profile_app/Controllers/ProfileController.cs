using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using profile_app.Model;

namespace profile_app.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {

        internal static List<profile> Profiles = new List<profile>() {
            new profile() { Id = 0, FullName = "Diya Deepak", Age = 2, Dob = "30/07/2017", City = "Kerala" },
            new profile() { Id =1, FullName = "Anupama Deepak", Age = 29, Dob = "30/07/1990", City = "Kerala" },
            new profile() { Id =2, FullName = "Deepak", Age = 29, Dob = "22/05/1990", City = "Kerala" },
            };

        [HttpGet]
        public IEnumerable<profile> Get()
        {
            return Profiles;
        }

        [HttpPost]
        public string Post([FromBody] profile data)
        {
            Profiles.Add(data);
            return data + "  Added Successfully";
        }

        [HttpPut("{id}")]
        public string Put(int id,[FromBody] profile profile)
        {
            Profiles[id] = profile;
            return profile + " Updated Successfully";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            Profiles.RemoveAt(id);
            return id + " Deleted Successfully";
        }

    }
}