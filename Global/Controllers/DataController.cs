using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Global.Controllers
{

    // WebGL Globe implementation from Google: https://github.com/dataarts/webgl-globe
    public class DataController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GeoJSON()
        {
            List<Models.Series> series = new List<Models.Series>();
            series.Add(new Models.Series(){Name = "Farmers Markets United States", LatLongMagnitude = DataManagement.GeoData.GetLongLatArray()});

            Models.Series[] data = series.ToArray();

            return Ok(data);
        }
    }
}
