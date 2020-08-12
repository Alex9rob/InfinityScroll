using System;
using System.Collections.Generic;
using System.Linq;

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
            Console.WriteLine("BlueDataCount " + _data.Count);

            return _data.ToList();
        }

        public void RemoveFrom(int item, EDirection direction)
        {
            var index = _data.FindIndex(f => f == item);

            if (direction == EDirection.Bottom)
            {
                _data.RemoveRange(index, _data.Count - index);
            }

            else if(direction == EDirection.Top)
            {
                _data.RemoveRange(0, index + 1);
            }
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