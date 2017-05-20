using SmartMote.Controls;
using SmartMote.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmartMote.Views.Settings
{
    public partial class SettingsView : TabPage
    {
        public SettingsViewModel SettingsViewModel = ((App)Application.Current).IoCResolver.Resolve<SettingsViewModel>();

        public SettingsView()
        {
            InitializeComponent();

            BindingContext = SettingsViewModel;
        }
    }
}
