using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartMote.Controls
{
    public class TimeSpanPicker : TimePicker
    {
        public static readonly BindableProperty Is24HourProperty = BindableProperty.Create(nameof(Is24Hour), typeof(bool), typeof(TextBox), true);

        public bool Is24Hour
        {
            get
            {
                return (bool)GetValue(Is24HourProperty);
            }
            set
            {
                SetValue(Is24HourProperty, value);
            }
        }
    }
}
