using System.Collections.Generic;

namespace LogViewer
{
    /// <summary>
    /// 
    /// </summary>
    public class Searches
    {
        #region Member Variables
        public List<SearchCriteria> Items { get; private set; }
        private ushort counter = 0; // Basically a counter 
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public Searches()
        {
            Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sc"></param>
        /// <returns>The Id of the search criteria object</returns>
        public ushort Add(SearchCriteria sc, bool cumulative)
        {
            if (cumulative == false)
            {
                Reset();
            }

            counter++;
            sc.Id = counter;
            this.Items.Add(sc);

            return sc.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void Reset()
        {
            this.Items = new List<SearchCriteria>();
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get
            {
                return this.Items.Count;
            }
        }
    }
}
