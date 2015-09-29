using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using SociaGrounds.Model.Controllers;
using SociaGrounds.Model.Players;
using SociaGrounds.Model.Screens;

namespace SociaGrounds
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;
        public static Texture2D Texture;
        public static SpriteFont Font;
        public const int SendTime = 50;

        public enum ScreenState
        {
            HomeScreen,
            LobbyScreen,
            RoomScreen,
            AboutScreen,
            SettingsScreen,
        }

        // All the screens
        private HomeScreen _homeScreen;
        private LobbyScreen _lobbyScreen;
        private AboutScreen _aboutScreen;
        private RoomScreen _roomScreen;
        private SettingsScreen _settingsScreen;

        //The size of the current screen
        public static Viewport Viewport { get; private set; }

        //The current Activated Screen;
        public static ScreenState CurrentScreenState;

        //The list containing all the players
        public static List<CPlayer> Players;

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
        /// CompareById the parameter id to the id of the player
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CPlayer CompareById(int id)
        {
            foreach (CPlayer player in Players)
            {
                if (player.Id == id)
                {
                    return player;
                }
            }
            return null;
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

            // Initializing playlist
            Players = new List<CPlayer>();

            CurrentScreenState = ScreenState.HomeScreen;

            IsMouseVisible = true;

            Viewport = GraphicsDevice.Viewport;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Texture = Content.Load<Texture2D>("SociaGrounds/Personas/Gyllion_Character");
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
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyState = Keyboard.GetState();

            // Switch statement to determine the screen update logic
            switch (CurrentScreenState)
            {
                case ScreenState.HomeScreen:
                    _homeScreen.Update(mouseState);
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
                    _aboutScreen.Update(keyState);
                    break;

                case ScreenState.SettingsScreen:
                    _settingsScreen.Update(keyState);
                    break;

                case ScreenState.RoomScreen:
                    _roomScreen.Update(gameTime, GraphicsDevice, keyState);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
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
