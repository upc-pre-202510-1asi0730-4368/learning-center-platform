namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.ValueObjects;

/// <summary>
/// Represents the various publishing statuses of a content item.
/// </summary>
public enum EPublishingStatus
{
    Draft,
    ReadyToEdit,
    ReadyToApproval,
    ApprovedAndLocked
}