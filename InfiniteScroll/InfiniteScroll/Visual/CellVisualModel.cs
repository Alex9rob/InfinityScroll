using InfiniteScroll.InfinityScroll.Diff;

namespace InfiniteScroll.Visual
{
    public struct CellVisualModel : IDiffable<string>
    {
        public string DiffIdentifier => Data.ToString();
        
        public int Data { get; set; }

        public CellVisualModel(int data)
        {
            Data = data;
        }
    }
}