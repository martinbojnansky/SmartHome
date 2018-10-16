using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinToolkit.IoC;

namespace XamarinToolkit.ViewModel
{
    public class NavigationService : ServiceBase
    {
        private INavigation _navigation => (Application.Current)?.MainPage?.Navigation;
        public IoCResolver _resolver;

        public NavigationService(IoCResolver resolver)
        {
            _resolver = resolver;
        }

        public TViewModel ResolveViewModel<TViewModel>() where TViewModel : IViewModelBase => _resolver.ResolveViewModel<TViewModel>();

        public async Task PushAsync<TViewModel>(bool animated = true) where TViewModel : IViewModelBase
        {
            TViewModel viewModel = _resolver.ResolveViewModel<TViewModel>();
            ViewModelPage view = _resolver.ResolveViewModelPage(viewModel);

            if (view != null)
            {
                await _navigation.PushAsync(view, animated);
            }
        }

        public async Task PushAsync<TViewModel>(TViewModel viewModel, bool animated = true) where TViewModel : IViewModelBase
        {
            ViewModelPage view = _resolver.ResolveViewModelPage(viewModel);

            if (view != null)
            {
                await _navigation.PushAsync(view, animated);
            }
        }

        public async Task PopAsync()
        {
            await _navigation.PopAsync();
        }
    }
}