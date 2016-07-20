using System.Collections.Generic;

namespace LogViewer
{
    /// <summary>
    /// 
    /// </summary>
    internal class LogLine
    {
        #region Member Variables/Properties
        public int LineNumber { get; set; } = 0;
        public int CharCount { get; set; } = 0;
        public long Offset { get; set; } = 0;
        public List<ushort> SearchMatches { get; set; } = new List<ushort>();
        public bool IsContextLine { get; set; } = false;
        #endregion
    }
}
