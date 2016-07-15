using System;
using System.IO;
using System.Drawing;
using Nett;
using woanware;

namespace LogViewer
{
    /// <summary>
    /// Allows us to save/load the configuration file to/from TOML
    /// </summary>
    public class Configuration
    {
        #region Member Variables
        public string HighlightColour { get; set; } = "Lime";
        public int MultiSelectLimit { get; set; } = 1000;
        private const string FILENAME = "LogViewer.toml";
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Load()
        {
            try
            {
                if (File.Exists(this.GetPath()) == false)
                {
                    return string.Empty;
                }

                Configuration c = Toml.ReadFile<Configuration>(this.GetPath());
                this.HighlightColour = c.HighlightColour;
                this.MultiSelectLimit = c.MultiSelectLimit;

                if (this.MultiSelectLimit > 10000)
                {
                    this.MultiSelectLimit = 10000;
                    return "The multiselect limit is 10000";
                }
                return string.Empty;
            }
            catch (FileNotFoundException fileNotFoundEx)
            {
                return fileNotFoundEx.Message;
            }
            catch (UnauthorizedAccessException unauthAccessEx)
            {
                return unauthAccessEx.Message;
            }
            catch (IOException ioEx)
            {
                return ioEx.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Save()
        {
            try
            {
                Toml.WriteFile(this, this.GetPath());
                return string.Empty;
            }
            catch (FileNotFoundException fileNotFoundEx)
            {
                return fileNotFoundEx.Message;
            }
            catch (UnauthorizedAccessException unauthAccessEx)
            {
                return unauthAccessEx.Message;
            }
            catch (IOException ioEx)
            {
                return ioEx.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Color GetHighlightColour()
        {
            Color temp = Color.FromName(this.HighlightColour);
            if (temp.IsKnownColor == false)
            {
                return Color.Lime;
            }

            return temp;
        }
        #endregion

        #region Misc Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetPath()
        {
            return System.IO.Path.Combine(Misc.GetApplicationDirectory(), FILENAME);
        }
        #endregion
    }
}