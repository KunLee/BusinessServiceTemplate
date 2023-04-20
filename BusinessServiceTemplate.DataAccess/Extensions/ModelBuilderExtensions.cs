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
                    Description = "test1",
                    DescriptionVisibility = true,
                },
                new SC_Test
                {
                    Id = 2,
                    Name = "test2",
                    Description = "test2",
                    DescriptionVisibility = true,
                }
           );

            modelBuilder.Entity<SC_Panel>()
                .HasData(new SC_Panel
                {
                    Id = 1,
                    Name = "Panel1",
                    Description= "Panel1",
                    DescriptionVisibility = true,
                    Price = new decimal(10.01),
                    PriceVisibility= true,
                    TestSelectionId = 2
                },
                new SC_Panel
                {
                    Id = 2,
                    Name = "Panel2",
                    Description = "Panel2",
                    DescriptionVisibility = true,
                    Price = new decimal(20.01),
                    PriceVisibility = true,
                    TestSelectionId= 1
                },
                new SC_Panel
                {
                    Id = 3,
                    Name = "Panel3",
                    Description = "Panel3",
                    DescriptionVisibility = true,
                    Price = new decimal(30.01),
                    PriceVisibility = true,
                    TestSelectionId = 1
                }
           );

            modelBuilder.Entity<SC_TestSelection>()
                .HasData(new SC_TestSelection
                {
                    Id = 1,
                    Name = "Test Selection 1",
                    Description = "Test Selection 1",
                    DescriptionVisibility = true,
                    SpecialityId = 1
                },
                new SC_TestSelection
                {
                    Id = 2,
                    Name = "Test Selection 2",
                    Description = "Test Selection 2",
                    DescriptionVisibility = false,
                    SpecialityId = 2
                }
           );
        }
    }
}
