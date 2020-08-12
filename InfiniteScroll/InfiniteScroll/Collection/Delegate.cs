using System;
using CoreGraphics;
using Foundation;
using InfiniteScroll.InfinityScroll;
using InfiniteScroll.InfinityScroll.InPorts;
using UIKit;

namespace InfiniteScroll.Collection
{
    public class Delegate : UICollectionViewDelegateFlowLayout
    {
        private IUserInteraction _userInteraction;

        public Delegate(IUserInteraction userInteraction)
        {
            _userInteraction = userInteraction;
        }

        public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            return new CGSize(collectionView.Frame.Width, 50);
        }

//        public override void Scrolled(UIScrollView scrollView)
//        {
////            base.Scrolled(scrollView);
//            var screenHeight = 800f;
//            if (scrollView.ContentOffset.Y < screenHeight)
//            {
//                //top
//                _userInteraction.ScrolledTo(EDirection.Top);
//            }
//            else if(scrollView.ContentOffset.Y > scrollView.ContentSize.Height - screenHeight*2)
//            {
//                //bottom
//                _userInteraction.ScrolledTo(EDirection.Bottom);
//            }
//        }
    }
}
