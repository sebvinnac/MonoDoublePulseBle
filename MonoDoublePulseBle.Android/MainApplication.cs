using System;
using Android.App;
using Android.Runtime;
using Shiny;
using ShinyMod;

namespace MonoDoublePulseBle.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication() : base() { }
        public MainApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }


        public override void OnCreate()
        {
            base.OnCreate();
            AndroidShinyHost.Init(this, new MyShinyStartup(),
                services =>
                {
                    services.UseCurrentActivityPlugin();
                }
            );
        }
    }
}