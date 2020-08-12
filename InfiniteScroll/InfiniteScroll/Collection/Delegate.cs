using System;
using CoreGraphics;
using Foundation;
using InfiniteScroll.InfinityScroll.InPorts;
using UIKit;

namespace InfiniteScroll.Collection
{
    public class Delegate : UICollectionViewDelegateFlowLayout
    {
        public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            return new CGSize(collectionView.Frame.Width, 20);
        }
    }
}
