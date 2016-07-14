
namespace LogViewer
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchCriteria
    {
        public bool Enabled { get; set; } = true;
        public ushort Id { get; set; } = 0;
        public Global.SearchType Type { get; set; }
        public string Pattern { get; set; } = "";
        public long Matches { get; set; } = 0;
    }
}
