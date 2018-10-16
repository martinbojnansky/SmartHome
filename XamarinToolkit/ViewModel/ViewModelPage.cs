using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinToolkit.IoC;
using XamarinToolkit.ViewModel;

namespace XamarinToolkit.ViewModel
{
    public abstract class ViewModelPage : ContentPage, IResolvable
    {
        public ViewModelPage() : base()
        {
        }

        protected async override void OnAppearing()
        {
            await ((IViewModelBase)BindingContext)?.OnAppearing();
            base.OnAppearing();
        }
    }
}