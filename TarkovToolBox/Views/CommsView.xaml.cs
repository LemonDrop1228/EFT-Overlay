using CefSharp.Wpf;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TarkovToolBox.Views
{
    /// <summary>
    /// Interaction logic for CommsView.xaml
    /// </summary>
    public partial class CommsView : BaseView
    {
        public CommsView()
        {
            InitializeComponent();
        }

        ChromiumWebBrowser MarketBrowser { get; set; }

        private void InitMarketBrowser(string url)
        {
            MarketBrowser = new ChromiumWebBrowser(url);
            Window visual = Application.Current.Windows[Application.Current.Windows.Count - 1];
            HwndSource parentWindowHwndSource = (HwndSource)HwndSource.FromVisual(visual);
            MarketBrowser.CreateBrowser(parentWindowHwndSource, new Size(100, 100));
            MarketBrowser.Name = $"browser_Market";


            BrowserContainerBorder.Child = MarketBrowser;
        }

        private void BaseView_Loaded(object sender, RoutedEventArgs e)
        {
            if (MarketBrowser == null)
                InitMarketBrowser("https://zibbp.github.io/Escape-From-Tarkov-Interactive-Online-Map/");
        }
    }
}
