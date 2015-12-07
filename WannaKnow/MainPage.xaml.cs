using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WannaKnow.Resources;

namespace WannaKnow
{
    public partial class MainPage : PhoneApplicationPage
    {
        string[] namesplaces = new string[4] { "teste 1", "teste 2", "teste 3", "teste 4" };

        string[] descplaces = new string[4] { "place 1", "place 2", "place 3", "place 4" };

        public void firstmethod()
        {
            List<Resultclass> mylist = new List<Resultclass>();
            for (int i = 0; i < 4; i++)
            {
                Resultclass obj = new Resultclass();
                obj.name = namesplaces[i];
                obj.description = descplaces[i];
                mylist.Add(obj);
            }

            mylistbox.ItemsSource = mylist;
        }

        public void secondmethod()
        {

        }

        public class Resultclass
        {
            public string name { get; set; }
            public string description
            {
                get; set;
            }
           
        }

        // Constructor
        public MainPage()
        {
            
            InitializeComponent();
            firstmethod();
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void button_adicionar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddPlace.xaml", UriKind.Relative));
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}