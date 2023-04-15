using BusinessServiceTemplate.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceTemplate.DataAccess.Data.Contexts
{
    public class TestSelectionRepositoryContext : DbContext
    {
        public TestSelectionRepositoryContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<SC_Panel_Test>? SC_Panel_Tests { get; set; }
        public DbSet<SC_Panel>? SC_Panels { get; set; }
        public DbSet<SC_Test>? SC_Tests { get; set; }
        public DbSet<SC_TestSelection>? SC_TestSelections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Seed();
            modelBuilder.Entity<SC_Panel>()
                        .HasMany(e => e.Tests)
                        .WithMany(e => e.Panels)
                        .UsingEntity<SC_Panel_Test>();
        }
    }
}
