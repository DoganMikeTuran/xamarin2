using Minutes.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Minutes
{
    
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        bool onOff = true;

        public MainPage()
        {
            InitializeComponent();
            entries.ItemTapped += OnItemTapped;
            newEntry.Completed += OnAddNewEntry;
        }

        private async void OnAddNewEntry(object sender, EventArgs e)
        {
            string text = newEntry.Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                NoteEntry item = new NoteEntry { Title = text };
                await App.Entries.AddAsync(item);
                await Navigation.PushAsync(new NoteEntryEditPage(item));
                newEntry.Text = string.Empty;
            }
        }



        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            NoteEntry item = e.Item as NoteEntry;
            await Navigation.PushAsync(new NoteEntryEditPage(item));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            entries.ItemsSource = await App.Entries.GetAllAsync();
        }
        
        private async void flashOnOff(object sender, EventArgs e) {

            // Turn On
            //await Flashlight.TurnOnAsync();
            if (onOff)
            {
                await Flashlight.TurnOnAsync();
                onOff = false;

            }
            else
            {

                await Flashlight.TurnOffAsync();
                onOff = true;
            }
        }
        private void Vibrate(object sender, EventArgs e)
        {

            // Or use specified time
            var duration = TimeSpan.FromSeconds(1);
            Vibration.Vibrate(duration);



        }
        public async void Location(object sender, EventArgs e) {
            
               
                //  await DisplayAlert("Location",$"Latitude: {location.Latitude}, Longitude: {location.Longitude} , Altitude: {location.Altitude}", "OK");
                var location = new Location( 55.770146, 12.512141);
                
                await Map.OpenAsync(location);

            



        }
    }
}
