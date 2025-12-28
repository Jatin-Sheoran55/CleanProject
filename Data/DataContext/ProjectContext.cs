
using Domain.Entities;
using System;
using Microsoft.EntityFrameworkCore;
//using Domain.Entities.Categorys;

namespace Infrastructure.DataContext;

public class ProjectContext:DbContext
{
    public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<UserCart> UserCarts { get; set; }
    public DbSet<UserCartItem> UserCartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Address>()
       .HasOne(a => a.User)
       .WithMany(u => u.Addresses)
       .HasForeignKey(a => a.UserId);
        base.OnModelCreating(modelBuilder);

        

    }
}

