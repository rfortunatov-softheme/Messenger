using System;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using Timer = System.Threading.Timer;

namespace Messenger
{
    /// <summary>
    /// Interaction logic for Notification.xaml
    /// </summary>
    public partial class Notification : Window
    {
        private readonly object _lock = new object();

        public Notification()
        {
            InitializeComponent();
            Left = SystemParameters.PrimaryScreenWidth - Width;
            Top = 30;
        }

        public void CloseWindow(object sender, RoutedEventArgs e)
        {
            lock (_lock)
            {
                Close();
            }
        }
    }
}
