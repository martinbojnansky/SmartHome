using SmartMote.Controls;
using SmartMote.UWP.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(TextBox), typeof(TextBoxRenderer))]
namespace SmartMote.UWP.Controls
{
    public class TextBoxRenderer : EntryRenderer
    {
        private TextBox _control;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.OldElement == null && Element != null && e.NewElement is TextBox)
            {
                _control = (TextBox)e.NewElement;
                _control.FontSize = Convert.ToSingle(Device.GetNamedSize(NamedSize.Default, _control));
            }
        }
    }
}