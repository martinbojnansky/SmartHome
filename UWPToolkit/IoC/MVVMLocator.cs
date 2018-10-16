using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UWPToolkit.IoC
{
    public class MVVMLocator
    {
        private IContainer _container;

        public MVVMLocator(Assembly assembly)
        {
            _container = new IoCBuilder().BuildContainer(assembly);
        }

        public MVVMLocator(IEnumerable<Assembly> assemblies)
        {
            _container = new IoCBuilder().BuildContainer(assemblies);
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
