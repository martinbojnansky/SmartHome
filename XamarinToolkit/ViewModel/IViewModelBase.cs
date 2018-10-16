using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinToolkit.ViewModel
{
    public interface IViewModelBase
    {
        NavigationService Navigation { get; set; }

        Task OnAppearing();
    }
}