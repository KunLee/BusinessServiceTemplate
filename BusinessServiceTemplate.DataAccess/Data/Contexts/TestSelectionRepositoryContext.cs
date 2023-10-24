using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessServiceTemplate.DataAccess.Data.Contexts
{
    public class TestSelectionRepositoryContext : DbContext
    {
        public TestSelectionRepositoryContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<SC_Panel_Test> SC_Panel_Tests { get; set; }
        public DbSet<SC_Panel> SC_Panels { get; set; }
        public DbSet<SC_Currency> SC_Currencies { get; set; }
        public DbSet<SC_Test> SC_Tests { get; set; }
        public DbSet<SC_TestSelection> SC_TestSelections { get; set; }
        public DbSet<SC_MBS> SC_MBS { get; set; }
        public DbSet<SC_AMA> SC_AMA { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setup relationships between panels and tests
            modelBuilder.Entity<SC_Panel_Test>().Property(sc => sc.Visibility);

            modelBuilder.Entity<SC_Panel>()
                .HasQueryFilter(x => !x.IsDeleted)
                .HasMany(x => x.Tests)
                .WithMany(x => x.Panels)
                .UsingEntity<SC_Panel_Test>(
                    r => r.HasOne(x => x.Tests).WithMany().HasForeignKey(x => x.TestId),
                    l => l.HasOne(x => x.Panels).WithMany().HasForeignKey(x => x.PanelId)).HasData(new[] {
                        new { Id = 1, PanelId = 1, TestId = 1, Visibility = true },
                        new { Id = 2, PanelId = 1, TestId = 2, Visibility = false },
                        new { Id = 3, PanelId = 2, TestId = 1, Visibility = true },
                        new { Id = 4, PanelId = 1, TestId = 2, Visibility = false }
                });

            modelBuilder.Entity<SC_Test>().HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<SC_TestSelection>()
                .HasQueryFilter(x => !x.IsDeleted)
                .HasMany(e => e.Panels)
                .WithOne(e => e.TestSelection)
                .HasForeignKey(e => e.TestSelectionId)
                .IsRequired();

            modelBuilder.Entity<SC_Currency>()
               .HasMany(e => e.Panels)
               .WithOne(e => e.Currency)
               .HasForeignKey(e => e.CurrencyId)
               .IsRequired(false);

            modelBuilder.Entity<SC_MBS>()
               .HasMany(e => e.AustralianMedicalAssociations)
               .WithOne(e => e.MedibankSchedule)
               .HasForeignKey(e => e.MedicareItem)
               .IsRequired(false);

            // Seed mock data
            modelBuilder.Seed();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleSoftDelete();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void HandleSoftDelete() 
        {
            var entities = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);

            foreach (var entity in entities) 
            {
                switch (entity.Entity) 
                {
                    case SC_Test test:
                        entity.State = EntityState.Modified;
                        test.IsDeleted = true;
                        break;
                    case SC_Panel panel:
                        entity.State = EntityState.Modified;
                        panel.IsDeleted = true;
                        break;
                    case SC_TestSelection testSelection:
                        entity.State = EntityState.Modified;
                        testSelection.IsDeleted = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
