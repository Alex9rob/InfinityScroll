using System;

namespace InfiniteScroll.InfinityScroll
{
    public static class EventBus
    {
        public static event Action LocalDataUpdated;

        public static void OnLocalDataUpdated()
        {
            LocalDataUpdated?.Invoke();
        }
    }
}