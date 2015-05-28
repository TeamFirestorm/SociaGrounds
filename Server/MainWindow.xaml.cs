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
            LibgrenWrapper.Server.Setup();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LibgrenWrapper.Server.StartServer();
        }
    }
}
