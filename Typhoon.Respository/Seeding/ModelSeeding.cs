using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Typhoon.Domain.Entities;

namespace Typhoon.Respository.Seeding
{
    public static class ModelSeeding
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Television",
                    Description = "Television Category",
                    CreatedDate = DateTime.Now
                },
                new Category
                {
                    Id = 2,
                    Name = "Mobile Phone",
                    Description = "Mobile Phone Category",
                    CreatedDate = DateTime.Now
                }

                );

            modelBuilder.Entity<Product>().HasData(
               new Product
               {
                   Id = 1,
                   Name = "Samsung Galaxy A04S",
                   Description = "Samsung Galaxy A04s 64 GB 4 GB Ram (Samsung Türkiye Garantili)",
                   Brand = "Samsung",
                   Price = 5300,
                   CategoryId = 2,
                   CreatedDate = DateTime.Now
               },
               new Product
               {
                   Id = 2,
                   Name = "Samsung Galaxy A54",
                   Description = "Samsung Galaxy A54 256 GB 8 GB Ram (Samsung Türkiye Garantili)",
                   Brand = "Samsung",
                   Price = 16660,
                   CategoryId = 2,
                   CreatedDate = DateTime.Now
               },
                new Product
                {
                    Id = 3,
                    Name = "Samsung Galaxy S24 Ultra",
                    Description = "Samsung Galaxy S24 Ultra 512 GB 12 GB Ram (Samsung Türkiye Garantili)",
                    Brand = "Samsung",
                    Price = 69999,
                    CategoryId = 2,
                    CreatedDate = DateTime.Now
                },
                  new Product
                  {
                      Id = 4,
                      Name = "Samsung Galaxy S23 Ultra",
                      Description = "Samsung Galaxy S23 Ultra 512 GB 12 GB Ram (Samsung Türkiye Garantili)",
                      Brand = "Samsung",
                      Price = 57599,
                      CategoryId = 2,
                      CreatedDate = DateTime.Now
                  },
                   new Product
                   {
                       Id = 5,
                       Name = "iPhone 15 Pro",
                       Description = "iPhone 15 Pro Max 512 GB",
                       Brand = "Apple",
                       Price = 85699,
                       CategoryId = 2,
                       CreatedDate = DateTime.Now
                   },
                    new Product
                    {
                        Id = 6,
                        Name = "iPhone 13 Pro Max",
                        Description = "iPhone 13 Pro Max 512 GB",
                        Brand = "Apple",
                        Price = 74999,
                        CategoryId = 2,
                        CreatedDate = DateTime.Now
                    },
                    new Product
                    {
                        Id = 7,
                        Name = "iPhone 14 Plus",
                        Description = "iPhone 14 Plus 128 GB",
                        Brand = "Apple",
                        Price = 48749,
                        CategoryId = 2,
                        CreatedDate = DateTime.Now
                    },
                    new Product
                    {
                        Id = 8,
                        Name = "Samsung 65QN85C",
                        Description = "Samsung 65QN85C 65\" 163 Ekran Uydu Alıcılı 4K Ultra HD Smart Neo QLED TV",
                        Brand = "Samsung",
                        Price = 61099,
                        CategoryId = 1,
                        CreatedDate = DateTime.Now
                    },
                    new Product
                    {
                        Id = 9,
                        Name = "Samsung 55CU8000",
                        Description = "Samsung 55CU8000 55\" 138 Ekran Uydu Alıcılı Crystal 4K Ultra HD Smart LED TV",
                        Brand = "Samsung",
                        Price = 25379,
                        CategoryId = 1,
                        CreatedDate = DateTime.Now
                    },
                    new Product
                    {
                        Id = 10,
                        Name = "LG OLED65CS3VA",
                        Description = "LG OLED65CS3VA 65\" 165 Ekran Uydu Alıcılı 4K Ultra HD webOS Smart OLED TV ",
                        Brand = "LG",
                        Price = 69799,
                        CategoryId = 1,
                        CreatedDate = DateTime.Now
                    },
                    new Product
                    {
                        Id = 11,
                        Name = "LG 65QNED756",
                        Description = "LG 65QNED756 65\" 165 Ekran Uydu Alıcılı 4K Ultra HD webOS Smart QNED TV",
                        Brand = "LG",
                        Price = 44513,
                        CategoryId = 1,
                        CreatedDate = DateTime.Now
                    }
               );

            modelBuilder.Entity<IdentityRole>().HasData(
                 new IdentityRole
                 {
                     Name = "Administrator",
                     NormalizedName = "ADMINISTRATOR"
                 },
                 new IdentityRole
                 {
                     Name = "User",
                     NormalizedName = "USER"
                 });
        }
    }
}
