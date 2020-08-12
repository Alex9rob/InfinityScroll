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
        private IUserInteraction _userInteraction;
        private bool _notEmptyCollection = true;

        public List<CellVisualModel> Data { get; set; }// = new List<int>();
        

        public DataSource(IUserInteraction userInteraction)
        {
            _userInteraction = userInteraction;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell(Cell.Key, indexPath) as Cell;
            var item = Data[(int)indexPath.Item];
            cell.SetupCell(item.Data);
            var border = 30;

            var index = (int)indexPath.Item;
            if (index < border && _notEmptyCollection)
            {
                _userInteraction.ScrolledTo(EDirection.Top);
                Console.WriteLine("ScrolledTo " + item + EDirection.Top);
            }
            else if (index > GetCount() - 1 - border && _notEmptyCollection)
            {
                _userInteraction.ScrolledTo(EDirection.Bottom);
                Console.WriteLine("ScrolledTo " + item + EDirection.Bottom);
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
