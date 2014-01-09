using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Global.DataManagement
{
    /// <summary>
    /// Handles extraction and transformation of geographic data
    /// </summary>
    public class GeoData
    {
        #region Properties

        // data from https://explore.data.gov/Agriculture/Farmers-Markets-Geographic-Data/wfna-38ey, public domain
        private static string tsvPath = HttpContext.Current.Server.MapPath("/Content/latlong"); 

        public static string TSVPath
        {
            get
            { return tsvPath; }

            set
            { tsvPath = value;}
        }

        #endregion


        #region Methods

        /// <summary>
        /// Retrieves an array of Longitude/Latitude strings from the disk
        /// </summary>
        /// <returns></returns>
        public static string[] LoadCoordinates()
        {
            return File.ReadAllLines(TSVPath);
        }

        /// <summary>
        /// Takes TSV strings of type long/lat and converts to lat/long/magnitude
        /// </summary>
        /// <param name="coordinateData"></param>
        /// <returns></returns>
        public static double[] TransformLongLatToArray(string[] coordinateData)
        {
            int fieldCount = 3;
            double defaultMagnitude = 0.1;

            // allocate memory in advance
            List<double> values = new List<double>(coordinateData.Length * fieldCount);

            // tab separated file, we can expect two values here
            string[] splitResult = new string[2];
            double parseResult;

            foreach (string line in coordinateData)
            {
                // prevent exeptions from a blank newline 
                if (!string.IsNullOrEmpty(line))
                {
                    splitResult = line.Split('\t');

                    if (double.TryParse(splitResult[1], out parseResult))
                    {
                        // add the latitude first
                        values.Add(parseResult);
                    }

                    if (double.TryParse(splitResult[0], out parseResult))
                    {
                        // add the longitude second
                        values.Add(parseResult);
                    }

                    // required by WebGL globe
                    values.Add(defaultMagnitude);
                }
            }

            return values.ToArray();
        }

        /// <summary>
        /// Convenience method for getting the formatted array
        /// </summary>
        /// <returns></returns>
        public static double[] GetLongLatArray()
        {
            return TransformLongLatToArray(LoadCoordinates());
        }

        #endregion
    }
}