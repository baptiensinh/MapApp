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
            if (db.InsertNode(node) == true && db.InsertPlace(place) == true)
                DisplayAlert("Notification", "Latitude: " + position.Latitude.ToString() + "\nLongitude: " + position.Longitude.ToString() + "\nAddress: " + address, "OK");
            

            latti = position.Latitude;
            longi = position.Longitude;

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
                if (latti != null) DisplayAlert("sdfsdf", "dxc" + latti, "ok");
                else
                    DisplayAlert("xbjsh", "null", "ok");
                /*
                Geocoder geoCoder = new Geocoder();

                IEnumerable<Position> approximateLocations = await geoCoder.GetPositionsForAddressAsync(Des.Text);
                Position position = approximateLocations.FirstOrDefault();
                //string coordinates = $"{position.Latitude}, {position.Longitude}";
                
                IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
                string address = possibleAddresses.FirstOrDefault();
                */
                
                foreach (var place in "qlDes.db")
                {
                    
                    Position Position = new Position(latti, longi);
                }
                MyMap.ItemsSource = listPlace;

                var location = await Xamarin.Essentials.Geolocation.GetLocationAsync();
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromKilometers(1)));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
       
    }
}

/*
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("MapApp.Places.json");
                string text = string.Empty;
                using (var reader = new StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }

                var resultObject = JsonConvert.DeserializeObject<Places>(text);

                foreach (var place in resultObject.results)
                {
                    placesList.Add(new Place
                    {
                        PlaceName = place.name,
                        Address = place.vicinity,
                        Location = place.geometry.location,
                        Position = new Position(place.geometry.location.lat, place.geometry.location.lng),
                        Icon = place.icon,
                    });
                }

                MyMap.ItemsSource = placesList;
                //PlacesListView.ItemsSource = placesList;
                */
