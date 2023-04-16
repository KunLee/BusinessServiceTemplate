using BusinessServiceTemplate.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessServiceTemplate.DataAccess.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SC_Test>()
                .HasData(new SC_Test
                {
                    Id = 1,
                    Name = "test1",
                    Description = "test1"
                },
                new SC_Test
                {
                    Id = 2,
                    Name = "test2",
                    Description = "test2"
                }
           );

            modelBuilder.Entity<SC_Panel>()
                .HasData(new SC_Panel
                {
                    Id = 1,
                    Name= "Panel1",
                    Description= "Panel1",
                    Price = new decimal(10.01)
                },
                new SC_Panel
                {
                    Id= 2,
                    Name = "Panel2",
                    Description = "Panel2",
                    Price = new decimal(20.01)
                }
           );
        }
    }
}
