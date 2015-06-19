using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MonoGame.Framework;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SociaGroundsEngine
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : SwapChainBackgroundPanel
    {
        readonly Game1 game;
        public GamePage()
        {
            this.InitializeComponent();
        }

        public GamePage(string launchArguments)
        {
            game = XamlGame<Game1>.Create(launchArguments, Window.Current.CoreWindow, this);
        }
    }
}
