namespace InfiniteScroll.InfinityScroll.Diff
{
    public interface IDiffable<T>
    {
        T DiffIdentifier { get; }
    }
}