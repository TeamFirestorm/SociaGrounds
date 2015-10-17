using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using SociaGrounds.Model;
using SociaGrounds.Model.Controllers;
using SociaGrounds.Model.GUI;
using SociaGrounds.Model.GUI.Input;
using SociaGrounds.Model.Players;
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

        // All the screens
        private HomeScreen _homeScreen;
        private LobbyScreen _lobbyScreen;
        private AboutScreen _aboutScreen;
        private RoomScreen _roomScreen;
        private SettingsScreen _settingsScreen;
        private RoomGui _roomGui;

        public static ContentManager StaticContent { get; private set; }

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
            StaticContent = Content;

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
            //Loads the player texture
            StaticPlayer.PlayerTexture = Content.Load<Texture2D>("SociaGrounds/Personas/Gyllion_Character");

            //Loads the used fonts
            Fonts.CreateFonts(Content);

            // Loads the songs
            SongPlayer.AddSong(Content.Load<Song>("SociaGrounds/Music/splashscreen_music"));
            SongPlayer.AddSong(Content.Load<Song>("SociaGrounds/Music/in-game-music"));

            // Screens load
            _homeScreen = new HomeScreen();
            _aboutScreen = new AboutScreen();
            _lobbyScreen = new LobbyScreen();
            _roomScreen = new RoomScreen();
            _settingsScreen = new SettingsScreen();
            _roomGui = new RoomGui();

            // Sets the menu background
            DefaultBackground.Background = Content.Load<Texture2D>("SociaGrounds/Background/background");
            DefaultBackground.Title = Content.Load<Texture2D>("SociaGrounds/Background/Sociagrounds_title");
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
            ControlStart();

            // Switch statement to determine the screen update logic
            switch (CurrentScreenState)
            {
                case ScreenState.HomeScreen:
                    _homeScreen.Update(gameTime);
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
                    _aboutScreen.Update(gameTime);
                    break;

                case ScreenState.SettingsScreen:
                    _settingsScreen.Update(gameTime);
                    break;

                case ScreenState.RoomScreen:
                    _roomScreen.Update(gameTime, GraphicsDevice);
                    _roomGui.Update(gameTime);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            ControlEnd();

            base.Update(gameTime);
        }

        private void ControlStart()
        {
            if (ThisDevice != "Windows.Desktop")
            {
                InputLocation.NewTouchLocations = TouchPanel.GetState();
            }
            else
            {
                InputLocation.NewMouseState = Mouse.GetState();
            }
        }

        private void ControlEnd()
        {
            if (ThisDevice == "Windows.Desktop")
            {
                InputLocation.OldMouseState = InputLocation.NewMouseState;
            }
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
                    _homeScreen.Draw(_spriteBatch);
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

                    _spriteBatch.Begin();
                    _roomGui.Draw(_spriteBatch);
                    _spriteBatch.End();
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
