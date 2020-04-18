using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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
        private static extern IntPtr GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr processId);

        private CultureInfo _currentLanaguge;

        private LanguageJson lang = new LanguageJson
        {
            Email = "james@example.com",
            Active = true,
            CreatedDate = new DateTime(2013, 1, 20, 0, 0, 0, DateTimeKind.Utc),
            Roles = new List<string>
                                {
                                    "User",
                                    "Admin"
                                }
        };


        public MainWindow()
        {
            InitializeComponent();
            string json_data = JsonConvert.SerializeObject(lang, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            MessageBox.Show(json_data);
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    HandleCurrentLanguage();
                    ConvertToJson();
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
                MessageBox.Show("");
            }
        }

        private void ConvertToJson()
        {
            //        {
            //  "game": "ADVENTURE",
            //  "event": "HEALTH",
            //  "min_value": 0,
            //  "max_value": 100,
            //  "icon_id": 1,
            //  "value_optional": false
            //}
         
        }

        // {
        //   "Email": "james@example.com",
        //   "Active": true,
        //   "CreatedDate": "2013-01-20T00:00:00Z",
        //   "Roles": [
        //     "User",
        //     "Admin"
        //   ]
        // }
    }
}