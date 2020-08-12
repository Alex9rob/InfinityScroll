using InfiniteScroll.Collection;
using System;
using System.Collections.Generic;
using InfiniteScroll.InfinityScroll.InPorts;
using InfiniteScroll.InfinityScroll.OutPorts;
using UIKit;

namespace InfiniteScroll
{
    public partial class ViewController : UIViewController, IShowData
    {

        private IUserInteraction _userInteraction;
        private DataSource _dataSource;
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
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
            _dataSource = new DataSource(_userInteraction);
            _collectionView.DataSource = _dataSource;
            _collectionView.Delegate = new Collection.Delegate();
            _collectionView.Bounces = false;
        }

        public void ShowData(List<int> data)
        {
            _dataSource.UpdateData(data);
            _collectionView.ReloadData();
        }
    }
}