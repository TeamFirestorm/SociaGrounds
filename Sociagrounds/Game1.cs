using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using SociaGrounds.Model.Controllers;
using SociaGrounds.Model.KeyBoards;
using SociaGrounds.Model.Screens;
using static SociaGrounds.Model.Controllers.Static;

namespace SociaGrounds
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;
        public const int SendTime = 50;

        // All the screens
        private HomeScreen _homeScreen;
        private LobbyScreen _lobbyScreen;
        private AboutScreen _aboutScreen;
        private RoomScreen _roomScreen;
        private SettingsScreen _settingsScreen;

        /// <summary>
        /// Creates Game1 wihich inherits from Monogame Game class
        /// </summary>
        public Game1()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            TouchPanel.EnabledGestures = GestureType.Tap;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ThisDevice = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;
            CurrentScreenState = ScreenState.HomeScreen;
            IsMouseVisible = true;
            ScreenSize = GraphicsDevice.Viewport;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            PlayerTexture = Content.Load<Texture2D>("SociaGrounds/Personas/Gyllion_Character");
            Font = Content.Load<SpriteFont>("SociaGrounds/SociaGroundsFont");

            // Songs load
            SongPlayer.AddSong(Content.Load<Song>("SociaGrounds/Music/splashscreen_music"));
            SongPlayer.AddSong(Content.Load<Song>("SociaGrounds/Music/in-game-music"));

            DefaultBackground.Background = Content.Load<Texture2D>("SociaGrounds/Background/background");
            DefaultBackground.Title = Content.Load<Texture2D>("SociaGrounds/Background/Sociagrounds_title");

            // Screens load
            _homeScreen = new HomeScreen(Content);
            _aboutScreen = new AboutScreen();
            _lobbyScreen = new LobbyScreen();
            _roomScreen = new RoomScreen(Content);
            _settingsScreen = new SettingsScreen();


            if (ThisDevice != "Windows.Desktop")
            {
                
            }
            else
            {
                Static.Keyboard = new RealKeyBoard();
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (ThisDevice != "Windows.Desktop")
            {
                STouch.NewTouchLocations = TouchPanel.GetState();
            }
            else
            {
                SMouse.NewMouseState = Mouse.GetState();
            }
            

            // Switch statement to determine the screen update logic
            switch (CurrentScreenState)
            {
                case ScreenState.HomeScreen:
                    _homeScreen.Update();
                    break;

                case ScreenState.LobbyScreen:

                    if (!_lobbyScreen.FirstStarted)
                    {
                        _lobbyScreen.FirstStarted = true;
                        _lobbyScreen.CreateConnections();
                    }
                    _lobbyScreen.Update(Content);
                    break;

                case ScreenState.AboutScreen:
                    _aboutScreen.Update();
                    break;

                case ScreenState.SettingsScreen:
                    _settingsScreen.Update();
                    break;

                case ScreenState.RoomScreen:
                    _roomScreen.Update(gameTime, GraphicsDevice);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (ThisDevice == "Windows.Desktop")
            {
                SMouse.OldMouseState = SMouse.NewMouseState;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Switch statement to determine the draw logic
            switch (CurrentScreenState)
            {
                case ScreenState.HomeScreen:
                    _spriteBatch.Begin();
                    _homeScreen.Draw(_spriteBatch);
                    _spriteBatch.End();
                    break;
                case ScreenState.LobbyScreen:
                    _lobbyScreen.Draw(_spriteBatch);
                    break;
                case ScreenState.AboutScreen:
                    _aboutScreen.Draw(_spriteBatch);
                    break;
                case ScreenState.SettingsScreen:
                    _settingsScreen.Draw(_spriteBatch);
                    break;
                case ScreenState.RoomScreen:
                    _roomScreen.Draw(_spriteBatch);
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
