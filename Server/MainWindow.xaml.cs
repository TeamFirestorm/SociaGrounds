using System;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using LibgrenWrapper;

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

            

            //IPAddress[] localIps = Dns.GetHostAddresses(Dns.GetHostName());

            //Console.WriteLine();
            //foreach (var a in localIps)
            //{
            //    if (a.AddressFamily == AddressFamily.InterNetwork)
            //    {
            //        Console.WriteLine(a);
            //    }
            //}

            //Console.WriteLine(Dns.GetHostAddresses());

            LibgrenWrapper.Server.Setup();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LibgrenWrapper.Server.StartServer();
            Console.WriteLine("Started");
        }
    }
}
