using FishyBuisness_3.Models;
using System;


namespace FishyBuisness_3.Data
{
    public class AppDbInitializer
    {

        public static async void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();



       

                //Fish
                if (!context.Fish.Any())
                {
                    context.Fish.AddRange(new List<Fish>()

                    {
                       new Fish
                        {
                            FishName = "Rainbow Trout",
                            FishDescription = "Colorful freshwater fish.",
                            Habitat = "Rivers and lakes",
                            Spieces = "Oncorhynchus mykiss",
                            Lenght = 30.5,
                            Weight = 2.5,
                            Price = 15.99,
                            EnvironmentId = 1
                        },
                        new Fish
                        {
                            FishName = "Blue Marlin",
                            FishDescription = "Fast-swimming oceanic fish.",
                            Habitat = "Open ocean",
                            Spieces = "Makaira nigricans",
                            Lenght = 350.0,
                            Weight = 200.0,
                            Price = 75.50,
                            EnvironmentId = 1
                        },
                        new Fish
                        {
                            FishName = "Goldfish",
                            FishDescription = "Small freshwater fish often kept as a pet.",
                            Habitat = "Ponds",
                            Spieces = "Carassius auratus",
                            Lenght = 15.0,
                            Weight = 0.2,
                            Price = 5.99,
                            EnvironmentId = 1
                        },
                        new Fish
                        {
                            FishName = "Tuna",
                            FishDescription = "Highly migratory oceanic fish.",
                            Habitat = "Open ocean",
                            Spieces = "Thunnini",
                            Lenght = 120.0,
                            Weight = 300.0,
                            Price = 30.25,
                            EnvironmentId = 1
                        },
                        new Fish
                        {
                            FishName = "Catfish",
                            FishDescription = "Freshwater fish with distinctive barbels.",
                            Habitat = "Rivers and lakes",
                            Spieces = "Siluriformes",
                            Lenght = 60.0,
                            Weight = 8.0,
                            Price = 12.99,
                            EnvironmentId = 1
                        },
                        new Fish
                        {
                            FishName = "Salmon",
                            FishDescription = "Anadromous fish that spawn in freshwater.",
                            Habitat = "Rivers and oceans",
                            Spieces = "Salmo",
                            Lenght = 90.0,
                            Weight = 5.5,
                            Price = 18.75,
                            EnvironmentId = 1
                        },
                        new Fish
                        {
                            FishName = "Clownfish",
                            FishDescription = "Small and colorful saltwater fish.",
                            Habitat = "Coral reefs",
                            Spieces = "Amphiprioninae",
                            Lenght = 8.0,
                            Weight = 0.1,
                            Price = 9.99,
                            EnvironmentId = 1
                        },
                        new Fish
                        {
                            FishName = "Barracuda",
                            FishDescription = "Predatory fish known for its sharp teeth.",
                            Habitat = "Tropical seas",
                            Spieces = "Sphyraena",
                            Lenght = 150.0,
                            Weight = 30.0,
                            Price = 45.50,
                            EnvironmentId = 1
                        },
                        new Fish
                        {
                            FishName = "Angelfish",
                            FishDescription = "Colorful freshwater fish with distinctive shape.",
                            Habitat = "Amazon River",
                            Spieces = "Pterophyllum",
                            Lenght = 15.0,
                            Weight = 0.3,
                            Price = 8.99,
                            EnvironmentId = 1
                        },
                        new Fish
                        {
                            FishName = "Swordfish",
                            FishDescription = "Large, highly migratory fish known for its long bill.",
                            Habitat = "Open ocean",
                            Spieces = "Xiphias gladius",
                            Lenght = 300.0,
                            Weight = 500.0,
                            Price = 55.25,
                            EnvironmentId = 1
                        },
                        new Fish
                        {
                            FishName = "Piranha",
                            FishDescription = "Predatory fish known for its sharp teeth and powerful bite.",
                            Habitat = "Amazon River",
                            Spieces = "Serrasalmidae",
                            Lenght = 30.0,
                            Weight = 1.0,
                            Price = 14.99,
                            EnvironmentId = 1
                        },
                        new Fish
                        {
                            FishName = "Guppy",
                            FishDescription = "Small and colorful freshwater fish popular in aquariums.",
                            Habitat = "Ponds",
                            Spieces = "Poecilia reticulata",
                            Lenght = 4.0,
                            Weight = 0.02,
                            Price = 3.50,
                            EnvironmentId = 1
                        },
                        new Fish
                        {
                            FishName = "Mahi-Mahi",
                            FishDescription = "Pelagic fish known for its vibrant colors.",
                            Habitat = "Open ocean",
                            Spieces = "Coryphaena hippurus",
                            Lenght = 150.0,
                            Weight = 25.0,
                            Price = 35.99,
                            EnvironmentId = 1
                        },
                        new Fish
                        {
                            FishName = "Sturgeon",
                            FishDescription = "Ancient fish known for its large size and caviar.",
                            Habitat = "Rivers and lakes",
                            Spieces = "Acipenseridae",
                            Lenght = 300.0,
                            Weight = 200.0,
                            Price = 80.50,
                            EnvironmentId = 1
                        },
                        new Fish
                        {
                            FishName = "Carp",
                            FishDescription = "Common freshwater fish often used in aquaculture.",
                            Habitat = "Ponds and lakes",
                            Spieces = "Cyprinus carpio",
                            Lenght = 60.0,
                            Weight = 10.0,
                            Price = 7.99,
                            EnvironmentId = 1
                        },
                        new Fish
                        {
                            FishName = "Perch",
                            FishDescription = "Freshwater fish known for its distinctive stripes.",
                            Habitat = "Lakes and rivers",
                            Spieces = "Perca fluviatilis",
                            Lenght = 40.0,
                            Weight = 1.5,
                            Price = 9.50,
                            EnvironmentId = 1
                        },
                        new Fish
                        {
                            FishName = "Red Snapper",
                            FishDescription = "Popular saltwater fish with a bright red color.",
                            Habitat = "Coral reefs",
                            Spieces = "Lutjanus campechanus",
                            Lenght = 50.0,
                            Weight = 3.0,
                            Price = 14.75,
                            EnvironmentId = 1
                        },
                        new Fish
                        {
                            FishName = "Trout",
                            FishDescription = "Coldwater fish with tasty flesh.",
                            Habitat = "Rivers and lakes",
                            Spieces = "Salmo trutta",
                            Lenght = 40.0,
                            Weight = 2.0,
                            Price = 11.99,
                            EnvironmentId = 1
                        },
                        new Fish
                        {
                            FishName = "Flathead Catfish",
                            FishDescription = "Large freshwater catfish with a flattened head.",
                            Habitat = "Rivers and lakes",
                            Spieces = "Pylodictis olivaris",
                            Lenght = 80.0,
                            Weight = 15.0,
                            Price = 22.25,
                            EnvironmentId = 1
                        }


                });
                    context.SaveChanges();
                }
            }

        }
    }
}
