using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;

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
            UserAgent.Text = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
            Dest.Text = @"C:\download.mp4";
            SpeedUp.IsChecked = true;
            DownloadBtn.IsEnabled = true;
            Status.Text = "Entper the clip info.\nPress \"download\" to start.\n";
        }

        public void Download(object sender, RoutedEventArgs e)
        {
            DownloadBtn.IsEnabled = false;
            Status.Text = String.Empty;
            new Thread(FFmpegBackgroundTask).Start();
        }

        private void FFmpegBackgroundTask()
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg.exe",
                    Arguments = "-h",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };
            proc.Start();

            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                this.Dispatcher.Invoke(() =>
                {
                    Status.Text += (line + "\n");
                    Status.ScrollToEnd();
                });
            }

            this.Dispatcher.Invoke(() =>
            {
                DownloadBtn.IsEnabled = true;
            });
        }
    }
}
