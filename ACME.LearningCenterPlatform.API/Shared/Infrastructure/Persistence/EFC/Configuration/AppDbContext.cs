using ACME.LearningCenterPlatform.API.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;
using ACME.LearningCenterPlatform.API.Profiles.Infrastructure.Persistence.EFC.Configuration.Extensions;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;
using ACME.LearningCenterPlatform.API.Publishing.Infrastructure.Persistence.EFC.Configuration.Extensions;
using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Apply configurations for the Publishing bounded context
        builder.ApplyPublishingConfiguration();
        // Apply configurations for the Profiles bounded context
        builder.ApplyProfilesConfiguration();
        // Apply configurations for the IAM bounded context
        builder.ApplyIamConfiguration();
        
        
        // Use snake case naming convention for the database
        builder.UseSnakeCaseNamingConvention();
    }
}