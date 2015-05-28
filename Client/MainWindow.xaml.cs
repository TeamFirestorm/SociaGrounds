using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibgrenWrapper;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
