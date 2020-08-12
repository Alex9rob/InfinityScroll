﻿using InfiniteScroll.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using InfiniteScroll.InfinityScroll;
using InfiniteScroll.InfinityScroll.InPorts;
using InfiniteScroll.InfinityScroll.OutPorts;
using InfiniteScroll.Visual;
using UIKit;

namespace InfiniteScroll
{
    public partial class ViewController : UIViewController, IShowData
    {

        private IUserInteraction _userInteraction;
        private DataSource _dataSource;
        private UpdateHelper<CellVisualModel> _updateHelper;
        private List<CellVisualModel> _oldData = new List<CellVisualModel>();
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _updateHelper = new UpdateHelper<CellVisualModel>();
            var blue = new Blue();
            var red = new Red();
            var net = new Net();
            
            _userInteraction = new InfinityScroll.InfinityScroll(blue, red, net, this);
            SetUpCollection();
            _userInteraction.EnterScreen();
        }

        private void SetUpCollection()
        {
            _collectionView.RegisterNibForCell(Cell.Nib, Cell.Key);
            _collectionView.FlowLayout = new InfiniteCollectionFlowLayout();
            _dataSource = new DataSource(_userInteraction);
            _collectionView.DataSource = _dataSource;
            _collectionView.Delegate = new Collection.Delegate(_userInteraction);
            _collectionView.Bounces = false;
        }

        public void ShowData(List<int> data)
        {
            // var currentContentOffsetY = _collectionView.ContentOffset.Y;

            // var newContentOffsetY = 0;
            // if(currentContentOffsetY< 100)
            // {
            //     newContentOffsetY = 3000;
            // }
            // else if(currentContentOffsetY> 5000)
            // {
            //     newContentOffsetY = 2540;
            // }
            // _dataSource.UpdateData(data);





            //if (_dataSource.Data != null)
            //{
                //var oldData = _dataSource.Data.Select(item => new CellVisualModel(item)).ToList();

                var oldData = _dataSource.Data== null?
                    new List<CellVisualModel>():
                    new List<CellVisualModel>(_dataSource.Data);


                var newData = data.Select(item => new CellVisualModel(item)).ToList();
                _dataSource.UpdateData(newData);
                _updateHelper.UpdateList(_collectionView, newData, oldData);
               // _oldData = newData.ToList();

            //}
            //else
            //{
            //    _dataSource.UpdateData(data);
            //    _collectionView.ReloadData();
            //}

            //_collectionView.SetContentOffset(new CoreGraphics.CGPoint(0, newContentOffsetY), false);
        }
    }
}