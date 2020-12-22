using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TDFit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomMaster : MasterDetailPage
    {
        List<MenuItems> MenuItems;

        
        public CustomMaster()
        {
            InitializeComponent();
            MenuItems = new List<MenuItems>();

            MenuItems.Add(new MenuItems { OptionName = "Moje konto"});
            MenuItems.Add(new MenuItems { OptionName = "Wyloguj" });
            navigationList.ItemsSource = MenuItems;
            Detail = new NavigationPage(new MainPage());
         


        }

        private void Item_Tapped(object sender, ItemTappedEventArgs e)
        {
          
            try
            {
                var item = e.Item as MenuItems;

                switch (item.OptionName)
                {
                    case "Moje konto":
                        {
                            Detail.Navigation.PushAsync(new Profile());  
                            IsPresented = false;
                                                       
                        }
                        break;
                    case "Wyloguj":
                        {

                            Application.Current.MainPage = new NavigationPage(new LoginPage());
                            Application.Current.Properties["MyToken"] = "";
                            IsPresented = false; 
                        }
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    public class MenuItems
    {
        public string OptionName { get; set; }
    }
}