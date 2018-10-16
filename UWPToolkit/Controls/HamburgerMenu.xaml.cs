using UWPToolkit.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UWPToolkit.Controls
{
    public sealed partial class HamburgerMenu : UserControl
    {
        public HamburgerMenu()
        {
            InitializeComponent();
            DataContext = this;
        }

        public NavigationLink[] NavigationLinks
        {
            get { return GetValue(NavigationLinksProperty) as NavigationLink[]; }
            set { SetValue(NavigationLinksProperty, value); }
        }

        public static readonly DependencyProperty NavigationLinksProperty =
              DependencyProperty.Register(
                  nameof(NavigationLinks), typeof(NavigationLink[]), typeof(HamburgerMenu), new PropertyMetadata(null)
                  );

        public SolidColorBrush HeaderBackground
        {
            get { return GetValue(HeaderBackgroundProperty) as SolidColorBrush; }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        public static readonly DependencyProperty HeaderBackgroundProperty =
              DependencyProperty.Register(
                  nameof(HeaderBackground), typeof(SolidColorBrush), typeof(HamburgerMenu), new PropertyMetadata(null)
                  );

        public SolidColorBrush HeaderForeground
        {
            get { return GetValue(HeaderForegroundProperty) as SolidColorBrush; }
            set { SetValue(HeaderForegroundProperty, value); }
        }

        public static readonly DependencyProperty HeaderForegroundProperty =
              DependencyProperty.Register(
                  nameof(HeaderForeground), typeof(SolidColorBrush), typeof(HamburgerMenu), new PropertyMetadata(null)
                  );

        public SolidColorBrush PaneBackground
        {
            get { return GetValue(PaneBackgroundProperty) as SolidColorBrush; }
            set { SetValue(PaneBackgroundProperty, value); }
        }

        public static readonly DependencyProperty PaneBackgroundProperty =
              DependencyProperty.Register(
                  nameof(PaneBackground), typeof(SolidColorBrush), typeof(HamburgerMenu), new PropertyMetadata(null)
                  );

        public SolidColorBrush PaneForeground
        {
            get { return GetValue(PaneForegroundProperty) as SolidColorBrush; }
            set { SetValue(PaneForegroundProperty, value); }
        }

        public static readonly DependencyProperty PaneForegroundProperty =
              DependencyProperty.Register(
                  nameof(PaneForeground), typeof(SolidColorBrush), typeof(HamburgerMenu), new PropertyMetadata(null)
                  );

        public FrameworkElement PaneBottomContent
        {
            get { return GetValue(PaneBottomContentProperty) as FrameworkElement; }
            set { SetValue(PaneBottomContentProperty, value); }
        }

        public static readonly DependencyProperty PaneBottomContentProperty =
              DependencyProperty.Register(
                  nameof(PaneBottomContent), typeof(FrameworkElement), typeof(HamburgerMenu), new PropertyMetadata(null)
                  );

        public FrameworkElement HeaderRightContent
        {
            get { return GetValue(HeaderRightContentProperty) as FrameworkElement; }
            set { SetValue(HeaderRightContentProperty, value); }
        }

        public static readonly DependencyProperty HeaderRightContentProperty =
              DependencyProperty.Register(
                  nameof(HeaderRightContent), typeof(FrameworkElement), typeof(HamburgerMenu), new PropertyMetadata(null)
                  );

        public SplitViewDisplayMode MobileDisplayMode
        {
            get { return (SplitViewDisplayMode)GetValue(MobileDisplayModeProperty); }
            set { SetValue(MobileDisplayModeProperty, value); }
        }

        public static readonly DependencyProperty MobileDisplayModeProperty =
              DependencyProperty.Register(
                  nameof(MobileDisplayMode), typeof(SplitViewDisplayMode), typeof(HamburgerMenu), new PropertyMetadata(SplitViewDisplayMode.Overlay, propertyChangedCallback: SplitViewDisplayModePropertyChangedCallback)
                  );

        public SplitViewDisplayMode TabletDisplayMode
        {
            get { return (SplitViewDisplayMode)GetValue(TabletDisplayModeProperty); }
            set { SetValue(TabletDisplayModeProperty, value); }
        }

        public static readonly DependencyProperty TabletDisplayModeProperty =
              DependencyProperty.Register(
                  nameof(TabletDisplayMode), typeof(SplitViewDisplayMode), typeof(HamburgerMenu), new PropertyMetadata(SplitViewDisplayMode.CompactOverlay, propertyChangedCallback: SplitViewDisplayModePropertyChangedCallback)
                  );

        public SplitViewDisplayMode DesktopDisplayMode
        {
            get { return (SplitViewDisplayMode)GetValue(DesktopDisplayModeProperty); }
            set { SetValue(DesktopDisplayModeProperty, value); }
        }

        public static readonly DependencyProperty DesktopDisplayModeProperty =
              DependencyProperty.Register(
                  nameof(DesktopDisplayMode), typeof(SplitViewDisplayMode), typeof(HamburgerMenu), new PropertyMetadata(SplitViewDisplayMode.CompactInline, propertyChangedCallback: SplitViewDisplayModePropertyChangedCallback)
                  );

        private static void SplitViewDisplayModePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HamburgerMenu hamburgerMenu = (HamburgerMenu)d;
        }

        private void HamburgerMenuNavigationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationLink navLink = HamburgerMenuNavigationListView.SelectedItem as NavigationLink;
            HamburgerMenuSplitView.Content = navLink.Control;

            if (HamburgerMenuSplitView.DisplayMode == SplitViewDisplayMode.Overlay || HamburgerMenuSplitView.DisplayMode == SplitViewDisplayMode.CompactOverlay)
            { 
                HamburgerMenuToggleButton.IsChecked = false;
            }
        }

        private void HamburgerMenuNavigationListView_Loaded(object sender, RoutedEventArgs e)
        {
            if (NavigationLinks != null && NavigationLinks.Count() > 0)
            {
                HamburgerMenuNavigationListView.SelectedIndex = 0;
            }
        }
    }
}
