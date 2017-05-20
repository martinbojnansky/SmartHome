using SmartMote.Controls;
using SmartMote.ViewModels.Alarm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmartMote.Views.Alarm
{
    public partial class AlarmView : TabPage
    {
        public AlarmViewModel AlarmViewModel = ((App)Application.Current).IoCResolver.Resolve<AlarmViewModel>();

        public AlarmView()
        {
            InitializeComponent();

            BindingContext = AlarmViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await AlarmViewModel.RefreshAsync();
        }
    }
}
