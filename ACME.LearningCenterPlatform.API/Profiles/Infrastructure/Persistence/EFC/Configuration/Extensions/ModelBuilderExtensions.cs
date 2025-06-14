using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace ACME.LearningCenterPlatform.API.Profiles.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyProfilesConfiguration(this ModelBuilder builder)
    {
        // Profiles Context
        builder.Entity<Profile>().HasKey(p => p.Id);
        builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Profile>().OwnsOne(p => p.Name, n =>
        {
            n.WithOwner().HasForeignKey("Id");
            n.Property(n => n.FirstName).HasColumnName("FirstName");
            n.Property(n => n.LastName).HasColumnName("LastName");
        });
        builder.Entity<Profile>().OwnsOne(p => p.Email, e =>
        {
            e.WithOwner().HasForeignKey("Id");
            e.Property(e => e.Address).HasColumnName("EmailAddress");
        });
        builder.Entity<Profile>().OwnsOne(p => p.Address, a =>
        {
            a.WithOwner().HasForeignKey("Id");
            a.Property(a => a.Street).HasColumnName("AddressStreet");
            a.Property(a => a.Number).HasColumnName("AddressNumber");
            a.Property(a => a.City).HasColumnName("AddressCity");
            a.Property(a => a.PostalCode).HasColumnName("AddressPostalCode");
            a.Property(a => a.Country).HasColumnName("AddressCountry");
        });
    }
}