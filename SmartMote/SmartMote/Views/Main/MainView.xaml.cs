using SmartMote.Controls;
using SmartMote.ViewModels.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmartMote.Views.Main
{
    public partial class MainView : TabControl
    {
        public MainViewModel MainViewModel = ((App)Application.Current).IoCResolver.Resolve<MainViewModel>();

        public MainView()
        {
            InitializeComponent();

            BindingContext = MainViewModel;
        }
    }
}
