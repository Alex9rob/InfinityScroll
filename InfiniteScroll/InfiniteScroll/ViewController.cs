using InfiniteScroll.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using InfiniteScroll.Entities;
using InfiniteScroll.InfinityScroll;
using InfiniteScroll.InfinityScroll.InPorts;
using InfiniteScroll.InfinityScroll.OutPorts;
using InfiniteScroll.Visual;
using UIKit;

namespace InfiniteScroll
{
    public partial class ViewController : UIViewController, IShowData<Number>
    {

        private IUserInteraction<Number> _userInteraction;
        private DataSource _dataSource;
        private UpdateHelper<CellVisualModel> _updateHelper;
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetupViews();
            
            _updateHelper = new UpdateHelper<CellVisualModel>();
            var blue = new HotStorage<Number>();
            var red = new ColdStorage();
            var net = new Net();

            _userInteraction = new InfinityScroll.InfinityScroll<Number>(blue, red, net, this);
            SetUpCollection();
            _userInteraction.EnterScreen();
        }

        private void SetupViews()
        {
            _btnNet.Layer.CornerRadius = 30;
            SetBtnNetBackground();
            _btnNet.TouchUpInside += BtnNetTouchUpInside;
        }

        private void BtnNetTouchUpInside(object sender, EventArgs e)
        {
            Net.IsOn = !Net.IsOn;
            SetBtnNetBackground();
        }

        private void SetBtnNetBackground()
        {
            _btnNet.BackgroundColor = Net.IsOn ? UIColor.Green : UIColor.Red;
        }



        private void SetUpCollection()
        {
            _collectionView.RegisterNibForCell(Cell.Nib, Cell.Key);
            _collectionView.FlowLayout = new InfiniteCollectionFlowLayout();
            _dataSource = new DataSource(_userInteraction);
            _collectionView.DataSource = _dataSource;
            _collectionView.Delegate = new Collection.Delegate();
            _collectionView.Bounces = false;
        }

        public void ShowData(List<Number> data)
        {
            //TODO: adapter
            
            var oldData = _dataSource.Data == null ?
                new List<CellVisualModel>() :
                new List<CellVisualModel>(_dataSource.Data);
            var newData = data.Select(item => new CellVisualModel(item.NumberData, item.StorageType)).ToList();
            _dataSource.UpdateData(newData);
            _updateHelper.UpdateList(_collectionView, newData, oldData);
        }
    }
}