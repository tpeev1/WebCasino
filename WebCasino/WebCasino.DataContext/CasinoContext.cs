using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WebCasino.Entities;

namespace WebCasino.DataContext
{
    public class CasinoContext : IdentityDbContext<User>
    {
        public CasinoContext()
        {

        }

        public CasinoContext(DbContextOptions<CasinoContext> options) : base(options)
        {

        }
    }
}
