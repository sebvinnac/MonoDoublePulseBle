using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShinyMod;
using Shiny;
using MonoDoublePulseBle.Models;
using MonoDoublePulseBle.Views;
using MonoDoublePulseBle.ViewModels;
using System.Reactive.Linq;
using System.Reactive.Disposables;
//using Acr.UserDialogs.Forms;

namespace MonoDoublePulseBle.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        IDisposable scanner;
        Shiny.BluetoothLE.Central.ICentralManager central;
        public ObservableList<PeripheralItemViewModel> Peripherals { get; } = new ObservableList<PeripheralItemViewModel>();

        public ItemsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Item item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        public void AddItem_Clicked(object sender, EventArgs e)
        {



            if (CrossBle.Central.IsScanning)
            {
                DisplayAlert("true", "true", "Ok");
            }
            else
            {
                DisplayAlert("false", "false", "Ok");
            }

            //await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
            scanner = CrossBle.Central.Scan().Subscribe(scanResult =>
            {
                DisplayAlert("Reçu",scanResult.ToString(),"Ok");
                // do something with it
                // the scanresult contains the device, RSSI, and advertisement packet
                //MessagingCenter.Send(this, "AddItem", Item);
                //await Navigation.PopModalAsync();
            });


            if(CrossBle.Central.IsScanning)
            {
                DisplayAlert("true", "true", "Ok");
            }
            else
            {
                DisplayAlert("false","false", "Ok");
            }
            //scanner.Dispose(); // to stop scanning
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}