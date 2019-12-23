using System;
using System.Threading.Tasks;
using Shiny.BluetoothLE.Central;
using Shiny;
using ShinyMod.Models;

namespace ShinyMod
{
    public class BleDelegate : IBleCentralDelegate
    {
        readonly CoreDelegateServices services;
        public BleDelegate(CoreDelegateServices services) => this.services = services;


        public async Task OnAdapterStateChanged(AccessState state)
        {
            if (state != AccessState.Available && this.services.AppSettings.UseNotificationsBle)
                await this.services.SendNotification("BLE State", "Turn on Bluetooth already");
        }

        public Task OnConnected(IPeripheral peripheral)
        {
            return Task.FromResult(0);
        }

        public void Nothing()
        {

        }


        /*public Task OnConnected(IPeripheral peripheral) => Task.WhenAll(
            this.services.Connection.InsertAsync(new BleEvent
            {
                Timestamp = DateTime.Now
            }),
            this.services.SendNotification(
                "BluetoothLE Device Connected",
                $"{peripheral.Name} has connected",
                x => x.UseNotificationsBle
            )
        );*/
    }
}
