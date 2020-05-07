using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BKind.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BKind.ViewModels;

namespace BKind.Data
{
    public class BKindContext : IdentityDbContext<AppUser>
    {
        public BKindContext (DbContextOptions<BKindContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            //composed key of ReqHistory
            builder.Entity<ReqHistory>().HasKey(table => new
            {
                table.UserId, table.RequestId
            });

        }

        public DbSet<BKind.Models.Address> Address { get; set; }

        public DbSet<BKind.Models.Category> Category { get; set; }

        public DbSet<BKind.Models.Request> Request { get; set; }

        public DbSet<BKind.Models.Person> Person { get; set; }
        public DbSet<BKind.Models.ReqHistory> ReqHistory { get; set; }

    }
}
