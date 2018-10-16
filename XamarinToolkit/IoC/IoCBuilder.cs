using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XamarinToolkit.IoC
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

        public IContainer BuildContainer(IoCResolver resolver, IEnumerable<Assembly> assemblies)
        {
            _builder = new ContainerBuilder();

            RegisterTypes(typeof(XamarinToolkit).GetTypeInfo().Assembly);

            foreach (var assembly in assemblies)
            {
                RegisterTypes(assembly);
            }

            _builder.RegisterInstance(resolver).As<IoCResolver>();

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