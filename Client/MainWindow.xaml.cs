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

            LibgrenWrapper.Client.Setup();

            InternetConnection.GetMyIpAndDns();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LibgrenWrapper.Client.Send(txtMessage.Text);
            txtMessage.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IPAddress adres = InternetConnection.CheckPossibleConnection();

            if (adres != null)
            {
                LibgrenWrapper.Client.Connect(InternetConnection.MyIp.ToString(), 14242);
            }
            else
            {
                MessageBox.Show("Connection Error");
            }
        }
    }
}
