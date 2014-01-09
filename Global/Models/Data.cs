using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Global.Models
{
    /// <summary>
    /// The model we're going to send to the client
    /// </summary>
    public class Data
    {
        public Series[] Series { get; set; }
    }

    /// <summary>
    /// An individual series; the WebGL Globe can accept multiple series and alternate among them
    /// </summary>
    public class Series
    {
        public string Name { get; set; }
        public double[] LatLongMagnitude { get; set; }
    }
     

}