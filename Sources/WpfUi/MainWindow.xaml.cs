using System.Threading.Tasks;
using System.Windows;
using Mmu.Rl.WpfUi.Domain.Services;

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
    }
}