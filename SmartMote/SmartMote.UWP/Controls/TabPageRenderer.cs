using SmartMote.Controls;
using SmartMote.UWP.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(TabPage), typeof(TabPageRenderer))]
namespace SmartMote.UWP.Controls
{
    public class TabPageRenderer : PageRenderer
    {
        private TabPage _control;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        { 
            base.OnElementChanged(e);

            if (e.NewElement is TabPage)
            {
                _control = (TabPage)e.NewElement;

                _control.Content.Margin = new Thickness(-12, 0, -12, 0);
            }
        }
    }
}