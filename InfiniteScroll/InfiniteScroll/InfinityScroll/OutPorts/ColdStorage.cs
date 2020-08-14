using System.Collections.Generic;
using System.Threading.Tasks;
using InfiniteScroll.Entities;
using InfiniteScroll.Enumerables;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public class ColdStorage : IColdStorage
    {
        private List<Number> _data;

        public ColdStorage()
        {
            _data = new List<Number>();
            for (var i = 0; i < 500; i++)
            {
                _data.Add(new Number(i, EStorageType.Cold));
            }
        }

        public async Task<List<Number>> GetFrom(int number)
        {
            await Task.Delay(300);
            var index = _data.FindIndex(f => f.NumberData == number);
            return GetNextData(index);
        }

        private List<Number> GetNextData(int index)
        {
            var startIndex = index + 1;
            var count = 100;
            if (startIndex + count > _data.Count)
            {
                count = _data.Count - startIndex;
            }
            return _data.GetRange(startIndex, count);
        }

        public void AddFrom(int number, List<Number> items)
        {
            _data.AddRange(items);
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}