using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SociaGrounds.Model.Controllers;
using SociaGrounds.Model.GUI;
using static SociaGrounds.Model.Controllers.SMouse;

namespace SociaGrounds.Model.Screens
{
    public class HomeScreen
    {
        private readonly List<Button> _buttons;
        public bool IsPlayingMusic { get; set; }

        public HomeScreen(ContentManager content)
        {
            float middleWidth = Static.ScreenSize.Width / 2f;
            float middleHeight = Static.ScreenSize.Height / 2f;

            _buttons = new List<Button>
            {
                new Button(new Vector2(middleWidth - 200, middleHeight + 200), "Play the Game", 1.0f, "Play the Game".Length, Fonts.LargeFont),
            };

            _buttons.Add(new Button(new Vector2(0 + 200, middleHeight), "About", 1.0f, _buttons[0].Parts -2, Fonts.LargeFont));
            _buttons.Add(new Button(new Vector2(Static.ScreenSize.Width - 600, middleHeight), "Settings", 1.0f, _buttons[0].Parts -2, Fonts.LargeFont));
        }

        public void Update()
        {
            if (!IsPlayingMusic)
            {
                SongPlayer.PlaySong(0);
                IsPlayingMusic = true;
            }

            foreach (Button button in _buttons)
            {
                if (button.CheckButtonSelected())
                {
                    ChangeScreen(button);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DefaultBackground.Draw(spriteBatch);

            foreach (Button button in _buttons)
            {
                button.Draw(spriteBatch);
            }
        }

        public void ChangeScreen(Button clicked)
        {
            if (_buttons[0].Equals(clicked))
            {
                Static.CurrentScreenState = ScreenState.LobbyScreen;
            }
            else if (_buttons[1].Equals(clicked))
            {
                Static.CurrentScreenState = ScreenState.AboutScreen;
            }
            else if (_buttons[2].Equals(clicked))
            {
                Static.CurrentScreenState = ScreenState.SettingsScreen;
            }
        }
    }
}
