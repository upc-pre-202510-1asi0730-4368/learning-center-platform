using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Commands;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.ValueObjects;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Aggregates;

public partial class Tutorial : IPublishable
{
    public ICollection<Asset> Assets { get; }
    public EPublishingStatus Status { get; protected set; }

    public bool Readable => HasReadableAssets;
    
    public bool Viewable => HasViewableAssets;

    public bool HasReadableAssets => Assets.Any(asset => asset.Readable);
    public bool HasViewableAssets => Assets.Any(asset => asset.Viewable);
    
    private bool HasAllAssetsWithStatus(EPublishingStatus status)
    {
        return Assets.All(asset => asset.Status == status);
    }
    
    public void SendToEdit()
    {
        if (HasAllAssetsWithStatus(EPublishingStatus.ReadyToEdit))
            Status = EPublishingStatus.ReadyToEdit;
    }

    public void SendToApproval()
    {
        if (HasAllAssetsWithStatus(EPublishingStatus.ReadyToApproval))
            Status = EPublishingStatus.ReadyToApproval;
    }

    public void ApproveAndLock()
    {
        if (HasAllAssetsWithStatus(EPublishingStatus.ApprovedAndLocked))
            Status = EPublishingStatus.ApprovedAndLocked;
    }
    

    public void Reject()
    {
        Status = EPublishingStatus.Draft;
    }

    public void ReturnToEdit()
    {
        Status = EPublishingStatus.ReadyToEdit;   
    }
    
    private bool ExistsImageByUrl(string imageUrl) 
    {
        return Assets.Any(asset => asset.Type == EAssetType.Image &&
                                   (string)asset.GetContent() == imageUrl);
    }
    
    public bool ExistsVideoByUrl(string videoUrl) 
    {
        return Assets.Any(asset => asset.Type == EAssetType.Video &&
                                   (string)asset.GetContent() == videoUrl);
    }

    public bool ExistsReadableContent(string readableContent)
    {
        return Assets.Any(asset => asset.Type == EAssetType.ReadableContentItem &&
                                   (string)asset.GetContent() == readableContent);
    }

    public void AddVideo(string videoUrl)
    {
        if (ExistsVideoByUrl(videoUrl)) return;
        Assets.Add(new VideoAsset(videoUrl));
    }
    
    public void AddImage(string imageUrl)
    {
        if (ExistsImageByUrl(imageUrl)) return;
        Assets.Add(new ImageAsset(imageUrl));
    }
    
    public void AddReadableContent(string readableContent)
    {
        if (ExistsReadableContent(readableContent)) return;
        Assets.Add(new ReadableContentAsset(readableContent));
    }

    public void RemoveAsset(AcmeAssetIdentifier identifier)
    {
        var asset = Assets.FirstOrDefault(a => a.AssetIdentifier.Identifier == identifier.Identifier);
        if (asset is null) return;
        Assets.Remove(asset);
    }
    
    public void ClearAssets()
    {
        Assets.Clear();
    }

    public List<ContentItem> GetContent()
    {
        var content = new List<ContentItem>();
        if (Assets.Count > 0)
            content.AddRange(Assets.Select(asset => 
                new ContentItem(asset.Type.ToString(), asset.GetContent() as string ?? string.Empty)));
        return content;
    }
    
    public void Handle(AddVideoAssetToTutorialCommand command)
    {
        if (command.TutorialId == Id) 
            AddVideo(command.VideoUrl);
    }
}