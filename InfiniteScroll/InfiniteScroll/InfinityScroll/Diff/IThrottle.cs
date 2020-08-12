using System;

namespace InfiniteScroll.InfinityScroll.Diff
{
    public interface IThrottle
    {
        void On(Action handler);
        void Cancel();
    }
}