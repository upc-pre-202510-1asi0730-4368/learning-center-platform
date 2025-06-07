namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.ValueObjects;

/// <summary>
/// Represents a content item with a type and content.
/// </summary>
/// <param name="Type">
/// The type of the content item, a string representation of <see cref="EAssetType">EAssetType</see> value.
/// </param>
/// <param name="Content">
/// The content of the item, which can be a string representation of the content.
/// </param>
public record ContentItem(string Type, string Content);