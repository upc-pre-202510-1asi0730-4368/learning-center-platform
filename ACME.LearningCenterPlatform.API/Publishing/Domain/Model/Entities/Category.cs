using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Commands;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;

/// <summary>
/// Represents a category for organizing content in the learning platform.
/// </summary>
/// <param name="name">
/// The name of the category.
/// </param>
public class Category(string name)
{
    public int Id { get; set; }
    public string Name { get; set; } = name;

    /// <summary>
    /// Initializes a new instance of the <see cref="Category"/> class with an empty name.
    /// </summary>
    public Category() : this(string.Empty) {}

    public Category(CreateCategoryCommand command) : this(command.Name)
    {
    }
    
}