using System.Collections.Generic;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public class Blue : IBlue
    {
        private List<int> _data;

        public Blue()
        {
            _data = new List<int>();
            for (var i = 450; i < 550; i++)
            {
                _data.Add(i);
            }
            
        }
        public bool IsBorder(int item, EDirection direction)
        {
            throw new System.NotImplementedException();
        }

        public List<int> Get()
        {
            return _data;
        }

        public void RemoveFrom(int item, EDirection direction)
        {
            throw new System.NotImplementedException();
        }

        public void AddFrom(int item, List<int> items)
        {
            if (item == _data[_data.Count - 1])
            {
                _data.AddRange(items);
            }
            else if (item == _data[0])
            {
                _data.InsertRange(0, items);
            }
        }
    }
}