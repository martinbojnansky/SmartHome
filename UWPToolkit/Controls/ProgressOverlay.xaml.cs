using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using UWPToolkit.IoC;
using UWPToolkit.ViewModel;

namespace UWPToolkit.Controls
{
    public sealed partial class ProgressOverlay : UserControl
    {
        public ProgressOverlay()
        {
            this.InitializeComponent();
        }
        
        public ProgressObject ProgressObject
        {
            get { return GetValue(ProgressObjectProperty) as ProgressObject; }
            set { SetValue(ProgressObjectProperty, value); }
        }

        public static readonly DependencyProperty ProgressObjectProperty =
              DependencyProperty.Register(
                  nameof(ProgressObject), typeof(ProgressObject), typeof(HamburgerMenu), new PropertyMetadata(new ProgressObject())
                  );
    }

    public class ProgressObject : ModelBase
    {
        private bool _isActive = false;
        public bool IsActive { get { return _isActive; } set { _isActive = value; RaisePropertyChanged(nameof(IsActive)); } }

        private string _text = "";
        public string Text { get { return _text; } set { _text = value; RaisePropertyChanged(nameof(Text)); } }

        private CancellationTokenSource _cancellationToken;
        public CancellationTokenSource CancellationToken { get { return _cancellationToken; } set { _cancellationToken = value; RaisePropertyChanged(nameof(CancellationToken)); RaisePropertyChanged(nameof(IsCancellable)); } }

        public bool IsCancellable { get { if (_cancellationToken == null) return false; else return true; } }

        public void Show(string text = "", CancellationTokenSource cts = null)
        {
            IsActive = true;
            Text = text;
            CancellationToken = cts;
        }

        public void Hide()
        {
            IsActive = false;
        }

        public void Cancel()
        {
            try
            {
                CancellationToken.Cancel();
                Hide();
            }
            catch { }
        }
    }
}
