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
            for (var i = -1000; i < 2000; i++)
            {
                _data.Add(i);
            }
        }
        public bool IsBorder(int item, EDirection direction)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<int>> GetFrom(int item, EDirection direction)
        {
            await Task.Delay(1000);
            var res = new List<int>();
            var index = _data.FindIndex(f => f == item);
            switch (direction)
            {
                case EDirection.Bottom:
                    res = GetNextData(index);
                    break;
                case EDirection.Top:
                    res = GetPreviousData(index);
                    break;
            }

            return res;
        }


        private List<int> GetPreviousData(int index)
        {
            var startIndex = index - 100;
            var count = 100;
            if (startIndex < 0)
            {
                count = count + startIndex;
                startIndex = 0;
            }
            return _data.GetRange(startIndex, count);
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
    }
}