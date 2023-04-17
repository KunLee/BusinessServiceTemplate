﻿using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.DataAccess.Extensions;
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
            // Seed mock data
            modelBuilder.Seed();

            // Setup relationships between panels and tests
            modelBuilder.Entity<SC_Panel>()
                .HasMany(x => x.Tests)
                .WithMany(x => x.Panels)
                .UsingEntity<SC_Panel_Test>(
                    r => r.HasOne(x => x.Tests).WithMany().HasForeignKey(x => x.TestId),
                    l => l.HasOne(x => x.Panels).WithMany().HasForeignKey(x => x.PanelId))
                .HasData(new[] {
                        new { Id = 1, PanelId = 1, TestId = 1 },
                        new { Id = 2, PanelId = 1, TestId = 2 },
                        new { Id = 3, PanelId = 2, TestId = 1 },
                        new { Id = 4, PanelId = 1, TestId = 2 }
                    });
        }
    }
}