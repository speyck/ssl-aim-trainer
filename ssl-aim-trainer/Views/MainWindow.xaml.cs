using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ssl_aim_trainer.Globals;
using ssl_aim_trainer.Classes;
using Memory;
using System.Threading;

namespace ssl_aim_trainer
{
    public partial class MainWindow : Window
    {
        private readonly Mem m = new();

        private readonly ProcessChecker Checker;

        public MainWindow()
        {
            InitializeComponent();

            Checker = new (PointerAddresses.ProcessID, memory: m);

            Checker.ProcessFound += Checker_ProcessFound;

            Checker.RunWorker();
        }

        private void Checker_ProcessFound(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                lblProcess.Content = "ProcessFound";
            });

            PointerAddressReader YPosReader = new (PointerAddresses.YPosPlayer, "float", 100, m);

            YPosReader.GotAddressValue += YPosReader_GotAddressValue;
        }

        private void YPosReader_GotAddressValue(object sender, GotAddressValueEventArgs e)
        {
            float value = (float)e.Value;
            lblPlayerY.Content = value.ToString();
        }
    }
}
