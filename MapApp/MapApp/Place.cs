using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace MapApp
{
    public class Place
    {
        public string PlaceName { get; set; }
        public string Address { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
