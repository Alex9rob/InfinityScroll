using System;

namespace InfiniteScroll.InfinityScroll.InPorts
{
    public interface IUserInteraction<T> where T : IComparable
    {
        void EnterScreen();
        void ScrolledToEnd();
    }
}
