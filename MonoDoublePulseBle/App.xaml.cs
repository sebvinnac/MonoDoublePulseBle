using System;
using Shiny;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.DryIoc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShinyMod.Infrastructure;
using DryIoc;
using MonoDoublePulseBle.Views;
using ShinyMod;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace MonoDoublePulseBle
{
    public partial class App : PrismApplication
    {


        protected override async void OnInitialized()
        {
            this.InitializeComponent();
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                var viewModelTypeName = viewType.FullName.Replace("Page", "ViewModel");
                var viewModelType = Type.GetType(viewModelTypeName);
                return viewModelType;
            });
            await this.NavigationService.Navigate("Main/Nav/Welcome");
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.RegisterForNavigation<NavigationPage>("Nav");
            containerRegistry.RegisterForNavigation<MainPage>("Main");
            containerRegistry.RegisterForNavigation<AboutPage>("About");
            /*containerRegistry.RegisterForNavigation<WelcomePage>("Welcome");
            containerRegistry.RegisterForNavigation<DelegateNotificationsPage>("DelegateNotifications");

            containerRegistry.RegisterForNavigation<About.MainPage>("MotionActivity");

            containerRegistry.RegisterForNavigation<BluetoothLE.AdapterPage>("BleCentral");
            containerRegistry.RegisterForNavigation<BluetoothLE.PeripheralPage>("Peripheral");
            containerRegistry.RegisterForNavigation<BluetoothLE.GattServerPage>("GattServer");
            containerRegistry.RegisterForNavigation<BluetoothLE.CentralExtensionsPage>("BleExtensions");
            containerRegistry.RegisterForNavigation<BluetoothLE.PerformancePage>("BlePerformance");

            containerRegistry.RegisterForNavigation<Jobs.MainPage>("Jobs");
            containerRegistry.RegisterForNavigation<Jobs.CreatePage>("CreateJob");

            containerRegistry.RegisterForNavigation<Notifications.MainPage>("Notifications");
            */

        }


        protected override IContainerExtension CreateContainerExtension()
        {
            var container = new Container(this.CreateContainerRules());
            ShinyHost.Populate((serviceType, func, lifetime) =>
                container.RegisterDelegate(
                    serviceType,
                    _ => func(),
                    Reuse.Singleton // HACK: I know everything is singleton
                )
            );
            return new DryIocContainerExtension(container);
        }
    }
}
