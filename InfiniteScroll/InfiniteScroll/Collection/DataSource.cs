using System;
using System.Collections.Generic;
using Foundation;
using InfiniteScroll.InfinityScroll;
using InfiniteScroll.InfinityScroll.InPorts;
using InfiniteScroll.Visual;
using UIKit;

namespace InfiniteScroll.Collection
{
    public class DataSource : UICollectionViewDataSource
    {
        private readonly IUserInteraction _userInteraction;
        private bool _notEmptyCollection = true;

        public List<CellVisualModel> Data { get; set; }

        public DataSource(IUserInteraction userInteraction)
        {
            _userInteraction = userInteraction;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell(Cell.Key, indexPath) as Cell;
            var item = Data[(int)indexPath.Item];
            cell.SetupCell(item);
            var border = 50;

            var index = (int)indexPath.Item;
            if (index > GetCount() - 1 - border && _notEmptyCollection)
            {
                _userInteraction.ScrolledToEnd();
                Console.WriteLine("ScrolledToEnd " + item.Data);
            }
            return cell;
        }

        private int GetCount()
        {
            return  Data?.Count ?? 0;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return GetCount();
        }

        public void UpdateData(List<CellVisualModel> data)
        {
            if(Data!= null && Data.Count != 0)
            {
                _notEmptyCollection = true;
            }
            Data = data;
        }
    }
}
