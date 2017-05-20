using SmartMote.Controls;
using SmartMote.ViewModels.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmartMote.Views.Devices
{
    public partial class DevicesView : TabPage
    {
        public DevicesViewModel DevicesViewModel = ((App)Application.Current).IoCResolver.Resolve<DevicesViewModel>();

        public DevicesView()
        {
            InitializeComponent();

            BindingContext = DevicesViewModel;
        }
    }
}
