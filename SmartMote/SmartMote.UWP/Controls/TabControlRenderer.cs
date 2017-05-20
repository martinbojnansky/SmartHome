using SmartMote.Controls;
using SmartMote.UWP.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(TabControl), typeof(TabControlRenderer))]
namespace SmartMote.UWP.Controls
{
    public class TabControlRenderer : TabbedPageRenderer
    {
        private TabControl _control;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.OldElement == null && Element != null && e.NewElement is TabControl)
            {
                _control = (TabControl)e.NewElement;

                _control.BarBackgroundColor = Color.FromHex("#eeeeee");
            }
        }
    }
}