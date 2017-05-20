using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using SmartMote.Controls;
using Xamarin.Forms.Platform.UWP;
using SmartMote.UWP.Controls;

[assembly: ExportRenderer(typeof(SelectBox), typeof(SelectBoxRenderer))]
namespace SmartMote.UWP.Controls
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
            }
        }
    }
}