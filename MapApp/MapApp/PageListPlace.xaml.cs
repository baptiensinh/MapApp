using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageListPlace : ContentPage
	{
		public PageListPlace ()
		{
            Database db;
            List<Place> DSPlace;

            InitializeComponent();

            db = new Database();
            DSPlace = db.selectPlace();
            ListPlace.ItemsSource = DSPlace;
        }
    }
}