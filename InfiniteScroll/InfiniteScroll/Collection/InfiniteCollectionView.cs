using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace InfiniteScroll.Collection
{
   [Register("InfiniteCollectionView")]
    public class InfiniteCollectionView : UICollectionView
    {
        private InfiniteCollectionFlowLayout _flowLayout;

        public InfiniteCollectionView(NSCoder coder) : base(coder)
        {
        }

        protected InfiniteCollectionView(NSObjectFlag t) : base(t)
        {
        }

        protected internal InfiniteCollectionView(IntPtr handle) : base(handle)
        {
        }

        public InfiniteCollectionView(CGRect frame, UICollectionViewLayout layout) : base(frame, layout)
        {
        }

        public InfiniteCollectionFlowLayout FlowLayout
        {
            get => _flowLayout;
            set
            {
                _flowLayout = value;
                SetCollectionViewLayout(_flowLayout, false);
            }
        }

        public bool IsInsertingCellsToTop
        {
            get => FlowLayout?.IsInsertingCellsToTop ?? false;
            set
            {
                if (FlowLayout != null)
                {
                    FlowLayout.IsInsertingCellsToTop = value;
                }
            }
        }

        public void ScrollCollectionViewToBottomIfPossible(bool animated)
        {
            if (NumberOfItemsInSection(0) > 0)
            {
                if (Bounds.Height < ContentSize.Height)
                {
                    ScrollToBottom(animated);
                }
            }
        }

        private void ScrollToBottom(bool animated)
        {
            var targetOffset = new CGPoint(ContentOffset.X, BottomPositionOffset);
            SetContentOffset(targetOffset, animated);
        }

        private nfloat BottomPositionOffset => ContentSize.Height - Bounds.Height + ContentInset.Bottom;
    }
}