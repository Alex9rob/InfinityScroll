using System;
using CoreGraphics;
using UIKit;

namespace InfiniteScroll.Collection
{
    public class InfiniteCollectionFlowLayout : UICollectionViewFlowLayout
    {
        public bool IsInsertingCellsToTop { get; set; }

        public InfiniteCollectionView ChatCollectionView
        {
            get
            {
                if (CollectionView is InfiniteCollectionView chatCollectionView)
                {
                    return chatCollectionView;
                }

                return null;
            }
        }

        public override void PrepareLayout()
        {
            base.PrepareLayout();
            if (IsInsertingCellsToTop)
            {
                var prevContentOffset = CollectionView.ContentOffset;
                var prevContentSize = CollectionView.ContentSize;
                var newContentSize = CollectionViewContentSize;

                var deltaHeight = newContentSize.Height - prevContentSize.Height;
                var newContentOffset = new CGPoint(prevContentOffset.X, prevContentOffset.Y + deltaHeight);
                CollectionView.ContentOffset = newContentOffset;
            }
        }
    }
}