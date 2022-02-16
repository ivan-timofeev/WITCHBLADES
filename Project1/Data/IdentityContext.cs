using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Controllers
{
    public class MyIdentityContext : IdentityDbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public MyIdentityContext(DbContextOptions<MyIdentityContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}