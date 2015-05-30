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
            ServerCon.Setup();

            DataBase.InsertConnectionInfo(InternetConnection.MyIp.ToString(), InternetConnection.MyDnsSuffix);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ServerCon.StartServer();
            Console.WriteLine("Started");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ServerCon.Shutdown();
            DataBase.DeleteConnection(InternetConnection.MyIp.ToString(), InternetConnection.MyDnsSuffix);
        }
    }
}
