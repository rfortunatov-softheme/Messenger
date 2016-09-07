using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using CefSharp;
using Messenger.Implementation;

namespace Messenger.Views
{
    /// <summary>
    /// Interaction logic for MessengerHost.xaml
    /// </summary>
    public partial class MessengerHost : Window
    {
        public MessengerHost()
        {
            var settings = new CefSettings();
            settings.CefCommandLineArgs.Add("disable-gpu", "1");
            settings.CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MessengerApp");
            settings.PersistSessionCookies = true;
            if (!Directory.Exists(settings.CachePath))
            {
                Directory.CreateDirectory(settings.CachePath);
            }

            Cef.Initialize(settings);
            InitializeComponent();
            Browser.Address = "https://messenger.com";
            Browser.RequestHandler = new RequestHandler(ShowToast);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        //Attach this to the MouseDown event of your drag control to move the window in place of the title bar
        private void WindowDrag(object sender, MouseButtonEventArgs e) // MouseDown
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle, 0xA1, (IntPtr)0x2, (IntPtr)0);
        }

        //Attach this to the PreviewMousLeftButtonDown event of the grip control in the lower right corner of the form to resize the window
        private void WindowResize(object sender, MouseButtonEventArgs e) //PreviewMousLeftButtonDown
        {
            HwndSource hwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            SendMessage(hwndSource.Handle, 0x112, (IntPtr)61448, IntPtr.Zero);
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
            Application.Current.Shutdown(0);
        }

        private void Minimize_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Maximize_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void ShowToast()
        {
            Task.Factory.StartNew(() =>
            {
                Views.Notification notification = null;
                Dispatcher.Invoke(() => (notification = new Views.Notification()).Show());
                Thread.Sleep(TimeSpan.FromSeconds(5));
                Dispatcher.Invoke(() => notification.CloseWindow(null, null));
            });
        }
    }
}
