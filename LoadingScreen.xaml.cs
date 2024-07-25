using System.Windows;

namespace Biomarkt_App_WPF
{
    /// <summary>
    /// Interaction logic for LoadingScreen.xaml
    /// </summary>
    public partial class LoadingScreen : Window
    {
        public LoadingScreen()
        {
            InitializeComponent();
            Loaded += LoadingScreen_Loaded;

        }

        private async void LoadingScreen_Loaded(object sender, RoutedEventArgs e)
        {
            await RunLoadingTaskAsync();
        }

        private async Task RunLoadingTaskAsync()
        {
            // Simulate a long-running task
            for (int i = 0; i <= 100; i++)
            {
                // Update the ProgressBar and TextBlock on the UI thread
                Dispatcher.Invoke(() =>
                {
                    loadingProgressBar.Value = i;
                    loadingPercentageText.Text = $"{i}%";
                });

                // Simulate some work 
                await Task.Delay(50);
            }

            this.Close();


        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void StartTimer()
        {

        }
    }
}
