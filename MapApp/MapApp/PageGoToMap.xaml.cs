using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Map = Xamarin.Essentials.Map;

namespace MapApp
{
    [DesignTimeVisible(false)]
    public partial class PageGoToMap : ContentPage
    {
        public PageGoToMap()
        {
            InitializeComponent();

            Task.Delay(2000);
            UpdateMap();
        }

        private void Street_Clicked(object sender, EventArgs e)
        {
            MyMap.MapType = MapType.Street;
        }

        private void Satellite_Clicked(object sender, EventArgs e)
        {
            MyMap.MapType = MapType.Satellite;
        }

        private void Hybrid_Clicked(object sender, EventArgs e)
        {
            MyMap.MapType = MapType.Hybrid;
        }

        double latti, longi;
        string addr;
        List<Place> listPlace = new List<Place>();


        private async void Search_Clicked(object sender, EventArgs e)
        {
            Database db = new Database();
            Node node = new Node { PlaceName = Des.Text };

            Geocoder geoCoder = new Geocoder();

            IEnumerable<Position> approximateLocations = await geoCoder.GetPositionsForAddressAsync(Des.Text);
            Position position = approximateLocations.FirstOrDefault();
            string coordinates = $"{position.Latitude}, {position.Longitude}";

            IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
            string address = possibleAddresses.FirstOrDefault();

            
            Place place = new Place { PlaceName = Des.Text, Address = address, Lat = position.Latitude, Lng = position.Longitude };

            if (db.insertNode(node) == true && db.insertPlace(place) == true)
                DisplayAlert("Notification", "Latitude: " + position.Latitude.ToString() + "\nLongitude: " + position.Longitude.ToString() + "\nAddress: " + address, "OK");
            
            latti = position.Latitude;
            longi = position.Longitude;
            addr = address;

            listPlace.Add(new Place
            {
                PlaceName = Des.Text,
                Address = address,
                Lat = latti,
                Lng = longi,
            });

            UpdateMap();

        }
        private async void UpdateMap()
        {
            try
            {
                Pin pin = new Pin
                {
                    Label = Des.Text,
                    Address = addr,
                    Type = PinType.Place,
                    Position = new Position(latti, longi)
                };
                MyMap.Pins.Add(pin);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latti, longi), Distance.FromKilometers(1)));

                var locat = new Location(latti, longi);
            //    var options = new MapLaunchOptions { Name = addr };
                var options = new MapLaunchOptions { NavigationMode = NavigationMode.None };

                await Map.OpenAsync(locat, options);

                var location = await Xamarin.Essentials.Geolocation.GetLocationAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
       
    }
}