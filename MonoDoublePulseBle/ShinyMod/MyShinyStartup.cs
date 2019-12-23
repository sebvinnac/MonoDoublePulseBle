//#define STARTUP_ATTRIBUTES
//#define STARTUP_AUTO

using Shiny;
using Shiny.BluetoothLE;
using Shiny.Logging;
using Microsoft.Extensions.DependencyInjection;

#if STARTUP_ATTRIBUTES
//[assembly: ShinySqliteIntegration(true, true, true, true, true)]
//[assembly: ShinyJob(typeof(SampleJob), "MyIdentifier", BatteryNotLow = true, DeviceCharging = false, RequiredInternetAccess = Shiny.Jobs.InternetAccess.Any)]
[assembly: ShinyAppCenterIntegration(Constants.AppCenterTokens, true, true)]
[assembly: ShinyService(typeof(SampleSqliteConnection))]
[assembly: ShinyService(typeof(GlobalExceptionHandler))]
[assembly: ShinyService(typeof(CoreDelegateServices))]
[assembly: ShinyService(typeof(JobLoggerTask))]
[assembly: ShinyService(typeof(IUserDialogs), typeof(UserDialogs))]
[assembly: ShinyService(typeof(IFullService), typeof(FullService))]
[assembly: ShinyService(typeof(IAppSettings), typeof(AppSettings))]

#if !STARTUP_AUTO
[assembly: ShinyNotifications(typeof(NotificationDelegate), true)]
[assembly: ShinyBeacons(typeof(BeaconDelegate))]
[assembly: ShinyBleCentral(typeof(BleCentralDelegate))]
[assembly: ShinyGps(typeof(LocationDelegates))]
[assembly: ShinyGeofences(typeof(LocationDelegates))]
[assembly: ShinyMotionActivity]
[assembly: ShinySensors]
[assembly: ShinyHttpTransfers(typeof(HttpTransferDelegate))]
[assembly: ShinySpeechRecognition]
#endif
#endif

namespace ShinyMod
{
    public class MyShinyStartup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection builder)
        {
            // for general client functionality
            builder.UseBleCentral();

            // for client functionality in the background
            builder.UseBleCentral<BleDelegate>();

            // for GATT server
            builder.UseBlePeripherals();
            // mix and match however you see fit
            builder.UseSqliteLogging(true, false);
            builder.UseSqliteCache();
            builder.UseSqliteSettings();
            builder.UseSqliteStorage();

            //builder.UseAppCenterLogging(Constants.AppCenterTokens, true, false);

            // custom logging
            Log.UseConsole();
            Log.UseDebug();

            // create your infrastructure

#if STARTUP_ATTRIBUTES
            services.RegisterModule(new AssemblyServiceModule());
#if STARTUP_AUTO
            services.RegisterModule(new AutoRegisterModule());
#endif
#endif
        }
    }
}
