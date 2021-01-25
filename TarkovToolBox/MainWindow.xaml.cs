using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TarkovToolBox.Utils;
using ContextMenu = System.Windows.Forms.ContextMenu;
using MenuItem = System.Windows.Forms.MenuItem;
using System.Drawing;
using TarkovToolBox.Extensions;
using Newtonsoft.Json;
using CefSharp;

namespace TarkovToolBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Drawing.Color trayContextBG = System.Drawing.Color.FromArgb(40, 51, 69);
        private System.Drawing.Color BackgroundBase = (System.Drawing.Color)System.Drawing.ColorTranslator.FromHtml("#FF3C4555");

        NotifyIcon TrayIcon { get; set; }
        ContextMenuStrip TrayContextMenu { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            PostInit();
            CefSharpSettings.ShutdownOnExit = true;
            CefSharpSettings.SubprocessExitIfParentProcessClosed = true;
        }

        private void PostInit()
        {
            TrayContextMenu = new ContextMenuStrip();
            TrayContextMenu.ShowCheckMargin = TrayContextMenu.ShowImageMargin = false;
            TrayContextMenu.Items.AddRange(GetTrayMenuItems());
            TrayContextMenu.BackColor = trayContextBG;
            TrayContextMenu.ForeColor = System.Drawing.Color.WhiteSmoke;


            TrayIcon = new NotifyIcon()
            {
                Icon = Properties.Resources.Toolsimage,
                Visible = true,
                Text = "Tarkov ToolBox",
                ContextMenuStrip = TrayContextMenu
            };
        }

        private ToolStripItem[] GetTrayMenuItems()
        {
            var mList = new List<ToolStripItem>();
            ToolStripItem menuItemTemplate;

            menuItemTemplate = new ToolStripLabel();
            menuItemTemplate.Text = "App";
            menuItemTemplate.ForeColor = System.Drawing.Color.DarkGoldenrod;
            mList.Add(menuItemTemplate);
            mList.Add(new ToolStripSeparator());
            menuItemTemplate = new ToolStripButton();
            menuItemTemplate.Click += (s,e) => { this.Close(); };
            menuItemTemplate.Text = "Exit";
            mList.Add(menuItemTemplate);
            mList.Add(new ToolStripSeparator());
            menuItemTemplate = new ToolStripLabel();
            menuItemTemplate.Text = " ";
            mList.Add(menuItemTemplate);
            menuItemTemplate = new ToolStripLabel();
            menuItemTemplate.Text = "Maps";
            menuItemTemplate.ForeColor = System.Drawing.Color.DarkGoldenrod;
            mList.Add(menuItemTemplate);
            mList.Add(new ToolStripSeparator());
            menuItemTemplate = new ToolStripMenuItem();
            menuItemTemplate.Text = "Active Map";
            (menuItemTemplate as ToolStripMenuItem).DropDownItems.AddRange(new ToolStripItem[] { 
                new ToolStripButton("Customs", null, (s,e) => { SetMap(MapEnums.Customs); }),
                new ToolStripButton("Interchange", null, (s,e) => { SetMap(MapEnums.Interchange); })
            });
            (menuItemTemplate as ToolStripMenuItem).DropDown.BackColor = trayContextBG;
            (menuItemTemplate as ToolStripMenuItem).DropDown.ForeColor = System.Drawing.Color.WhiteSmoke;
            //(menuItemTemplate as ToolStripMenuItem).DropDown.
            mList.Add(menuItemTemplate);


            return mList.ToArray();
        }

        private void SetMap(MapEnums map)
        {
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Console.WriteLine("WINDOW DED");
            if (TrayIcon != null)
            {
                TrayIcon.Visible = false;
                TrayIcon.Dispose(); 
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //AltTabHelper.RemoveFromAltTab(this);
            AlphaSlider.Value = (double)25;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //Console.WriteLine(JsonConvert.SerializeObject(e));
            this.Background = new SolidColorBrush(BackgroundBase.WithAlpha((int)e.NewValue));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainTabCtrl.SelectedIndex = 1;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainTabCtrl.SelectedIndex = 2;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainTabCtrl.SelectedIndex = 3;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MainTabCtrl.SelectedIndex = 4;
        }
    }

    enum MapEnums
    {
        Customs,
        Interchange
    }
}
