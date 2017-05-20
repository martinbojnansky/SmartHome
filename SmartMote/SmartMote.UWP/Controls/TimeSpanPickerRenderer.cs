using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Xamarin.Forms;
using SmartMote.Controls;
using Xamarin.Forms.Platform.UWP;
using SmartMote.UWP.Controls;

[assembly: ExportRenderer(typeof(TimeSpanPicker), typeof(TimeSpanPickerRenderer))]
namespace SmartMote.UWP.Controls
{
    public class TimeSpanPickerRenderer : Xamarin.Forms.Platform.UWP.TimePickerRenderer
    {
        private TimeSpanPicker _control;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.OldElement == null && Element != null && e.NewElement is TimeSpanPicker)
            {
                _control = (TimeSpanPicker)e.NewElement;

                _control.HorizontalOptions = LayoutOptions.Start;
                if (_control.Is24Hour)
                {
                    Control.ClockIdentifier = "24HourClock";
                }
            }
        }
    }
}