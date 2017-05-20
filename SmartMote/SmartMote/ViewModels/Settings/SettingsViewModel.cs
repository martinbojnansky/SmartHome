using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinToolkit.Storage;
using XamarinToolkit.ViewModel;

namespace SmartMote.ViewModels.Settings
{
    public class SettingsViewModel : ViewModelBase
    {
        public LocalSettings LocalSettings { get; set; }

        public string IPAddress
        {
            get
            {
                return (string)LocalSettings.TryGetValue(nameof(IPAddress));
            }
            set
            {
                LocalSettings.SetValue(nameof(IPAddress), value);
            }
        }
    }
}