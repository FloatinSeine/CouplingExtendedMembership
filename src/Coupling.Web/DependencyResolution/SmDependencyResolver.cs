using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Coupling.Web.DependencyResolution;
using StructureMap;

namespace Coupling.Web
{
    public class SmDependencyResolver : StructureMapDependencyScope, IDependencyResolver
    {

        //private readonly IContainer _container;

        public SmDependencyResolver(IContainer container) : base(container) {
            //_container = container;
        }

        /// <summary>
        /// The begin scope.
        /// </summary>
        /// <returns>
        /// The System.Web.Http.Dependencies.IDependencyScope.
        /// </returns>
        public IDependencyScope BeginScope()
        {
            var child = this.Container.GetNestedContainer();
            return new SmDependencyResolver(child);
        }

        /*public object GetService(Type serviceType) {
            if (serviceType == null) return null;
            try {
                  return serviceType.IsAbstract || serviceType.IsInterface
                           ? _container.TryGetInstance(serviceType)
                           : _container.GetInstance(serviceType);
            }
            catch {

                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }*/
    }
}