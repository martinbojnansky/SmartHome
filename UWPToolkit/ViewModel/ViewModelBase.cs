using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UWPToolkit.IoC;
using UWPToolkit.Navigation;

namespace UWPToolkit.ViewModel
{
    [DataContract]
    public abstract class ViewModelBase : PropertyChangedBase, IResolvable
    {
        public NavigationService Navigation { get; set; }
    }
}
