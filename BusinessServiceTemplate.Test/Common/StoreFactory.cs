using BusinessServiceTemplate.DataAccess.Entities;

namespace BusinessServiceTemplate.Test.Common
{
    public static class StoreFactory
    {
        public static List<SC_Panel> PanelStore = new()
        {
            new SC_Panel{
                Id= 1,
                Name = "Panel 1",
                Description = "Panel 1 Desc",
                DescriptionVisibility = true,
                Price = 10.01m,
                PriceVisibility= true,
                TestSelectionId= 1,
                Visibility= true,
                Tests = new List<SC_Test>{
                    new SC_Test {
                        Id= 1,
                        Description="Test 1 Desc",
                        DescriptionVisibility= true,
                        Name = "Test 1",
                        Panels = new List<SC_Panel>()
                    }
                }
            },
            new SC_Panel{
                Id= 2,
                Name = "Panel 2",
                Description = "Panel 2 Desc",
                DescriptionVisibility = true,
                Price = 20.01m,
                PriceVisibility= true,
                TestSelectionId= 2,
                Visibility= true,
                Tests = new List<SC_Test>{
                    new SC_Test {
                        Id= 1,
                        Description="Test 1 Desc",
                        DescriptionVisibility= true,
                        Name = "Test 1",
                        Panels = new List<SC_Panel>()
                    },
                    new SC_Test {
                        Id= 2,
                        Description="Test 2 Desc",
                        DescriptionVisibility= true,
                        Name = "Test 2",
                        Panels = new List<SC_Panel>()
                    },
                    new SC_Test {
                        Id= 3,
                        Description="Test 3 Desc",
                        DescriptionVisibility= true,
                        Name = "Test 3",
                        Panels = new List<SC_Panel>()
                    }
                }
            },
            new SC_Panel{
                Id= 3,
                Name = "Panel 3",
                Description = "Panel 3 Desc",
                DescriptionVisibility = true,
                Price = 30.01m,
                PriceVisibility= true,
                TestSelectionId= 3,
                Visibility= true,
                Tests = new List<SC_Test>{
                    new SC_Test {
                        Id= 3,
                        Description="Test 3 Desc",
                        DescriptionVisibility= true,
                        Name = "Test 3",
                        Panels = new List<SC_Panel>()
                    }
                }
            }
        };

        public static List<SC_Test> TestStore = new()
        {
            new SC_Test {
                Id= 1,
                Description="Test 1 Desc",
                DescriptionVisibility= true,
                Name = "Test 1",
                Panels = new List<SC_Panel>{
                    new SC_Panel{
                        Id= 1,
                        Name = "Panel 1",
                        Description = "Panel 1 Desc",
                        DescriptionVisibility = true,
                        Price = 10.01m,
                        PriceVisibility= true,
                        TestSelectionId= 1,
                        Visibility= true,
                        Tests = new List<SC_Test>()
                    },
                    new SC_Panel{
                        Id= 2,
                        Name = "Panel 2",
                        Description = "Panel 2 Desc",
                        DescriptionVisibility = true,
                        Price = 20.01m,
                        PriceVisibility= true,
                        TestSelectionId= 2,
                        Visibility= true,
                        Tests = new List<SC_Test>()
                    }
                }
            },
            new SC_Test {
                Id= 2,
                Description="Test 2 Desc",
                DescriptionVisibility= true,
                Name = "Test 2",
                Panels = new List<SC_Panel>{
                    new SC_Panel{
                        Id= 2,
                        Name = "Panel 2",
                        Description = "Panel 2 Desc",
                        DescriptionVisibility = true,
                        Price = 20.01m,
                        PriceVisibility= true,
                        TestSelectionId= 2,
                        Visibility= true,
                        Tests = new List<SC_Test>()
                    }
                }
            },
            new SC_Test {
                Id= 3,
                Description="Test 3 Desc",
                DescriptionVisibility= true,
                Name = "Test 3",
                Panels = new List<SC_Panel>{
                    new SC_Panel{
                        Id= 2,
                        Name = "Panel 2",
                        Description = "Panel 2 Desc",
                        DescriptionVisibility = true,
                        Price = 20.01m,
                        PriceVisibility= true,
                        TestSelectionId= 2,
                        Visibility= true,
                        Tests = new List<SC_Test>()
                    },
                    new SC_Panel{
                        Id= 3,
                        Name = "Panel 3",
                        Description = "Panel 3 Desc",
                        DescriptionVisibility = true,
                        Price = 30.01m,
                        PriceVisibility= true,
                        TestSelectionId= 3,
                        Visibility= true,
                        Tests = new List<SC_Test>()
                    }
                }
            }
        };

        public static List<SC_TestSelection> TestSelectionStore = new()
        {
            new SC_TestSelection{ 
                Id= 1,
                Name = "Test Selection 1",
                Description = "Test Selection 1 Desc",
                DescriptionVisibility= true,
                SpecialityId= 1
            },
            new SC_TestSelection{
                Id= 2,
                Name = "Test Selection 2",
                Description = "Test Selection 2 Desc",
                DescriptionVisibility= true,
                SpecialityId= 2
            },
            new SC_TestSelection{
                Id= 3,
                Name = "Test Selection 3",
                Description = "Test Selection 3 Desc",
                DescriptionVisibility= true,
                SpecialityId= 3
            }
        };

        public static List<SC_Panel_Test> PanelTestStore = new()
        {
            new SC_Panel_Test{ 
                PanelId= 1,
                TestId= 1,
                Visibility= true,
            },
            new SC_Panel_Test{
                PanelId= 2,
                TestId= 1,
                Visibility= false,
            },
            new SC_Panel_Test{
                PanelId= 2,
                TestId= 2,
                Visibility= true,
            },
            new SC_Panel_Test{
                PanelId= 2,
                TestId= 3,
                Visibility= true,
            },
            new SC_Panel_Test{
                PanelId= 3,
                TestId= 3,
                Visibility= false,
            },
        };
    }
}
