using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UWPToolkit.IoC
{
    public interface IResolvable { }
    public interface ISingleResolvable { }

    public class IoCBuilder
    {
        private ContainerBuilder _builder = new ContainerBuilder();

        public IContainer BuildContainer(Assembly assembly)
        {
            _builder = new ContainerBuilder();

            RegisterTypes(assembly);

            return _builder.Build();
        }

        public IContainer BuildContainer(IEnumerable<Assembly> assemblies)
        {
            _builder = new ContainerBuilder();

            foreach (var assembly in assemblies)
            {
                RegisterTypes(assembly);
            }

            return _builder.Build();
        }

        public virtual void RegisterTypes(Assembly assembly)
        {
            Register<IResolvable>(assembly);
            RegisterSingle<ISingleResolvable>(assembly);
        }

        public void Register<T>(Assembly assembly)
        {
            _builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsAssignableTo<T>() && !t.IsAssignableTo<ISingleResolvable>())
                .PropertiesAutowired();
        }

        public void RegisterSingle<T>(Assembly assembly)
        {
            _builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsAssignableTo<T>()).SingleInstance()
                .PropertiesAutowired();
        }
    }
}
