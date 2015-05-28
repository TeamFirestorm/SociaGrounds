using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using LibgrenWrapper;
using System.Net.NetworkInformation;

namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LibgrenWrapper.Server.Setup();

            DataBase.InsertConnectionInfo(InternetConenction.MyIp.ToString(), InternetConenction.MyDnsSuffix);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LibgrenWrapper.Server.StartServer();
            Console.WriteLine("Started");
        }
    }
}
