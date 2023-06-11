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
            },
            new SC_Panel{
                Id= 4,
                Name = "Panel Duplicate",
                Description = "Panel Duplicate Desc",
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
                Id= 5,
                Name = "Panel For Delete",
                Description = "Panel For Delete Desc",
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
            },
            new SC_Test {
                Id= 4,
                Description="Test Duplicate Desc",
                DescriptionVisibility= true,
                Name = "Test Duplicate",
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
                Id= 5,
                Description="Test For Delete Desc",
                DescriptionVisibility= true,
                Name = "Test For Delete",
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
            },
            new SC_TestSelection{
                Id= 4,
                Name = "Test Selection Duplicate",
                Description = "Test Selection Duplicate Desc",
                DescriptionVisibility= true,
                SpecialityId= 4
            },
            new SC_TestSelection{
                Id= 5,
                Name = "Test Selection For Delete",
                Description = "Test Selection For Delete Desc",
                DescriptionVisibility= true,
                SpecialityId= 4
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

        public static List<SC_Currency> CurrencyStore = new()
        {
            new SC_Currency{ 
                Id= 1,
                Name = "Currency 1",
                Country = "Country 1",
                Shortcode= "Shortcode 1",
                Symbol= "Symbol 1",
                Active= true
            },
            new SC_Currency{
                Id= 2,
                Name = "Currency 2",
                Country = "Country 2",
                Shortcode= "Shortcode 2",
                Symbol= "Symbol 2",
                Active= true
            },
            new SC_Currency{
                Id= 3,
                Name = "Currency 3",
                Country = "Country 3",
                Shortcode= "Shortcode 3",
                Symbol= "Symbol 3",
                Active= true
            },
            new SC_Currency{
                Id= 4,
                Name = "Currency Duplicate",
                Country = "Country Duplicate",
                Shortcode = "Shortcode Duplicate",
                Symbol = "Symbol Duplicate",
                Active = true
            },
            new SC_Currency{
                Id= 5,
                Name = "Currency For Delete",
                Country = "Country For Delete",
                Shortcode= "Shortcode For Delete",
                Symbol= "Symbol For Delete",
                Active = true
            }
        };
    }
}
