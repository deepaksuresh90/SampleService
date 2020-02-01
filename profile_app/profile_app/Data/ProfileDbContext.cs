using Microsoft.EntityFrameworkCore;
using profile_app.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profile_app.Data
{
    public class ProfileDbContext : DbContext
    {

        public ProfileDbContext(DbContextOptions<ProfileDbContext> options) :base(options)
        {

        }
        public DbSet<profile> Profiles { get; set; }

    }
}
