using Microsoft.EntityFrameworkCore;
using RssCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssCollection.Services
{
    public class DataBaseContext:DbContext
    {
        public DbSet<MailModel> MailModelSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MailModel>();
        }
        
    }
}
