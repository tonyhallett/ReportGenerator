using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGeneratorCM
{
    [Export(typeof(ShellViewModel))]
    public class ShellViewModel : PropertyChangedBase
    {
    }
}
