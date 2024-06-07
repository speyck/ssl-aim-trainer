using System.Windows;
using ssl_aim_trainer.Globals;
using ssl_aim_trainer.Classes;
using Memory;

namespace ssl_aim_trainer
{
    public partial class MainWindow : Window
    {
        private readonly Mem m = new();

        private readonly ProcessChecker Checker;

        private PointerAddressReader YPosReader;

        public MainWindow()
        {
            InitializeComponent();

            Checker = new (PointerAddresses.ProcessID, m);

            Checker.ProcessFound += Checker_ProcessFound;
            Checker.ProcessClosed += Checker_ProcessClosed;

            Checker.Start();
        }

        private void Checker_ProcessFound(object sender, ProcessCheckerEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                lblProcess.Content = $"Process \"{e.ProcessID}\" found";
            });

            m.OpenProcess(e.ProcessID);

            YPosReader = new (PointerAddresses.YPosPlayer, "float", 100, m);

            YPosReader.GotAddressValue += YPosReader_GotAddressValue;

            YPosReader.Start();
        }

        private void Checker_ProcessClosed(object sender, ProcessCheckerEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                lblProcess.Content = $"No process found";
            });

            m.CloseProcess();

            YPosReader.GotAddressValue -= YPosReader_GotAddressValue;

            YPosReader.Stop();
        }

        private void YPosReader_GotAddressValue(object sender, GotAddressValueEventArgs e)
        {
            float value = (float)e.Value;

            Dispatcher.Invoke(() =>
            {
                lblPlayerY.Content = value.ToString();
            });
        }
    }
}
