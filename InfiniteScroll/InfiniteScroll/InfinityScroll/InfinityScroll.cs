using System;
using System.Threading.Tasks;
using InfiniteScroll.InfinityScroll.InPorts;
using InfiniteScroll.InfinityScroll.OutPorts;

namespace InfiniteScroll.InfinityScroll
{
    public class InfinityScroll : IUserInteraction
    {
        private readonly IHotStorage _hotStorage;
        private readonly IColdStorage _coldStorage;
        private INet _net;
        private readonly IShowData _showData;
        private bool _dataRequested;

        public InfinityScroll(IHotStorage hotStorage, IColdStorage coldStorage, INet net, IShowData showData)
        {
            _hotStorage = hotStorage;
            _coldStorage = coldStorage;
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

            var localData = _hotStorage.Get();
            var localDataCount = localData.Count;

            _dataRequested = true;

            var lastIndex = localData[localDataCount - 1].NumberData;
            var storedDataDown = await _coldStorage.GetFrom(lastIndex);


            if(storedDataDown.Count == 0) //or less than threshold
            {
                var netData = await _net.GetFrom(lastIndex);
                _coldStorage.AddFrom(lastIndex, netData);
                storedDataDown = await _coldStorage.GetFrom(lastIndex);
            }

            _dataRequested = false;
            _hotStorage.Add(storedDataDown);
            ShowLocalData();
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
                var storedData = await _coldStorage.GetFrom(0);
                _hotStorage.Add(storedData);
                data = _hotStorage.Get();
                _showData.ShowData(data);
            }
        }
    }
}