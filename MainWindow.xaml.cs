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
            Link.Text = "https://sample.com/playlist.m3u8";
            UserAgent.Text = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.46 Safari/537.36 Edg/100.0.1185.17";
            HeaderCookie.Text = "Cookie: logged_in=yes; tracker=direct`r`n";
            Dest.Text = @"C:\Users\Public\download.mp4";
            SpeedUp.IsChecked = true;
            DownloadBtn.IsEnabled = true;
            Status.Text = "Enter the clip info.\nPress \"download\" to start.\n";
        }

        public void Download(object sender, RoutedEventArgs e)
        {
            DownloadBtn.IsEnabled = false;
            Status.Text = String.Empty;
            new Thread(FFmpegBackgroundTask).Start();
        }

        private void FFmpegBackgroundTask()
        {
            String copyOption = String.Empty;
            String arguments = "-h";
            this.Dispatcher.Invoke(() =>
            {
                copyOption = SpeedUp.IsChecked.HasValue && SpeedUp.IsChecked.Value ? "-filter_complex \"[0:v]setpts=PTS/2[v];[0:a]atempo=2[a]\" -map \"[v]\" -map \"[a]\"" : "-c copy";
                arguments = String.Format("-user_agent \"{0}\" -headers \"{1}\" -i {2} {3} -bsf:a aac_adtstoasc {4}", UserAgent.Text, HeaderCookie.Text, Link.Text, copyOption, Dest.Text);
            });

            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg.exe",
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            const int MaxLoggedLineCnt = 100;
            DataReceivedEventHandler redirectionHandler = new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    string line = e.Data;
                    this.Dispatcher.Invoke(() =>
                    {
                        Status.Text += (line + "\n");
                        Status.ScrollToEnd();
                        while (Status.LineCount > MaxLoggedLineCnt)
                        {
                            Status.Text = Status.Text.Substring(Status.Text.IndexOf("\n", StringComparison.OrdinalIgnoreCase) + 1);
                        }
                    });
                }
            });

            proc.OutputDataReceived += redirectionHandler;
            proc.ErrorDataReceived += redirectionHandler;
            proc.Start();
            proc.StandardInput.Write("y\ny\ny\ny\ny\ny\ny\ny\ny\ny\n");
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
            proc.WaitForExit();
            proc.Close();

            this.Dispatcher.Invoke(() =>
            {
                DownloadBtn.IsEnabled = true;
            });
        }
    }
}
