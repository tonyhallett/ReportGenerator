namespace ReportGenerator
{
    using System.ComponentModel.Composition;

    public interface IDialogViewModel
    {
        string Title { get; }
    }

    public interface IDialogService
    {
        bool? Show(IDialogViewModel viewModel);
    }

    [Export(typeof(IDialogService))]
    public class DialogService : IDialogService
    {
        public bool? Show(IDialogViewModel viewModel)
        {
            var dialogWindow = new DialogWindow
            {
                DataContext = viewModel,
            };
            return dialogWindow.ShowDialog();
        }
    }
}
