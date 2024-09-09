using CampusConnect.Data.Interceptors;
using CampusConnect.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CampusConnect.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Country> Country { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<UserUniversityBookmark> UserUniversityBookmarks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new AuditableInterceptor(), new SoftDeleteInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var (countries, universities) = GetSeedData();

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasData(countries);
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity
            .HasOne(x => x.Country)
            .WithMany()
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.Property(x => x.Id)
            .ValueGeneratedOnAdd();

            entity.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

            entity.Property(x => x.Webpages)
            .HasConversion(new StringArrayToStringConverter())
            .HasMaxLength(200);

            entity.HasData(universities);
        });

        modelBuilder.Entity<UserUniversityBookmark>(entity =>
        {
            entity.HasKey(x => new { x.UserId, x.UniversityId });
        });

        // Set SoftDelete QueryFilter
        var softDeleteEntities = typeof(ISoftDelete).Assembly.GetTypes()
              .Where(type => typeof(ISoftDelete)
                              .IsAssignableFrom(type)
                              && type.IsClass
                              && !type.IsAbstract);

        foreach (var softDeleteEntity in softDeleteEntities)
        {
            modelBuilder.Entity(softDeleteEntity).HasQueryFilter(
                GenerateSoftDeleteQueryFilterLambda(softDeleteEntity));
        }
    }

    private static LambdaExpression GenerateSoftDeleteQueryFilterLambda(Type type)
    {
        var parameter = Expression.Parameter(type, "x");
        var falseConstantValue = Expression.Constant(false);
        var propertyAccess = Expression.PropertyOrField(parameter, nameof(ISoftDelete.IsDeleted));
        var equalExpression = Expression.Equal(propertyAccess, falseConstantValue);
        var lambda = Expression.Lambda(equalExpression, parameter);

        return lambda;
    }

    private static (List<Country> countries, List<University> universities) GetSeedData()
    {
        var singapore = new Country { Id = Guid.NewGuid(), CountryCode = "SGP", Name = "Singapore" };
        var malaysia = new Country { Id = Guid.NewGuid(), CountryCode = "MYS", Name = "Malaysia" };

        var countries = new List<Country>
        {
            singapore,
            malaysia
        };

        var universities = new List<University>
        {
            new() { Id = Guid.NewGuid(), Name = "National University of Singapore", CountryId = singapore.Id, Webpages = ["www.website1.com", "www.register.website1.com"], IsActive = true, Created = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Name = "Nanyang Technological University", CountryId = singapore.Id, Webpages = ["www.website2.com", "www.register.website2.com"], IsActive = true, Created = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Name = "Universiti Malaya", CountryId = malaysia.Id, Webpages = ["www.website3.com", "www.register.website3.com"], IsActive = true, Created = DateTime.UtcNow }
        };

        return (countries, universities);
    }
}