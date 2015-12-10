using System;
using System.Net;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Windows.Devices.Geolocation;
using System.IO.IsolatedStorage;

namespace WannaKnow
{
    public partial class AddPlace : PhoneApplicationPage
    {

        string url = "http://google.com";
        String clienId = "3CX0442VU1HVADWPIFRWRKIANJ0CYDSXPPNQV3QLV0R51K3I";
        String clienSecret = "GGS0VL53WWV11BBL0NSBWFCUVXRYBKPL503ZPGWNNNGBGII4";
        String v = "20130815";
        String urlFormat = "https://api.foursquare.com/v2/venues/search?client_id={0}&client_secret={1}&v={2}&ll={3},{4}";

        public AddPlace()
        {
            InitializeComponent();   
        }

        private async void OneShotLocation_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"] != true)
            {
                return;
            }

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)
                System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 10;

            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10));

                string urlFilled = String.Format(urlFormat,
                    clienId,
                    clienSecret,
                    v,
                    geoposition.Coordinate.Latitude,
                    geoposition.Coordinate.Longitude);

                //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(urlFilled);
                //request.Method = "GET";
                //request.BeginGetResponse(GetListPlacesCallback, request);

                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(GetListPlacesCallback);
                webClient.DownloadStringAsync(new Uri(urlFilled));

                LatitudeTextBlock.Text = geoposition.Coordinate.Latitude.ToString("0.0000000");
                LongitudeTextBlock.Text = geoposition.Coordinate.Longitude.ToString("0.0000000");
            }
            catch (Exception ex)
            {
                if ((uint) ex.HResult == 0x80004004)
                {
                    StatusTextBlock.Text = "location is disabled in phone settings";
                }
                else
                {
                    
                }
            }

        }

        void GetListPlacesCallback(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                StatusTextBlock.Text = e.Result;

                
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
            {
                return;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("This app accesses yout phone's location. Is that ok?",
                    "Locatoin",
                    MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = true;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = false;
                }
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }




    }

}