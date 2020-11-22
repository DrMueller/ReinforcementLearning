using System;
using System.Threading.Tasks;
using System.Windows;
using Mmu.Rl.WpfUi.Services;

namespace Mmu.Rl.WpfUi
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override async void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            await Task.Run(() =>
            {
                Runner.Run(Cnv);
            });
        }
    }
}