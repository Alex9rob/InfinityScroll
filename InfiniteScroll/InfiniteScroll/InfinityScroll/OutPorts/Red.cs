using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public class Red : IRed
    {
        private List<int> _data;
        public Red()
        {
            _data = new List<int>();
            for (var i = -1000; i < 200000; i++)
            {
                _data.Add(i);
            }
        }

        public async Task<List<int>> GetFrom(int item)
        {
            await Task.Delay(300);
            var index = _data.FindIndex(f => f == item);
            return GetNextData(index);
        }

        private List<int> GetNextData(int item)
        {
            var startIndex = item + 1;
            var count = 100;
            if (startIndex + count > _data.Count)
            {
                count = _data.Count - startIndex;
            }
            return _data.GetRange(startIndex, count);
        }

        public void RemoveFrom(int item, EDirection direction)
        {
            throw new System.NotImplementedException();
        }

        public void AddFrom(int item, List<int> items)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}