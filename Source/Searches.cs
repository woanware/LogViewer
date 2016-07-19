using System.Collections.Generic;
using System.Linq;

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

            if (sc.Type == Global.SearchType.RegexCaseInsensitive || sc.Type == Global.SearchType.SubStringCaseInsensitive)
            {
                var ret = this.Items.SingleOrDefault(x => x.Pattern.ToLower() == sc.Pattern.ToLower());
                if (ret != null)
                {
                    return 0;
                }
            }
            else
            {
                var ret = this.Items.SingleOrDefault(x => x.Pattern == sc.Pattern);
                if (ret != null)
                {
                    return 0;
                }
            }

            counter++;
            sc.Enabled = true;
            sc.Id = counter;
            this.Items.Add(sc);

            return sc.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sc"></param>
        /// <returns>The Id of the search criteria object</returns>
        public ushort Add(SearchCriteria sc)
        {
            // If the SearchCriteria already exists then just enable it
            if (sc.Type == Global.SearchType.RegexCaseInsensitive || sc.Type == Global.SearchType.SubStringCaseInsensitive)
            {
                var ret = this.Items.SingleOrDefault(x => x.Pattern.ToLower() == sc.Pattern.ToLower());
                if (ret != null)
                {
                    ret.Enabled = true;
                    return 0;
                }
            }
            else
            {
                var ret = this.Items.SingleOrDefault(x => x.Pattern == sc.Pattern);
                if (ret != null)
                {
                    ret.Enabled = true;
                    return 0;
                }
            }

            counter++;
            sc.Enabled = true;
            sc.Id = counter;
            this.Items.Add(sc);

            return sc.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void Reset()
        {
            counter = 0;
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
