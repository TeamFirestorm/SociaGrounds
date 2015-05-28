using System.Windows;

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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LibgrenWrapper.Client.Send(txtMessage.Text);
            txtMessage.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LibgrenWrapper.Client.Connect("10.110.110.191", 14242);            
        }
    }
}
