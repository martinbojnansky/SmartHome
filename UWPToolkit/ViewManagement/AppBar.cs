using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace UWPToolkit.ViewManagement
{
    public static class AppBar
    {
        public static void CustomizeDesktopAppBar(Color backgroundColor, Color highlightColor, Color foregroundColor, Color accentColor)
        {
            if (ApiInformation.IsTypePresent(typeof(ApplicationView).FullName))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;

                if (titleBar != null)
                {
                    titleBar.BackgroundColor = backgroundColor;
                    titleBar.ForegroundColor = foregroundColor;
                    titleBar.InactiveBackgroundColor = highlightColor;
                    titleBar.InactiveForegroundColor = foregroundColor;
                    titleBar.ButtonBackgroundColor = backgroundColor;
                    titleBar.ButtonForegroundColor = foregroundColor;
                    titleBar.ButtonHoverBackgroundColor = highlightColor;
                    titleBar.ButtonHoverForegroundColor = foregroundColor;
                    titleBar.ButtonPressedBackgroundColor = accentColor;
                    titleBar.ButtonPressedForegroundColor = foregroundColor;
                    titleBar.ButtonInactiveBackgroundColor = highlightColor;
                    titleBar.ButtonInactiveForegroundColor = foregroundColor;
                }
            }
        }

        public static void CustomizeMobileAppBar(
            Color backgroundColor, Color foregroundColor,
            bool isVisible = true, double backgroundOpacity = 1)
        {
            if (ApiInformation.IsTypePresent(typeof(StatusBar).FullName))
            {
                var statusBar = StatusBar.GetForCurrentView();

                if (statusBar != null)
                {
                    statusBar.BackgroundColor = backgroundColor;
                    statusBar.ForegroundColor = foregroundColor;
                    statusBar.BackgroundOpacity = backgroundOpacity;

                    if (isVisible)
                        ShowMobileAppBar();
                    else
                        HideMobileAppBar();
                }
            }
        }

        public static async void ShowMobileAppBar()
        {
            if (ApiInformation.IsTypePresent(typeof(StatusBar).FullName))
            {
                var statusBar = StatusBar.GetForCurrentView();

                if (statusBar != null)
                {
                    await statusBar.ShowAsync();
                }
            }
        }

        public static async void HideMobileAppBar()
        {
            if (ApiInformation.IsTypePresent(typeof(StatusBar).FullName))
            {
                var statusBar = StatusBar.GetForCurrentView();

                if (statusBar != null)
                {
                    await statusBar.HideAsync();
                }
            }
        }

        public static async void ShowMobileAppBarProgressIndicator(string text = "")
        {
            if (ApiInformation.IsTypePresent(typeof(StatusBar).FullName))
            {
                var statusBar = StatusBar.GetForCurrentView();

                if (statusBar != null)
                {
                    StatusBarProgressIndicator progressIndicator = statusBar.ProgressIndicator;

                    if (!String.IsNullOrEmpty(text))
                        progressIndicator.Text = text;

                    await progressIndicator.ShowAsync();
                }
            }
        }

        public static async void HideMobileAppBarProgressIndicator()
        {
            if (ApiInformation.IsTypePresent(typeof(StatusBar).FullName))
            {
                var statusBar = StatusBar.GetForCurrentView();

                if (statusBar != null)
                {
                    await statusBar.ProgressIndicator.HideAsync();
                }
            }
        }
    }
}
