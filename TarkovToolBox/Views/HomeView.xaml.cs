using System;
using System.Collections.Generic;
using System.IO;
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

namespace TarkovToolBox.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : BaseView
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void BaseView_Loaded(object sender, RoutedEventArgs e)
        {
            LoadWelcomeBlurb();
        }

        private void LoadWelcomeBlurb()
        {
            var range = new TextRange(WelcomRTB.Document.ContentStart, WelcomRTB.Document.ContentEnd);
            using (MemoryStream memLicStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(Properties.Resources.welcome_blurb)))
            {
                range.Load(memLicStream, DataFormats.Rtf);
            }
        }
    }
}
