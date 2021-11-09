using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace ReportGeneratorCM
{
    public class MEFBootstrapper : BootstrapperBase
    {
        private CompositionContainer compositionContainer;

        public MEFBootstrapper(bool useApplication = true) : base(useApplication) 
        {
            Initialize();
        }

        protected virtual bool AddEventAggregator { get; } = true;
        protected virtual bool AddWindowManager { get; } = true;

        protected virtual ComposablePartCatalog GetCatalog()
        {
            return new AssemblyCatalog(Assembly.GetExecutingAssembly());
        }
        protected override void Configure()
        {
            compositionContainer = new CompositionContainer(GetCatalog());
            if (AddWindowManager)
            {
                compositionContainer.ComposeExportedValue<IWindowManager>(new WindowManager());
            }
            if (AddEventAggregator)
            {
                compositionContainer.ComposeExportedValue<IEventAggregator>(new EventAggregator());
            }

            AdditionalConfiguration();
        }

        protected virtual void AdditionalConfiguration()
        {

        }

        protected override void BuildUp(object instance)
        {
            compositionContainer.SatisfyImportsOnce(instance);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return compositionContainer.GetExports(service, null, null).Select(l => l.Value);
        }

        protected override object GetInstance(Type service, string key)
        {
            return compositionContainer.GetExports(service, null, key).Single().Value;
        }
    }

    public abstract class MEFBootstrapperViewModel<TViewModel> : MEFBootstrapper
    {
        public MEFBootstrapperViewModel(bool useApplication = true) : base(useApplication) { }
        protected virtual IDictionary<string, object> ShellViewSettings { get; }
        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            await DisplayRootViewForAsync(typeof(TViewModel), ShellViewSettings);
        }
    }

    public class Bootstrapper : MEFBootstrapperViewModel<ShellViewModel>
    {
    }
}
