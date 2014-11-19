using System.Web.Http;
using System.Web.Mvc;
using StructureMap;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Coupling.Web.App_Start.StructuremapMvc), "Start")]

namespace Coupling.Web.App_Start {
    public static class StructuremapMvc {
        public static void Start() {
            var container = (IContainer) IoC.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new SmDependencyResolver(container);
        }
    }
}