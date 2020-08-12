namespace InfiniteScroll.InfinityScroll.Diff
{
    public struct MoveIndex
    {
        public MoveIndex(int @from, int to)
        {
            From = @from;
            To = to;
        }

        public int From { get; }
        public int To { get; }

        public override string ToString()
        {
            return $"{From} => {To}";
        }
    }
}