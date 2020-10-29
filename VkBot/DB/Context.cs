using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VkBot.DB.Entity;

namespace VkBot.DB
{
    public class Context : DbContext
    {
        public DbSet<Messages> History { get; set; }
        public DbSet<Log> Logs { get; set; }

        public Context()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=ec2-54-217-234-157.eu-west-1.compute.amazonaws.com;" +
                "Port=5432;" +
                "Database=d4aqscs82qa70n;" +
                "Username=iqzmaawudtvkpg;" +
                "Password=9f65770be35785f22cae30aaab7bc253a455d2339e1b3dd38ef2b2179b432d19"
                );
        }
    }
    
}
