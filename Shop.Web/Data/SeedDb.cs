namespace Shop.Web.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Shop.Web.Helpers;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");
            if (!this.context.Countries.Any())
            {
                var cities = new List<City>();
                cities.Add(new City { Name = "Medellín" });
                cities.Add(new City { Name = "Bogotá" });
                cities.Add(new City { Name = "Calí" });
                
                this.context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Colombia"
                });
                var citiesb = new List<City>();
                citiesb.Add(new City { Name = "Belo Horizonte" });
                citiesb.Add(new City { Name = "Sao Paulo" });
                citiesb.Add(new City { Name = "Rio de Janeiro" });

                this.context.Countries.Add(new Country
                {
                    Cities = citiesb,
                    Name = "Brasil"
                });

                await this.context.SaveChangesAsync();
            }


            // Add user
            var user = await this.userHelper.GetUserByEmailAsync("gilcielphp@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Gilciel",
                    LastName = "Almeida",
                    Email = "gilcielphp@gmail.com",
                    UserName = "gilcielphp@gmail.com",
                    Address = "Rua Rio Negro",
                    CityId = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = this.context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }
            var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }

            // Add products
            if (!this.context.Products.Any())
            {
                this.AddProduct("iPhone X", user);
                this.AddProduct("Magic Mouse", user);
                this.AddProduct("iWatch Series 4", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(1000),
                IsAvailabe = true,
                Stock = this.random.Next(100),
                User = user
            });
        }
    }

    //using System;
    //using System.Linq;
    //using System.Threading.Tasks;
    //using Entities;
    //using Microsoft.AspNetCore.Identity;
    //using Helpers;

    //public class SeedDb
    //{
    //    private readonly DataContext context;
    //    private readonly IUserHelper userHelper;
    //    private Random random;

    //    public SeedDb(DataContext context,IUserHelper userHelper)
    //    {
    //        this.context = context;
    //        this.userHelper = userHelper;
    //        this.random = new Random();
    //    }

    //    public async Task SeedAsync()
    //    {
    //        await this.context.Database.EnsureCreatedAsync();
    //        var user = await this.userHelper.GetUserByEmailAsync("gilcielphp@gmail.com");
    //        if (user == null)
    //        {
    //            user = new User
    //            {
    //                FirstName = "Gilciel",
    //                LastName = "Almeida",
    //                Email = "gilcielphp@gmail.com",
    //                UserName = "gilcielphp@gmail.com"
    //            };

    //            var result = await this.userHelper.AddUserAsync(user, "123456");
    //            if (result != IdentityResult.Success)
    //            {
    //                throw new InvalidOperationException("Could not create the user in seeder");
    //            }
    //        }


    //        if (!this.context.Products.Any())
    //        {
    //            this.AddProduct("iPhone X", user);
    //            this.AddProduct("Maginc Mouse", user);
    //            this.AddProduct("iWatch Series 4", user);
    //            await this.context.SaveChangesAsync();
    //        }
    //    }

    //    private void AddProduct(string name,User user)
    //    {
    //        this.context.Products.Add(new Product
    //        {
    //            Name = name,
    //            Price = this.random.Next(1000),
    //            IsAvailabe = true,
    //            Stock = this.random.Next(100),
    //            User = user
    //        });
    //    }
    //}
}
