using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XamarinToolkit.ViewModel;

namespace XamarinToolkit.IoC
{
    public class IoCResolver
    {
        private IContainer _container;

        public void BuildContainer(params Assembly[] assemblies)
        {
            _container = new IoCBuilder().BuildContainer(this, assemblies);
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public TViewModel ResolveViewModel<TViewModel>() where TViewModel : IViewModelBase
        {
            return _container.Resolve<TViewModel>();
        }

        public ViewModelPage ResolveViewModelPage<TViewModel>(TViewModel viewModel) where TViewModel : IViewModelBase
        {
            var viewTypeName = (viewModel?.GetType() ?? typeof(TViewModel)).AssemblyQualifiedName.Replace("Model", "");
            var viewType = Type.GetType(viewTypeName);

            if (viewType != null)
            {
                var view = _container.Resolve(viewType) as ViewModelPage;
                view.BindingContext = viewModel;

                return view;
            }

            return null;
        }
    }
}