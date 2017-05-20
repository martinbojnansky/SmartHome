using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using SmartMote.Controls;
using SmartMote.Droid.Controls;

[assembly: ExportRenderer(typeof(TimeSpanPicker), typeof(TimeSpanPickerRenderer))]
namespace SmartMote.Droid.Controls
{
    public class TimeSpanPickerRenderer : Xamarin.Forms.Platform.Android.TimePickerRenderer, Android.Views.View.IOnClickListener
    {
        private TimeSpanPicker _control;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.OldElement == null && Element != null && e.NewElement is TimeSpanPicker)
            {
                _control = (TimeSpanPicker)e.NewElement;

                Control.SetBackground(Context.GetDrawable(Resource.Drawable.EntryBorder));
                Control.SetPadding(10,10,10,10);
                Control.TextSize = Convert.ToSingle(Device.GetNamedSize(NamedSize.Medium, _control));
                Control.SetOnClickListener(this);
            }
        }

        public void OnClick(global::Android.Views.View v)
        {
            var dialog = new TimePickerDialog(Context, new EventHandler<TimePickerDialog.TimeSetEventArgs>(OnTimeSet), _control.Time.Hours, _control.Time.Minutes, _control.Is24Hour);
            dialog.SetCustomTitle(new TextView(Context) { Visibility = ViewStates.Gone });
            dialog.Show();
        }

        private void OnTimeSet(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            _control.Time = new TimeSpan(e.HourOfDay, e.Minute, 0);
            Control.Text = DateTime.Today.Add(_control.Time).ToString(_control.Format);
        }
    }
}