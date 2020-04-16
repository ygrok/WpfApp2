using System;
using System.Collections.Generic;
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
using System.Runtime.InteropServices;
using System.Globalization;
using System.Threading;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    //public partial class MainWindow : Window
    //{
    //    string language = "temp";
    //    public MainWindow()
    //    {
    //        InitializeComponent();

    //        InputLanguageManager.Current.InputLanguageChanged += new InputLanguageEventHandler(Current_InputLanguageChanged);

    //        System.Windows.Input.InputLanguageManager.Current.InputLanguageChanged += new InputLanguageEventHandler((sender, e) =>
    //        {
    //            language = e.NewLanguage.DisplayName;
    //        });
    //        Console.WriteLine(language);
    //    }

    //    private void Current_InputLanguageChanged(object sender, EventArgs e)
    //    {
    //        //Cleanup so that the icon will be removed when the application is closed
    //        Console.WriteLine("language changed");
    //    }

    //}
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetKeyboardLayout(uint idThread);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr processId);

        private CultureInfo _currentLanaguge;

        public MainWindow()
        {
            InitializeComponent();

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    HandleCurrentLanguage();
                    Thread.Sleep(500);
                }
            });
        }

        private static CultureInfo GetCurrentCulture()
        {
            var l = GetKeyboardLayout(GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero));
            return new CultureInfo((short)l.ToInt64());
        }

        private void HandleCurrentLanguage()
        {
            var currentCulture = GetCurrentCulture();
            if (_currentLanaguge == null || _currentLanaguge.LCID != currentCulture.LCID)
            {
                _currentLanaguge = currentCulture;
                MessageBox.Show(_currentLanaguge.Name);
            }
        }
    }
}
