using System.Threading.Tasks;
using System.Windows;
using Mmu.Rl.WpfUi.Domain.Services;
using Mmu.Rl.WpfUi.Ui.Services;

namespace Mmu.Rl.WpfUi
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Run(
                () =>
                {
                    Runner.Run(Cnv);
                });
        }

        private void SpeedUp_Click(object sender, RoutedEventArgs e)
        {
            SpeedManager.SpeedUp();
        }

        private void SpeedDown_Click(object sender, RoutedEventArgs e)
        {
            SpeedManager.SlowDown();
        }

        private void MaxSpeed_Click(object sender, RoutedEventArgs e)
        {
            SpeedManager.MaxSpeed();
        }

        private void MinSpeed_Click(object sender, RoutedEventArgs e)
        {
            SpeedManager.MinSpeed();
        }
    }
}