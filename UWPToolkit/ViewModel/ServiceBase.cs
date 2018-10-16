using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UWPToolkit.IoC;

namespace UWPToolkit.ViewModel
{
    [DataContract]
    public abstract class ServiceBase : PropertyChangedBase, IResolvable
    {
    }
}