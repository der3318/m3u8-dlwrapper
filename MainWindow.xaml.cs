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

namespace m3u8_dlwrapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Link.Text = "http://example.com/source.m3u8";
            Dest.Text = @"C:\download.mp4";
            SpeedUp.IsChecked = true;
            DownloadBtn.IsEnabled = true;
            Status.Text = "Entper the clip info.\nPress \"download\" to start.\n";
        }

        public void Download(object sender, RoutedEventArgs e)
        {
            DownloadBtn.IsEnabled = false;
            Status.Text += "Start downloading...\n";
        }
    }
}
