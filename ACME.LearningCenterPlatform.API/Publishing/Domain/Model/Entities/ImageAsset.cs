using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.ValueObjects;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;

public class ImageAsset(EAssetType type) : Asset(type)
{
    public Uri? ImageUri { get;  }

    public override bool Readable => false;
    public override bool Viewable => true;
    
    public ImageAsset() : this(EAssetType.Image)
    {
    }

    public ImageAsset(string imageUrl) : this(EAssetType.Image)
    {
        ImageUri = new Uri(imageUrl);    
    }

    public override string GetContent()
    {
        return ImageUri != null ? ImageUri.AbsoluteUri : string.Empty;   
    }
}