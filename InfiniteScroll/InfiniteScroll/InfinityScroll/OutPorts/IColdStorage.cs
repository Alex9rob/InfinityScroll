using System.Collections.Generic;
using System.Threading.Tasks;
using InfiniteScroll.Entities;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public interface IColdStorage
    {
        Task<List<Number>> GetFrom(int number);
        void AddOrUpdate(int number, List<Number> items);
        void Clear();
    }
}