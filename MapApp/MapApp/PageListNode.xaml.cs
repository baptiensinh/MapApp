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
	public partial class PageListNode : ContentPage
	{
        
        public PageListNode ()
		{
            Database db;
            List<Node> DSNode;

            InitializeComponent();

            db = new Database();
            DSNode = db.SelectNode();
            ListNode.ItemsSource = DSNode;
        }
	}
}