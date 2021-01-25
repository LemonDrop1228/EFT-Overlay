using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TarkovToolBox.Extensions;

namespace TarkovToolBox.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class MapView : BaseView
    {
        ObservableCollection<Button> MapButtonCollection { get; set; }

        public MapView()
        {
            InitializeComponent();
            BuildMapButtons();
            InitMap();
        }

        private void InitMap()
        {
            var imageBrush = Resources["0"];
            ImageSource imageSource = ((ImageBrush)imageBrush).ImageSource;
            MapImageControl.Source = imageSource;
        }


        private void BuildMapButtons()
        {
            MapButtonCollection = new ObservableCollection<Button>() {
                GetMapButton("CUSTOMS", 0),
                GetMapButton("FACTORY", 1),
                GetMapButton("INTERCHANGE", 2),
                GetMapButton("WOODS", 3),
                GetMapButton("RESERVE", 4),
                GetMapButton("SHORELINE", 5),
                GetMapButton("THE LAB", 6)
            };
            MapButtonStackPanel.Children.AddCollection(MapButtonCollection);
        }

        private Button GetMapButton(string text, int id)
        {
            var button = new Button()
            {
                Content = text,
                Margin = new Thickness(5, 0, 5, 0),
                Tag = id,
                FontSize = 26,
                MinHeight = 75,
                FontFamily = new FontFamily("Consolas")
            };
            button.Click += MapSelectionChanged;
            return button;
        }


        private void MapSelectionChanged(object sender, RoutedEventArgs e)
        {
            MapImageControl.Source = ((ImageBrush)Resources[(e.Source as Button).Tag.ToString()]).ImageSource;
        }

    }
}
