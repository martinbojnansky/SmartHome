using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPToolkit.IoC;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPToolkit.Navigation
{
    public abstract class NavigationService : IResolvable
    {
        private Frame _frame => (Frame)Window.Current.Content;

        public object NavigationParameter { get; private set; }

        public void Navigate(Type type, object parameter = null)
        {
            if (_frame != null)
            {
                NavigationParameter = parameter;
                _frame.Navigate(type, parameter);
            }
        }

        public void GoBack()
        {
            if (_frame != null && _frame.CanGoBack)
            {
                NavigationParameter = null;
                _frame.GoBack();
            }
        }
    }
}
