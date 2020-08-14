using System.Collections.Generic;
using System.Threading.Tasks;
using InfiniteScroll.Entities;
using InfiniteScroll.Enumerables;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public class Net : INet
    {
        public static bool IsOn = true;

        private List<Number> _data;

        public Net()
        {
            _data = new List<Number>();
            for (var i = 0; i < 400; i++)
            {
                AddModel(i);
            }
            for (var i = 550; i < 1000; i++)
            {
                AddModel(i);
            }
        }

        private void AddModel(int number)
        {
            _data.Add(new Number(number, EStorageType.Net));
        }

        public async Task<List<Number>> GetFrom(int number)
        {

            await Task.Delay(1000);

            if (!IsOn)
            {
                return new List<Number>();
            }
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