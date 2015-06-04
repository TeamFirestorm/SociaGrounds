using System.Net;
using System.Windows;
using LibgrenWrapper;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            ClientCon.Setup();

            InternetConnection.GetMyIpAndDns();
        }

        private void SendMessgae_Click(object sender, RoutedEventArgs e)
        {
            ClientCon.Send(TxtMessage.Text);
            TxtMessage.Text = "";
        }

        private void Connenct_Click(object sender, RoutedEventArgs e)
        {
            IPAddress adres = InternetConnection.CheckPossibleConnection();

            if (adres != null)
            {
                ClientCon.Connect(adres.ToString(), 14242);
                return;
            }
            MessageBox.Show("Connection Error");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ClientCon.Shutdown();
            e.Cancel = true;
        }

        
    }
}
