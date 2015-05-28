using System;
using System.Net;
using System.Net.Sockets;
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

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                IPAddress adres = IPAddress.Parse(LocalIPAddress());

                if (adres.AddressFamily == AddressFamily.InterNetwork && properties.DnsSuffix != "")
                {
                    Console.WriteLine("DNS Suffix: " + properties.DnsSuffix);
                }
        
            }
            Console.WriteLine();

            LibgrenWrapper.Server.Setup();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LibgrenWrapper.Server.StartServer();
            Console.WriteLine("Started");
        }

        public string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
    }
}
