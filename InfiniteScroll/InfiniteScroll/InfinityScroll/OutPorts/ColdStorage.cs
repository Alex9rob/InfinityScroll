using System.Collections.Generic;
using System.Threading.Tasks;
using InfiniteScroll.Entities;
using InfiniteScroll.Enumerables;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public class ColdStorage : IColdStorage<Number> 
    {
        private List<Number> _data;

        public ColdStorage()
        {
            _data = new List<Number>();
            for (var i = 0; i <= 350; i++)
            {
                AddModel(i);
            }

            for (var i = 450; i <= 900; i++)
            {
                AddModel(i);
            }
        }

        private void AddModel(int number)
        {
            _data.Add(new Number(number, EStorageType.Cold));
        }

        public async Task<List<Number>> Get()
        {
            await Task.Delay(300);
            var res = GetNextData(-1);
            return res;
        }

        public async Task<List<Number>> GetFrom(Number itemFrom)
        {
            await Task.Delay(300);
            var index = _data.FindIndex(f => f.NumberData == itemFrom.NumberData);
            var res = GetNextData(index);
            return res;
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

        public Task AddOrUpdate(List<Number> items)
        {
            if(items.Count == 0)
            {
                return Task.CompletedTask;
            }
            var firstNumber = items[0].NumberData;
            var lastNumber = items[items.Count - 1].NumberData;

            var firstNumberDataInStorage = GetFirstNumberDataInStorage(firstNumber);
            var lastNumberDataInStorage = GetLastNumberDataInStorage(lastNumber);

            var firstIndexInStorage = _data.FindIndex(f => f.NumberData == firstNumberDataInStorage);
            var lastIndexInStorage = _data.FindIndex(f => f.NumberData == lastNumberDataInStorage);

            var count = 0;
            foreach(var item in _data)
            {
                if(item.NumberData>= firstIndexInStorage & item.NumberData <= lastIndexInStorage)
                {
                    count++;
                }
            }                

            _data.RemoveRange(firstIndexInStorage, count);
            _data.InsertRange(firstIndexInStorage, items);
            return Task.CompletedTask;
        }

        private int GetFirstNumberDataInStorage(int firstNumberInNet)
        {
            for (int i = 0; i < _data.Count; i++)
            {
                var number = _data[i].NumberData;
                if (number >= firstNumberInNet)
                {
                    return number;
                }
            }
            return 0;
        }

        private int GetLastNumberDataInStorage(int lastNumberNet)
        {
            var lastIndex = 0;
            for (int i = 0; i < _data.Count; i++)
            {
                var number = _data[i].NumberData;
                if (number > lastNumberNet)
                {
                    break;
                }
                lastIndex = number;
            }
            return lastIndex;
        }

        public void Clear()
        {
            _data = new List<Number>();
        }
    }
}