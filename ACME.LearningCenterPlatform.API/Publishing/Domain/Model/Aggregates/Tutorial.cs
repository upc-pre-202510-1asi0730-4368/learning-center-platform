using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Commands;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Aggregates;

public partial class Tutorial
{
    // ReSharper disable once UnassignedGetOnlyAutoProperty
    public int Id { get; }
    public string Title { get; private set; }
    public string Summary { get; private set; }
    public Category Category { get; internal set; } = null!;
    public int CategoryId { get; private set; }
    
    public Tutorial(string title, string summary, int categoryId) 
    {
        Title = title;
        Summary = summary;
        CategoryId = categoryId;
    }
    
    public Tutorial(CreateTutorialCommand command) : this(command.Title, command.Summary, command.CategoryId)
    {
    }
    
}