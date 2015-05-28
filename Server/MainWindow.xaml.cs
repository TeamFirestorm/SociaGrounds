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

            Console.WriteLine("\n" + LibgrenWrapper.Server.MyIp);
            Console.WriteLine(LibgrenWrapper.Server.MyDnsSuffix + "\n");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LibgrenWrapper.Server.StartServer();
            Console.WriteLine("Started");
        }
    }
}
