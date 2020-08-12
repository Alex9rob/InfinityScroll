using System;
using InfiniteScroll.InfinityScroll.InPorts;
using InfiniteScroll.InfinityScroll.OutPorts;

namespace InfiniteScroll.InfinityScroll
{
    public class InfinityScroll : IUserInteraction
    {
        private IBlue _blue;
        private IRed _red;
        private INet _net;
        private IShowData _showData;
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

        public async void ScrolledTo(int item, EDirection direction)
        {
            var localData = _blue.Get();
            var localDataCount = localData.Count;
            if (_dataRequested)
            {
                return;
            }
            switch (direction)
            {
                case EDirection.Bottom:
                    if (item > localData[localDataCount - 1] - 40)
                    {
                        _dataRequested = true;
                        var storedDataDown = await _red.GetFrom(localData[localDataCount - 1], EDirection.Bottom);
                        _dataRequested = false;
                        _blue.AddFrom(localData[localDataCount - 1], storedDataDown);
                        ShowLocalData();
                    }
                    break;
                case EDirection.Top:
                    if (item < localData[0] + 40)
                    {
                        _dataRequested = true;
                        var storedDataUp = await _red.GetFrom(localData[0], EDirection.Top);
                        _dataRequested = false;
                        _blue.AddFrom(localData[0], storedDataUp);
                        ShowLocalData();
                    }
                    break;
            }

            Console.WriteLine("ScrolledTo " + item + direction);
        }

        private void ShowLocalData()
        {
            var data = _blue.Get();
            if (data != null && data.Count != 0)
            {
                _showData.ShowData(data);
            }
        }
    }
}