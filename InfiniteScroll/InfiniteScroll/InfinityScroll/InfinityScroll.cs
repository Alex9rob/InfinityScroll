using System;
using InfiniteScroll.InfinityScroll.InPorts;
using InfiniteScroll.InfinityScroll.OutPorts;

namespace InfiniteScroll.InfinityScroll
{
    public class InfinityScroll : IUserInteraction
    {
        private readonly IBlue _blue;
        private readonly IRed _red;
        private INet _net;
        private readonly IShowData _showData;
        private bool _dataRequested;

        public InfinityScroll(IBlue blue, IRed red, INet net, IShowData showData)
        {
            _blue = blue;
            _red = red;
            _net = net;
            _showData = showData;
        }

        public void EnterScreen()
        {
            ShowLocalData();
        }

        public async void ScrolledToEnd()
        {
            if (_dataRequested)
            {
                return;
            }

            var localData = _blue.Get();
            var localDataCount = localData.Count;

            _dataRequested = true;
            var storedDataDown = await _red.GetFrom(localData[localDataCount - 1]);
            _dataRequested = false;
            _blue.Add(storedDataDown);
            ShowLocalData();
        }
        
        private async void ShowLocalData()
        {
            var data = _blue.Get();
            if (data != null && data.Count != 0)
            {
                _showData.ShowData(data);
            }
            else
            {
                var storedData = await _red.GetFrom(0);
                _blue.Add(storedData);
                data = _blue.Get();
                _showData.ShowData(data);
            }
        }
    }
}