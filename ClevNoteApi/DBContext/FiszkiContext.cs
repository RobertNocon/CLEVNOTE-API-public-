using Fiszki.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiszki.Controllers
{
    public class FiszkiContext : DbContext
    {
        //public DbSet<Progress> ProgressTable { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLogs> UsersLogs { get; set; }
        public DbSet<NotesModel> NotesTable { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer("Server=.;Database=TopFiszki;Trusted_Connection=True;");   //lokalna baza
            optionsBuilder.UseSqlServer("Server=mssql6.webio.pl,2401;Database=roberth_TopFiszki;Uid=roberth_webioDB;Password=HASŁO_TUTAJ;"); // webio
        }
    }
}
