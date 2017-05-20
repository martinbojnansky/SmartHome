using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(SmartMote.Controls.Button), typeof(SmartMote.UWP.Controls.ButtonRenderer))]
namespace SmartMote.UWP.Controls
{
    public class ButtonRenderer : Xamarin.Forms.Platform.UWP.ButtonRenderer
    {
        private SmartMote.Controls.Button _control;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.OldElement == null && Element != null && e.NewElement is SmartMote.Controls.Button)
            {
                _control = (SmartMote.Controls.Button)e.NewElement;
                _control.BackgroundColor = Color.FromHex("#d2d2d2");
            }
        }
    }
}