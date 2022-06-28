﻿using Microsoft.EntityFrameworkCore;

namespace Lab_1.Models
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }        
        public DbSet<InvestmentContainer> InvestmentContainers { get; set; }        
        public DbSet<Skin> Skins { get; set; }        

        public Context(DbContextOptions<Context> options): base(options)
        {
            Database.EnsureCreated();
        }      
    }
}
