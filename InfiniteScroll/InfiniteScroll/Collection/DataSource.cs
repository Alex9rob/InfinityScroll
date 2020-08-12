using System;
using System.Collections.Generic;
using Foundation;
using InfiniteScroll.InfinityScroll;
using InfiniteScroll.InfinityScroll.InPorts;
using UIKit;

namespace InfiniteScroll.Collection
{
    public class DataSource : UICollectionViewDataSource
    {
        
        
        private IUserInteraction _userInteraction;
        private List<int> _data;
        public DataSource(IUserInteraction userInteraction)
        {
            _userInteraction = userInteraction;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell(Cell.Key, indexPath) as Cell;
            var item = _data[(int)indexPath.Item];
            cell.SetupCell(item);
            var border = 30;

            var index = (int)indexPath.Item;
            if (index < border)
            {
                _userInteraction.ScrolledTo(item, EDirection.Top);
                Console.WriteLine("ScrolledTo " + item + EDirection.Top);
            }
            else if (index > GetCount() - 1 - border)
            {
                _userInteraction.ScrolledTo(item, EDirection.Bottom);
                Console.WriteLine("ScrolledTo " + item + EDirection.Bottom);
            }

            return cell;
        }

        private int GetCount()
        {
            return  _data?.Count ?? 0;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return GetCount();
        }

        public void UpdateData(List<int> data)
        {
            _data = data;
        }

        
    }
}
