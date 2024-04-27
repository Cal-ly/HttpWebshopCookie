﻿using HttpWebshopCookie.Data.IndexTables;
using HttpWebshopCookie.Models;

namespace HttpWebshopCookie.Data.MockData;

public static class SeedData
{
    private static readonly PasswordHasher<ApplicationUser> hasher = new();
    private static readonly Random random = new();
    private const string Password = "Test.1234";
    private static readonly string[] RoleIds = [Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()];
    private static readonly string[] EmployeeIds = [Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()];

    public static List<string>? CompanyRepIdList;
    public static List<string>? CustomerIdList;
    public static List<string>? GuestIdList;
    public static List<string>? ProductIdList;
    public static List<string>? TagIdList;

    public static void SeedRoles(ModelBuilder modelBuilder)
    {
        string[] RoleNames = ["Admin", "Manager", "Staff", "Assistant", "CompanyRep", "Registered"];
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = RoleIds[0], Name = RoleNames[0], NormalizedName = RoleNames[0].ToUpper() },
            new IdentityRole { Id = RoleIds[1], Name = RoleNames[1], NormalizedName = RoleNames[1].ToUpper() },
            new IdentityRole { Id = RoleIds[2], Name = RoleNames[2], NormalizedName = RoleNames[2].ToUpper() },
            new IdentityRole { Id = RoleIds[3], Name = RoleNames[3], NormalizedName = RoleNames[3].ToUpper() },
            new IdentityRole { Id = RoleIds[4], Name = RoleNames[4], NormalizedName = RoleNames[4].ToUpper() },
            new IdentityRole { Id = RoleIds[5], Name = RoleNames[5], NormalizedName = RoleNames[5].ToUpper() }
        );
    }

    public static void SeedEmployees(ModelBuilder modelBuilder)
    {
        string[] EmployeeNames = ["Admin", "Manager", "Staff", "Assistant"];
        Employee adminUser = new()
        {
            Id = EmployeeIds[0],
            UserName = EmployeeNames[0],
            NormalizedUserName = EmployeeNames[0].ToUpper(),
            Email = $"{EmployeeNames[0]}@test.com",
            NormalizedEmail = $"{EmployeeNames[0]}@test.com".ToUpper(),
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null!, Password),
            SecurityStamp = string.Empty,
            JobTitle = "CEO",
            Salary = 100000,
            EnrollmentDate = DateTime.UtcNow,
            Address = new Address
            {
                Street = "Admin Street",
                City = "Admin City",
                PostalCode = "1234",
                Country = "Admin Country"
            }
        };
        Employee managerUser = new()
        {
            Id = EmployeeIds[1],
            UserName = EmployeeNames[1],
            NormalizedUserName = EmployeeNames[1].ToUpper(),
            Email = $"{EmployeeNames[1]}@test.com",
            NormalizedEmail = $"{EmployeeNames[1]}@test.com".ToUpper(),
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null!, Password),
            SecurityStamp = string.Empty,
            JobTitle = "Manager",
            Salary = 50000,
            EnrollmentDate = DateTime.UtcNow,
            Address = new Address
            {
                Street = "Manager Street",
                City = "Manager City",
                PostalCode = "1234",
                Country = "Manager Country"
            }
        };
        Employee staffUser = new()
        {
            Id = EmployeeIds[2],
            UserName = EmployeeNames[2],
            NormalizedUserName = EmployeeNames[2].ToUpper(),
            Email = $"{EmployeeNames[2]}@test.com",
            NormalizedEmail = $"{EmployeeNames[2]}@test.com".ToUpper(),
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null!, Password),
            SecurityStamp = string.Empty,
            JobTitle = "Staff",
            Salary = 30000,
            EnrollmentDate = DateTime.UtcNow,
            Address = new Address
            {
                Street = "Staff Street",
                City = "Staff City",
                PostalCode = "1234",
                Country = "Staff Country"
            }
        };
        Employee assistantUser = new()
        {
            Id = EmployeeIds[3],
            UserName = EmployeeNames[3],
            NormalizedUserName = EmployeeNames[3].ToUpper(),
            Email = $"{EmployeeNames[3]}@test.com",
            NormalizedEmail = $"{EmployeeNames[3]}@test.com".ToUpper(),
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null!, Password),
            SecurityStamp = string.Empty,
            JobTitle = "Assistant",
            Salary = 20000,
            EnrollmentDate = DateTime.UtcNow,
            Address = new Address
            {
                Street = "Assistant Street",
                City = "Assistant City",
                PostalCode = "1234",
                Country = "Assistant Country"
            }
        };

        modelBuilder.Entity<Employee>().HasData(adminUser, managerUser, staffUser, assistantUser);
        modelBuilder.Entity<Address>().HasData(adminUser.Address, managerUser.Address, staffUser.Address, assistantUser.Address);
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = EmployeeIds[0], RoleId = RoleIds[0] });
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = EmployeeIds[1], RoleId = RoleIds[1] });
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = EmployeeIds[2], RoleId = RoleIds[2] });
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = EmployeeIds[3], RoleId = RoleIds[3] });
    }

    public static void SeedCompanies(ModelBuilder modelBuilder)
    {
        CompanyRepIdList = [];
        List<Address> companyAddresses = new();
        List<Company> companies = new();
        List<Customer> companyReps = new();
        for(int i = 0; i < 6; i++)
        {
            string randomCVR = random.Next(10000000, 99999999).ToString();
            string randomPhone = random.Next(10000000, 99999999).ToString();
            string[] randomAddress = GenerateRandomAddress(random);
            Address companyAddress = new Address
            {
                //Id = Guid.NewGuid().ToString(),
                Resident = $"Company {i}",
                Street = $"{randomAddress[0]}",
                PostalCode = $"{randomAddress[1]}",
                City = $"{randomAddress[2]}",
            };
            companyAddresses.Add(companyAddress);
            Company company = new()
            {
                //Id = Guid.NewGuid().ToString(),
                CVR = randomCVR,
                Name = $"Company {i}",
                PhoneNumber = randomPhone,
                Address = companyAddress
            };
            companies.Add(company);
            string[] randomName = GenerateRandomName(random);
            Customer companyRep = new()
            {
                UserName = $"CompanyRep{i}",
                NormalizedUserName = $"CompanyRep{i}".ToUpper(),
                Email = $"companyrep{i}@rep.com",
                NormalizedEmail = $"companyrep{i}@test.com".ToUpper(),
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null!, Password),
                SecurityStamp = string.Empty,
                Title = "Owner",
                FirstName = randomName[0],
                LastName = randomName[1],
                PhoneNumber = randomPhone,
                Address = company.Address
            };
            CompanyRepIdList.Add(companyRep.Id);
        }
        modelBuilder.Entity<Address>().HasData([.. companyAddresses]);
        modelBuilder.Entity<Company>().HasData([.. companies]);
        modelBuilder.Entity<Customer>().HasData([.. companyReps]);
        foreach (var companyRep in companyReps)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = companyRep.Id, RoleId = RoleIds[4] });
        }
    }

    public static void SeedCustomers(ModelBuilder modelBuilder)
    {
        CustomerIdList = [];
        List<Address> customerAddresses = [];
        List<Customer> customers = [];
        for (int i = 0; i < 10; i++)
        {
            string[] randomAddress = GenerateRandomAddress(random);
            string randomPhone = random.Next(10000000, 99999999).ToString();
            Address customerAddress = new()
            {
                Resident = $"Customer {i}",
                Street = $"{randomAddress[0]}",
                PostalCode = $"{randomAddress[1]}",
                City = $"{randomAddress[2]}",
            };
            customerAddresses.Add(customerAddress);
            string[] randomName = GenerateRandomName(random);
            Customer customer = new()
            {
                UserName = $"Customer{i}",
                NormalizedUserName = $"Customer{i}".ToUpper(),
                Email = $"customer{i}@test.com",
                NormalizedEmail = $"customer{i}@test.com".ToUpper(),
                PhoneNumber = randomPhone,
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null!, Password),
                SecurityStamp = string.Empty,
                Title = "Mr",
                FirstName = randomName[0],
                LastName = randomName[1],
                Address = customerAddress
            };
            customers.Add(customer);
            CustomerIdList.Add(customer.Id);
        }

        modelBuilder.Entity<Address>().HasData([.. customerAddresses]);
        modelBuilder.Entity<Customer>().HasData([.. customers]);
        foreach (var customer in customers)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = customer.Id, RoleId = RoleIds[5] });
        }
    }

    public static void SeedGuests(ModelBuilder modelBuilder)
    {
        GuestIdList = [];
        string[] names = GenerateRandomName(random);
        List<Address> guestAddresses = [];
        List<Guest> guests = [];
        for(int i = 0; i < 10; i++)
        {
            string[] randomAddress = GenerateRandomAddress(random);
            string randomPhone = random.Next(10000000, 99999999).ToString();
            Address guestAddress = new()
            {
                Resident = names[0] + names[1],
                Street = $"{randomAddress[0]}",
                PostalCode = $"{randomAddress[1]}",
                City = $"{randomAddress[2]}",
            };
            guestAddresses.Add(guestAddress);
            Guest guest = new()
            {
                FirstName = names[0],
                LastName = names[1],
                Email = $"{names[0]}{names[1]}{random.Next(1, 99)}@test.com",
                PhoneNumber = randomPhone,
                Address = guestAddress
            };
            guests.Add(guest);
            GuestIdList.Add(guest.Id);
        }
        modelBuilder.Entity<Address>().HasData([.. guestAddresses]);
        modelBuilder.Entity<Guest>().HasData([.. guests]);
    }

    public static void SeedTags(ModelBuilder modelBuilder)
    {
        TagIdList = [];
        List<Tag> tags = new();

        Dictionary<string, List<(string Category, List<string> SubCategories)>> categoriesByOccasion = new Dictionary<string, List<(string, List<string>)>>
        {
            ["Begravelse"] = new List<(string, List<string>)>
            {
                ("Bårebukket", new List<string> { "Traditionel", "Moderne", "Fredfyldt", "Naturlig" }), // index 0, 1, 2, 3
                ("Kondolencebuket", new List<string> { "Lille", "Mellem", "Stor", "Pastel", "Farverig" }), // index 4, 5, 6, 7, 8
                ("Begravelsesdekoration", new List<string> { "Traditionel", "Moderne", "Monokrom" }), // index 9, 10, 11
                ("Bisættelse", new List<string> { "Traditionel", "Moderne", "Kristen", "Sekulær" }), // index 12, 13, 14, 15
                ("Gravdekoration", new List<string> { "Traditionel", "Moderne", "Elegant", "Farverig", "Pastel" }), // index 16, 17, 18, 19, 20
                ("Blomsterkranse", new List<string> { "Traditionel", "Moderne", "Elegant", "Farverig", "Pastel" }), // index 21, 22, 23, 24, 25
                ("Kistepynt", new List<string> { "Traditionel", "Moderne", "Roser", "Liljer" }), // index 26, 27, 28, 29
                ("Urnepynt", new List<string> { "Traditionel", "Moderne", "Bæredygtig" }) // index 30, 31, 32
            },
            ["Bryllup"] = new List<(string, List<string>)>
            {
                ("Brudebuket", new List<string> {"Klassisk", "Romantisk", "Boheme", "Vintage"}), // index 33, 34, 35, 36
                ("Brudepigebuket", new List<string> {"Simpel", "Moderne", "Farverig", "Minimalistisk"}), // index 37, 38, 39, 40
                ("Bryllupsdekoration", new List<string> {"Traditionel", "Moderne", "Elegant", "Farverig", "Pastel"}), // index 41, 42, 43, 44, 45
                ("Bryllupsgave", new List<string> {"Traditionel", "Moderne", "Elegant", "Farverig", "Pastel"}), // index 46, 47, 48, 49, 50
                ("Bryllupsdag", new List<string> {"Traditionel", "Moderne"}) // index 51, 52

            },
            ["Konfirmation"] = new List<(string, List<string>)>
            {
                ("Konfirmationsbuket", new List<string> {"Sporty", "Musikalsk", "Natur", "Teknologi"}), // index 53, 54, 55, 56
                ("Konfirmationsgave", new List<string> {"Praktisk", "Sjov", "Uddannelsesrig", "Kreativ"}), // index 57, 58, 59, 60
                ("Konfirmationspynt", new List<string> {"Traditionel", "Moderne"}) // index 61, 62
            },
            ["Særlige dage"] = new List<(string, List<string>)>
            {
                ("Fødselsdag", new List<string> {"Ham", "Hende", "Barn"}), // index 63, 64, 65
                ("Nyfødt", new List<string> {"Dreng", "Pige", "Tvillinger"}), // index 66, 67, 68
                ("Årsdag", new List<string> {"Kæreste", "Ægtefælle"}), // index 69, 70
                ("Valentinsdag", new List<string> {"Romantisk", "Sød", "Passioneret", "Elegant"}), // index 71, 72, 73, 74
                ("Mors dag", new List<string> {"Klassisk Mor", "Moderne Mor", "Haveelsker", "Kunstelsker"}), // index 75, 76, 77, 78
                ("Fars dag", new List<string> {"Sporty Far", "Forretningsmand", "Outdoor Far", "Hobbykok"}) // index 79, 80, 81, 82
            },
            ["Kærlig tanke"] = new List<(string, List<string>)>
            {
                ("Tænker på dig", new List<string> {"Lille", "Mellem", "Stor"}), // index 83, 84, 85
                ("God bedring", new List<string> {"Lille", "Mellem", "Stor"}), // index 86, 87, 88
                ("Opmuntring", new List<string> {"Støttende", "Håbefuld", "Optimistisk", "Beroligende"}), // index 89, 90, 91, 92
                ("Held og lykke", new List<string> {"Lille", "Mellem", "Stor"}), // index 93, 94, 95
                ("Tak for", new List<string> {"Lille", "Mellem", "Stor"}), // index 96, 97, 98
                ("Undskyld", new List<string> {"Lille", "Mellem", "Stor"}), // index 99, 100, 101
                ("Romantik", new List<string> {"Passioneret", "Sød", "Intim", "Grand Amor"}) // index 102, 103, 104, 105
            },
            ["Gave"] = new List<(string, List<string>)>
            {
                ("Barselsgaver", new List<string> {"Lille", "Mellem", "Stor"}), // index 106, 107, 108
                ("Gaveideer til hende", new List<string> {"Fashionista", "Fitness", "Bogelsker"}), // index 109, 110, 111
                ("Gaveideer til ham", new List<string> {"Teknologi", "Sport", "Musik", "Handyman"}), // index 112, 113, 114, 115
                ("Indflyttergave", new List<string> {"Lille", "Mellem", "Stor"}), // index 116, 117, 118
                ("Værtindegave", new List<string> {"Lille", "Mellem", "Stor"}) // index 119, 120, 121
            },
            ["Hjemmet"] = new List<(string, List<string>)>
            {
                ("Gulvplante", new List<string> {"Lille", "Mellem", "Stor", "Lav vedligeholdelse", "Luftrensende", "Skygge-tolerant", "Farverig"}), // index 122, 123, 124, 125, 126, 127, 128
                ("Stueplante", new List<string> {"Lille", "Mellem", "Stor", "Lav vedligeholdelse", "Luftrensende", "Skygge-tolerant", "Farverig"}), // index 129, 130, 131, 132, 133, 134, 135
                ("Udendørsplante", new List<string> {"Lille", "Mellem", "Stor"}) // index 136, 137, 138
            },
            ["Arbejde"] = new List<(string, List<string>)>
            {
                ("Første arbejdsdag", new List<string> {"Lille", "Mellem", "Stor"}), // index 139, 140, 141
                ("Fødselsdag", new List<string> {"Lille", "Mellem", "Stor"}), // index 142, 143, 144
                ("Jubilæum", new List<string> {"Lille", "Mellem", "Stor"}), // index 145, 146, 147
                ("Pension", new List<string> {"Lille", "Mellem", "Stor"}) // index 148, 149, 150
            },
            ["Sæson"] = new List<(string, List<string>)>
            {
                ("Forår", new List<string> {"Lille", "Mellem", "Stor"}), // index 151, 152, 153
                ("Sommer", new List<string> {"Lille", "Mellem", "Stor"}), // index 154, 155, 156
                ("Efterår", new List<string> {"Lille", "Mellem", "Stor"}), // index 157, 158, 159
                ("Vinter", new List<string> {"Lille", "Mellem", "Stor"}) // index 160, 161, 162
            },
            ["Blomster"] = new List<(string, List<string>)>
            {
                ("Roser", new List<string> {"Røde", "Gule", "Hvide"}), // index 163, 164, 165
                ("Liljer", new List<string> {"Hvide", "Gule", "Rosa"}), // index 166, 167, 168
                ("Orkideer", new List<string> {"Hvide", "Rosa", "Lilla"}), // index 169, 170, 171
                ("Tulipaner", new List<string> {"Røde", "Gule", "Hvide"}), // index 172, 173, 174
                ("Gerbera", new List<string> {"Røde", "Gule", "Hvide"}), // index 175, 176, 177
                ("Solsikker", new List<string> {"Store", "Små", "Mellem"}), // index 178, 179, 180
                ("Hortensia", new List<string> {"Blå", "Rosa", "Hvide"}), // index 181, 182, 183
                ("Valmuer", new List<string> {"Røde", "Gule", "Hvide"}), // index 184, 185, 186
                ("Krysantemum", new List<string> {"Røde", "Gule", "Hvide"}), // index 187, 188, 189
                ("Peon", new List<string> {"Røde", "Gule", "Hvide"}), // index 190, 191, 192
                ("Anemone", new List<string> {"Røde", "Gule", "Hvide"}), // index 193, 194, 195
                ("Amaryllis", new List<string> {"Røde", "Gule", "Hvide"}), // index 196, 197, 198
                ("Freesia", new List<string> {"Røde", "Gule", "Hvide"}), // index 199, 200, 201
                ("Lavendel", new List<string> {"Blå", "Rosa", "Hvide"}), // index 202, 203, 204
                ("Kornblomst", new List<string> {"Blå", "Rosa", "Hvide"}) // index 205, 206
            }
        };
        foreach (var occasion in categoriesByOccasion)
        {
            foreach (var category in occasion.Value)
            {
                foreach (var subCategory in category.SubCategories)
                {
                    var tag = new Tag
                    {
                        Occasion = occasion.Key,
                        Category = category.Category,
                        SubCategory = subCategory
                    };
                    TagIdList.Add(tag.Id);
                }
            }
        }
        modelBuilder.Entity<Tag>().HasData([.. tags]);
    }

    public static void SeedProducts(ModelBuilder modelBuilder)
    {
        SeedTags(modelBuilder);
        ProductIdList = [];
        List<Product> products = new List<Product>
        {
            new Product
            {
                Name = "Ærefuld Farvel",
                Description = "En dybfølt og respektfuld buket til at ære en elsket. Traditionelle hvide liljer og roser.",
                Price = 350,
                ImageUrl = "https://via.placeholder.com/150",
                IsDeleted = false,
                UpdatedAt = DateTime.Now
            },
            new Product
            {
                Name = "Pastel Kondolence",
                Description = "Beroligende pastelfarvede blomster til at udtrykke sympati.",
                Price = 300,
                ImageUrl = "https://via.placeholder.com/150",
                IsDeleted = false,
                UpdatedAt = DateTime.Now
            },
            new Product
            {
                Name = "Sekulær Bisættelse",
                Description = "Diskret og enkel buket til en sekulær afskedsceremoni.",
                Price = 350,
                ImageUrl = "https://via.placeholder.com/150",
                IsDeleted = false,
                UpdatedAt = DateTime.Now
            },
            new Product
            {
                Name = "Romantisk Gestus",
                Description = "En passioneret rød rosenbuket, der udtrykker dyb kærlighed og hengivenhed.",
                Price = 450,
                ImageUrl = "https://via.placeholder.com/150",
                IsDeleted = false,
                UpdatedAt = DateTime.Now
            },
            new Product
            {
                Name = "Moderne Elegance",
                Description = "En stilfuld og moderne arrangement med orkideer og eksotiske blomster.",
                Price = 500,
                ImageUrl = "https://via.placeholder.com/150",
                IsDeleted = false,
                UpdatedAt = DateTime.Now
            },
            new Product
            {
                Name = "Fødselsdagsglæde",
                Description = "Farverig og festlig buket der lyser op i enhver fødselsdagsfest.",
                Price = 300,
                ImageUrl = "https://via.placeholder.com/150",
                IsDeleted = false,
                UpdatedAt = DateTime.Now
            },
            new Product
            {
                Name = "Nyfødte Lykønskninger",
                Description = "Sød og blid buket til at fejre ankomsten af et nyt barn.",
                Price = 350,
                ImageUrl = "https://via.placeholder.com/150",
                IsDeleted = false,
                UpdatedAt = DateTime.Now
            },
            new Product
            {
                Name = "Stilfuld Forretningsgave",
                Description = "Eksklusiv plante der tilføjer en professionel atmosfære til ethvert kontor.",
                Price = 600,
                ImageUrl = "https://via.placeholder.com/150",
                IsDeleted = false,
                UpdatedAt = DateTime.Now
            },
            new Product
            {
                Name = "Til Konfirmanten",
                Description = "Friske og unge toner i en buket, der passer perfekt til en stor dag.",
                Price = 250,
                ImageUrl = "https://via.placeholder.com/150",
                IsDeleted = false,
                UpdatedAt = DateTime.Now
            },
            new Product
            {
                Name = "Efterårsharmoni",
                Description = "Varm og indbydende buket der afspejler efterårets farver.",
                Price = 375,
                ImageUrl = "https://via.placeholder.com/150",
                IsDeleted = false,
                UpdatedAt = DateTime.Now
            },
            new Product
            {
                Name = "Vintermagi",
                Description = "En kold og krystalklar buket, der bringer vinterens essens indendørs.",
                Price = 400,
                ImageUrl = "https://via.placeholder.com/150",
                IsDeleted = false,
                UpdatedAt = DateTime.Now
            },
            new Product
            {
                Name = "Livlige Sommerblomster",
                Description = "En sprudlende og energisk buket der fanger sommerens ånd.",
                Price = 325,
                ImageUrl = "https://via.placeholder.com/150",
                IsDeleted = false,
                UpdatedAt = DateTime.Now
            },
            new Product
            {
                Name = "Boheme Brudebuket",
                Description = "Unik og kunstnerisk buket designet til den moderne brud.",
                Price = 800,
                ImageUrl = "https://via.placeholder.com/150",
                IsDeleted = false,
                UpdatedAt = DateTime.Now
            },
            new Product
            {
                Name = "Klassisk Mors Dags Buket",
                Description = "Traditionel og tidløs buket for at hylde alle mødre.",
                Price = 275,
                ImageUrl = "https://via.placeholder.com/150",
                IsDeleted = false,
                UpdatedAt = DateTime.Now
            },
            new Product
            {
                Name = "Haveelskerens Favorit",
                Description = "Diverse og naturlige blomster valgt til en ægte haveelsker.",
                Price = 400,
                ImageUrl = "https://via.placeholder.com/150",
                IsDeleted = false,
                UpdatedAt = DateTime.Now
            }
        };

        foreach (Product product in products)
        {
            ProductIdList.Add(product.Id);
            modelBuilder.Entity<Product>().HasData(product);
        }

        if (TagIdList is null)
            return;

        modelBuilder.Entity<ProductTag>().HasData(
            new ProductTag { ProductId = ProductIdList[0], TagId = TagIdList[0] },
            new ProductTag { ProductId = ProductIdList[0], TagId = TagIdList[165] },
            new ProductTag { ProductId = ProductIdList[0], TagId = TagIdList[166] },
            new ProductTag { ProductId = ProductIdList[1], TagId = TagIdList[4] },
            new ProductTag { ProductId = ProductIdList[1], TagId = TagIdList[7] },
            new ProductTag { ProductId = ProductIdList[2], TagId = TagIdList[15] },
            new ProductTag { ProductId = ProductIdList[3], TagId = TagIdList[102] },
            new ProductTag { ProductId = ProductIdList[3], TagId = TagIdList[163] },
            new ProductTag { ProductId = ProductIdList[4], TagId = TagIdList[124] },
            new ProductTag { ProductId = ProductIdList[4], TagId = TagIdList[128] },
            new ProductTag { ProductId = ProductIdList[4], TagId = TagIdList[169] },
            new ProductTag { ProductId = ProductIdList[5], TagId = TagIdList[63] },
            new ProductTag { ProductId = ProductIdList[6], TagId = TagIdList[66] },
            new ProductTag { ProductId = ProductIdList[7], TagId = TagIdList[114] },
            new ProductTag { ProductId = ProductIdList[8], TagId = TagIdList[55] },
            new ProductTag { ProductId = ProductIdList[9], TagId = TagIdList[158] },
            new ProductTag { ProductId = ProductIdList[10], TagId = TagIdList[161] },
            new ProductTag { ProductId = ProductIdList[11], TagId = TagIdList[155] },
            new ProductTag { ProductId = ProductIdList[12], TagId = TagIdList[36] },
            new ProductTag { ProductId = ProductIdList[13], TagId = TagIdList[75] },
            new ProductTag { ProductId = ProductIdList[13], TagId = TagIdList[77] },
            new ProductTag { ProductId = ProductIdList[14], TagId = TagIdList[126] }
        );
    }

    public static string[] GenerateRandomName(Random random)
    {
        string[] firstNames = ["Hans", "Jens", "Lone", "John", "Emma", "Michael", "Sophia", "William", "Olivia", "Kristian"];
        int indexFirst = random.Next(firstNames.Length);

        string[] lastNames = ["Hansen", "Jensen", "Johnson", "Gormsen", "Michaelson", "Kristiansdottir", "Wilson", "Olivera"];
        int indexLast = random.Next(lastNames.Length);

        return [firstNames[indexFirst], lastNames[indexLast]];
    }

    public static string[] GenerateRandomAddress(Random random)
    {
        string[] streets = ["Hovedgade", "Bakkevej", "Skovvej", "Strandvej", "Søndergade", "Nørregade", "Vestergade", "Østergade"];
        int indexStreet = random.Next(streets.Length);

        string streetCombine;
        string streetNumber = random.Next(1, 99).ToString();
        if (random.Next(1, 3) == 1)
        {
            string streetLetter = Convert.ToChar(random.Next(65, 91)).ToString();
            string streetFloor = random.Next(1, 5).ToString();
            string streetSide = random.Next(1, 3) == 1 ? "tv" : "th";
            streetCombine = $"{streetNumber}{streetLetter}, {streetFloor}{streetSide}";
        }
        else
        {
            streetCombine = streetNumber;
        }

        string streetLongAddress = $"{streets[indexStreet]} {streetCombine}";

        string[] postalCodes = ["1000", "2000", "3000", "4000", "5000", "6000", "7000", "8000"];
        int indexPostal = random.Next(postalCodes.Length);

        Dictionary<string, string> cities = new()
        {
            ["1000"] = "København",
            ["2000"] = "Frederiksberg",
            ["3000"] = "Helsingør",
            ["4000"] = "Roskilde",
            ["5000"] = "Odense",
            ["6000"] = "Kolding",
            ["7000"] = "Esbjerg",
            ["8000"] = "Aarhus",
            ["9000"] = "Aalborg"
        };

        return [streetLongAddress, postalCodes[indexPostal], cities[postalCodes[indexPostal]]];
    }
}