namespace WebKiosk
{
    using System.Windows;
    using System.Windows.Input;

    public partial class KioskWindow : Window
    {
        private readonly ExecutionStateAdapter _executionStateAdapter = new();

        public KioskWindow()
        {
            InitializeComponent();

            PreviewKeyDown += new KeyEventHandler(HandleEscape);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _executionStateAdapter.RequireDisplayOn();
        }

        private void MainWindow_OnUnloaded(object sender, RoutedEventArgs e)
        {
            _executionStateAdapter.Shutdown();
        }

        private void HandleEscape(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            { 
                Close();
            }
        }
    }
}