using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfiniteScroll.InfinityScroll.OutPorts
{
    public interface IColdStorage<T> where T : IComparable
    {
        Task<List<T>> Get();
        Task<List<T>> GetFrom(T itemFrom);
        Task AddOrUpdate(List<T> items);
        void Clear();
    }
}