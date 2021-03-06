using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using SmartMote.Controls;
using SmartMote.Droid.Controls;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SelectBox), typeof(SelectBoxRenderer))]
namespace SmartMote.Droid.Controls
{
    public class SelectBoxRenderer : PickerRenderer
    {
        private SelectBox _control;

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.OldElement == null && Element != null && e.NewElement is SelectBox)
            {
                _control = (SelectBox)e.NewElement;

                Control.SetBackground(Context.GetDrawable(Resource.Drawable.EntryBorder));
                Control.SetPadding(10,10,10,10);
                Control.TextSize = Convert.ToSingle(Device.GetNamedSize(NamedSize.Medium, _control));
            }
        }
    }
}