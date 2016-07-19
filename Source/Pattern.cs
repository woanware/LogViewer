namespace LogViewer
{
    #region Pattern Object
    /// <summary>
    /// The ObjectListView only deals in objects so here we
    /// have a very simple object to encapsulate a line
    /// </summary>
    public class Pattern
    {
        public string Data { get; set; }

        public Pattern(string line)
        {
            this.Data = line;
        }
    }
    #endregion
}
