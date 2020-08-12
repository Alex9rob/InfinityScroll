namespace InfiniteScroll.InfinityScroll.InPorts
{
    public interface IUserInteraction
    {
        void EnterScreen();
        void ScrolledTo(EDirection direction);    
    }
}
