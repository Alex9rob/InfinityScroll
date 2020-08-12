namespace InfiniteScroll.InfinityScroll.InPorts
{
    public interface IUserInteraction
    {
        void EnterScreen();
        void ScrolledTo(int item, EDirection direction);    
    }
}
