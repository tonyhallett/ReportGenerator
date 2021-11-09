namespace ReportGenerator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Windows.Threading;

    [Export(typeof(IUIRefresher))]
    public class UIRefresher : IUIRefresher
    {
        private static readonly Action EmptyDelegate = () => { };

        public void Refresh()
        {
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }
}
