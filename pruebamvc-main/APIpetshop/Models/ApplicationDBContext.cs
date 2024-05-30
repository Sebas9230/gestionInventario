using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace APIpetshop.Models
{
	public class ApplicationDBContext: DbContext
	{
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options){ }

        public DbSet<Producto> productos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().HasData(
                new Producto()
                {
                    id = 1001,
                    name = "Cool Watch",
                    quantity = "44",
                    price = "99"
                },
            new Producto()
            {
                id = 1002,
                name = "Diving Watch",
                quantity = "89",
                price = "89"
            },
            new Producto()
            {
                id = 1003,
                name = "Dress Watch",
                quantity = "66",
                price = "85"
            },
            new Producto()
            {
                id = 1004,
                name = "Military Watch",
                quantity = "45",
                price = "99"
            },

            new Producto()
            {
                id = 1005,
                name = "Wrist Watch",
                quantity = "75",
                price = "85"
            },

            new Producto()
            {
                id = 1006,
                name = "Army Watch",
                quantity = "50",
                price = "99"
            }
            );
        }
    }
}

