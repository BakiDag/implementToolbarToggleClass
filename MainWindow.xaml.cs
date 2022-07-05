using SimpleWifi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Contexts;
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
using Brushes = System.Drawing.Brushes;

namespace implementToolbarToggleClass
{

    public partial class MainWindow : Window
    {        
        private Wifi wifi = new Wifi();
        private Process proc = new Process();
        private Process proc2 = new Process();
        NotifyIcon nIcon = new NotifyIcon();
        
        

        public MainWindow()
        {
            InitializeComponent();            
            ImageConnected.Visibility = Visibility.Hidden;
            ImageDisconnected.Visibility = Visibility.Hidden;
            ShowInTaskbar = true;
            nIcon.Icon = new Icon("toggleon.ico");
            nIcon.Visible = true;
            nIcon.Text = "Wifi On Off";
            nIcon.ShowBalloonTip(500, "", "Wlan An/Aus gestartet", ToolTipIcon.None);
            ShowInTaskbar = true;
            

            #region if connected toggle state at start change
            if (wifi.ConnectionStatus == WifiStatus.Connected)
            {
                Togg.IsChecked = true;
                
            }
            else
            {
                Togg.IsChecked = false;
               
            }
            #endregion

            NetworkChange.NetworkAvailabilityChanged += (obj, eventArgs) =>
            {
                if (eventArgs.IsAvailable)
                {
                    Dispatcher.BeginInvoke(new Action(() => { Togg.IsChecked = true; }));                    
                    return;
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() => { Togg.IsChecked = false; }));                    
                }
            };
            
        nIcon.Click +=delegate (object sender, EventArgs args)
        {
            if (WindowState == WindowState.Minimized)
            {
                //System.Windows.MessageBox.Show(WindowState.ToString()+" min festgestellt am anfang");
                this.Show();
                this.WindowState = WindowState.Normal;                
                //Topmost = true;
                this.Show();
                return;
                
            }
            else if (WindowState == WindowState.Normal)
            {
                //System.Windows.MessageBox.Show(WindowState.ToString()+" normal festgestellt");

                this.WindowState = WindowState.Minimized;

                this.Hide();
                //Topmost = false;
                return;
                
            }
            if (WindowState == WindowState.Maximized)
            {
               
                this.WindowState = WindowState.Minimized;
                this.Hide();
                //Topmost = false;
                return;
            }
        };

        }
        
    

        private void Togg_Click_1(object sender, RoutedEventArgs e)
        {   
                if (Togg.IsChecked == false)
            {
                wifi.Disconnect();                
                proc2.StartInfo.FileName = "CMD.exe";
                proc2.StartInfo.Arguments = "/c netsh WLAN disconnect";
                proc2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc2.Start();
                proc2.WaitForExit();
                nIcon.Visible = false;
                nIcon.Visible = true;
                nIcon.ShowBalloonTip(1000, "Wlan", "Verbindung getrennt", ToolTipIcon.Warning);
                StatusbarLabel.FontWeight=FontWeights.Bold;
                StatusbarLabel.Foreground = new System.Windows.Media.SolidColorBrush(Colors.Red);
                StatusbarLabel.Content = "Verbindung getrennt";
                ImageConnected.Visibility = Visibility.Hidden;
                ImageDisconnected.Visibility = Visibility.Visible;               
                
            }
            else if (Togg.IsChecked == true)            
            {
                proc.StartInfo.FileName = "CMD.exe";
                proc.StartInfo.Arguments = "/c netsh WLAN connect name = UPC7379676";
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();
                nIcon.Visible = false;
                nIcon.Visible = true;
                nIcon.ShowBalloonTip(1000, "Wlan", "Verbunden", ToolTipIcon.Info);
                StatusbarLabel.FontWeight = FontWeights.Normal;
                StatusbarLabel.Foreground = new System.Windows.Media.SolidColorBrush(Colors.Green);
                StatusbarLabel.Content = "Verbunden";
                ImageConnected.Visibility = Visibility.Visible;
                ImageDisconnected.Visibility = Visibility.Hidden;
                Togg.Background = new System.Windows.Media.SolidColorBrush(Colors.Red);
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            nIcon.Visible = false;
            nIcon.Icon = null;
            nIcon.Dispose();
        }

        

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter & Togg.IsChecked == false)
            {
                proc.StartInfo.FileName = "CMD.exe";
                proc.StartInfo.Arguments = "/c netsh WLAN connect name = UPC7379676";
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();
                Togg.IsChecked = true;
                //System.Windows.MessageBox.Show("Key pressed");
            }
            else if (e.Key == Key.Enter & Togg.IsChecked == true)
            {
                wifi.Disconnect();
                Togg.IsChecked = false;
            }
  //################################################################################
            if (e.Key == Key.Space & Togg.IsChecked == false)
            {
                proc.StartInfo.FileName = "CMD.exe";
                proc.StartInfo.Arguments = "/c netsh WLAN connect name = UPC7379676";
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();
                Togg.IsChecked = true;
                //System.Windows.MessageBox.Show("Key pressed");
            }
            else if(e.Key == Key.Space & Togg.IsChecked == true)
            {
                wifi.Disconnect();
                Togg.IsChecked = false;
            }

        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                this.Hide();
                
                //this.WindowState = WindowState.Normal;
                //Topmost = true;
                this.Hide();
                return;

            }
            else if (WindowState == WindowState.Normal)
            {

                //this.WindowState = WindowState.Minimized;
                this.Show();
                //Topmost = false;
                return;
            }
            if (WindowState == WindowState.Maximized)
            {

                //this.WindowState = WindowState.Minimized;
                //this.Hide();
                //Topmost = false;
            }
        }
       
    }
}
