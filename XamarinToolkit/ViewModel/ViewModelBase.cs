using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinToolkit.ViewModel
{
    [DataContract]
    public abstract class ViewModelBase : PropertyChangedBase, IViewModelBase
    {
        public NavigationService Navigation { get; set; }

        public virtual async Task OnAppearing()
        {
        }
    }
}