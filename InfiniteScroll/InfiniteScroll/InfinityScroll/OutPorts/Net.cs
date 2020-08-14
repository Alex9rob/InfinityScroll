using System.Collections.Generic;
using System.Threading.Tasks;
using InfiniteScroll.Entities;
using InfiniteScroll.Enumerables;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public class Net : INet
    {
        private List<Number> _data;

        public Net()
        {
            _data = new List<Number>();
            for (var i = 0; i < 1000; i++)
            {
                _data.Add(new Number(i, EStorageType.Net));
            }
        }

        public async Task<List<Number>> GetFrom(int number)
        {
            await Task.Delay(700);
            var index = _data.FindIndex(f => f.NumberData == number);
            return GetNextData(index);
        }

        private List<Number> GetNextData(int index)
        {
            var startIndex = index + 1;
            var count = 300;
            if (startIndex + count > _data.Count)
            {
                count = _data.Count - startIndex;
            }
            return _data.GetRange(startIndex, count);
        }
    }
}