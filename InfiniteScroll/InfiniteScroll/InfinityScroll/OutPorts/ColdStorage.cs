using System.Collections.Generic;
using System.Threading.Tasks;
using InfiniteScroll.Entities;
using InfiniteScroll.Enumerables;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public class ColdStorage : IColdStorage
    {
        private List<Number> _data;
        private IColdStorage _coldStorageImplementation;

        public ColdStorage()
        {
            _data = new List<Number>();
            for (var i = -1000; i < 200000; i++)
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
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}