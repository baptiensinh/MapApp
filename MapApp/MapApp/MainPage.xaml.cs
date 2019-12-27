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

namespace MapApp
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SearchHistory_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageListNode());
        }

        private void DestinationHistory_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageListNode());
        }

        private void GoToMap_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageGoToMap());
        }
    }
}
