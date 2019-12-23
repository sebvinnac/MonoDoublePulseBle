using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Shiny;
using ShinyMod.Settings;

namespace ShinyMod
{
    public class CoreDelegateServices
    {
        public CoreDelegateServices(string x, string y, IAppSettings appSettings)
        {
            this.AppSettings = appSettings;
        }


        public IAppSettings AppSettings { get; }


        public async Task SendNotification(string title, string message, Expression<Func<IAppSettings, bool>> expression = null)
        {
            var notify = expression == null
                ? true
                : this.AppSettings.ReflectGet(expression);

            if (notify)
            { }
                //await this.Notifications.Send(title, message);
        }
    }
}
