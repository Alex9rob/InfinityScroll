using System;
using InfiniteScroll.InfinityScroll.InPorts;
using InfiniteScroll.InfinityScroll.OutPorts;

namespace InfiniteScroll.InfinityScroll
{
    public class InfinityScroll<T> : IUserInteraction<T>  where T : IComparable
    {
        private readonly IHotStorage<T> _hotStorage;
        private readonly IColdStorage<T> _coldStorage;
        private INet<T> _net;
        private readonly IShowData<T> _showData;
        private bool _dataRequested;

        public InfinityScroll(IHotStorage<T> hotStorage, IColdStorage<T> coldStorage, INet<T> net, IShowData<T> showData)
        {
            _hotStorage = hotStorage;
            _coldStorage = coldStorage;
            _net = net;
            _showData = showData;
        }

        public void EnterScreen()
        {
            FillDataFromNet();
            ShowLocalData();
        }

        private async void FillDataFromNet()
        {
            var data = await _net.Get();

            await _coldStorage.AddOrUpdate(data);
            var storedData = await _coldStorage.Get();
            _hotStorage.Clear();
            _hotStorage.Add(storedData);
            data = _hotStorage.Get();
            _showData.ShowData(data);
        }

        public async void ScrolledToEnd()
        {
            if (_dataRequested)
            {
                return;
            }

            var localData = _hotStorage.Get();
            var localDataCount = localData.Count;

            _dataRequested = true;
            var lastItem = localData[localDataCount - 1];

            var netData = await _net.GetFrom(lastItem);
            await _coldStorage.AddOrUpdate(netData);

            var storedData = await _coldStorage.GetFrom(lastItem);
            _dataRequested = false;

            _hotStorage.Add(storedData);
            var hotData = _hotStorage.Get();

            _showData.ShowData(hotData);
        }

        private async void ShowLocalData()
        {
            var data = _hotStorage.Get();
            if (data != null && data.Count != 0)
            {
                _showData.ShowData(data);
            }
            else
            {
                var storedData = await _coldStorage.Get();
                _hotStorage.Add(storedData);
                data = _hotStorage.Get();
                _showData.ShowData(data);
            }
        }
    }
}