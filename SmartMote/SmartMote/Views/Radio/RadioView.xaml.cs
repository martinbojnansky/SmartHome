using SmartMote.Controls;
using SmartMote.ViewModels.Radio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmartMote.Views.Radio
{
    public partial class RadioView : TabPage
    {
        public RadioViewModel RadioViewModel = ((App)Application.Current).IoCResolver.Resolve<RadioViewModel>();

        public RadioView()
        {
            InitializeComponent();

            BindingContext = RadioViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await RadioViewModel.RefreshAsync();
        }
    }
}
