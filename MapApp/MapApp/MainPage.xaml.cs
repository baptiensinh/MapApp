using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;

namespace MapApp
{   
    
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
       // Double lat, lng;
        public  MainPage()
        {
            InitializeComponent();
          //  Map map = new Map();
           // Content = map;
             Task.Delay(2000);
              UpdateMap();

            /*      Pin pin = new Pin
                  {
                      Label = "Santa Cruz",
                      Address = "The city with a boardwalk",
                      Type = PinType.Place,
                      Position = new Position(lat ,lng )
                  }; map.Pins.Add(pin); */
            var monkeyList = new List<string>();
            monkeyList.Add("Street");
            monkeyList.Add("Satellite");
            monkeyList.Add("Hybrid");

            var picker = new Picker { Title = "Map Type", TitleColor = Color.MidnightBlue };
            picker.ItemsSource = monkeyList;
            var Mapty = new Map();
            Mapty.SetBinding(Map.BindingContextProperty, new Binding("SelectedItem", source: picker));
        }





           List<Place> placesList = new List<Place>();

       private async void UpdateMap()
        {
            try
            {
                

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
                         //Icon = place.icon,
                         //Distance = $"{GetDistance(lat1, lon1, place.geometry.location.lat, place.geometry.location.lng, DistanceUnit.Kiliometers).ToString("N2")}km",
                         //OpenNow = GetOpenHours(place?.opening_hours?.open_now)
                     });
                 }

                 map.ItemsSource = placesList;
                 //PlacesListView.ItemsSource = placesList;
                 var loc = await Xamarin.Essentials.Geolocation.GetLocationAsync();
                 map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(loc.Latitude, loc.Longitude), Distance.FromKilometers(1)));
 
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                Mapty.Text = (string)picker.ItemsSource[selectedIndex];
            }
        }

    }
}
