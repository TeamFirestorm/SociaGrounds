using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using SocialGroundsStore.PlayerFolder;
using SocialGroundsStore.Screens;
using Microsoft.Xna.Framework.Media;

namespace SocialGroundsStore
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;
        public static Texture2D texture;
        public static SpriteFont font;
        public const int SendTime = 50;

        public enum ScreenState
        {
            LoginScreen,
            LobbyScreen,
            HomeScreen,
            RoomScreen
        }

        //The current Activated Screen;
        public static ScreenState currentScreenState;

        //The list containing all the players
        public static List<CPlayer> players;

        // All the screens
        private LoginScreen _loginScreen;
        private HomeScreen _homeScreen;
        private LobbyScreen _lobbyScreen;
        private RoomScreen _roomScreen;

        // All music
        private static List<Song> _songList;

        /// <summary>
        /// 
        /// </summary>
        public Game1()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new GraphicsDeviceManager(game: this);

            Content.RootDirectory = "Content";
            TouchPanel.EnabledGestures = GestureType.Tap;
        }

        public static CPlayer CompareById(int id)
        {
            foreach (CPlayer player in players)
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
            players = new List<CPlayer>();
            
            // Initializing songlist
            _songList = new List<Song>();

            currentScreenState = ScreenState.LoginScreen;

            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            texture = Content.Load<Texture2D>("Personas/Gyllion_Character");
            font = Content.Load<SpriteFont>("SociaGroundsFont");

            // Songs load
            _songList.Add(Content.Load<Song>("Music/splashscreen_music"));
            _songList.Add(Content.Load<Song>("Music/in-game-music"));

            // Screens load
            _loginScreen = new LoginScreen(Content);
            _roomScreen = new RoomScreen(Content);
            _homeScreen = new HomeScreen(Content);
            _lobbyScreen = new LobbyScreen();
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

            // Switch statement to determine the screen update logic
            switch (currentScreenState)
            {
                case ScreenState.LoginScreen:
                    _loginScreen.Update(Content, mouseState);

                    if (_loginScreen.ToHomeScreen(gameTime))
                    {
                        _lobbyScreen.CreateConnections();
                        currentScreenState = ScreenState.LobbyScreen;
                    }

                    if (!_loginScreen.IsPlayingMusic)
                    {
                        MediaPlayer.Play(_songList[0]);
                        _loginScreen.IsPlayingMusic = true;
                    }

                    break;
                case ScreenState.LobbyScreen:
                    _lobbyScreen.Update(Content);
                    break;

                case ScreenState.HomeScreen:
                    _homeScreen.Update(Content);
                    break;

                case ScreenState.RoomScreen:
                    _roomScreen.Update(gameTime, GraphicsDevice);

                    if (!_roomScreen.IsPlayingMusic)
                    {
                        MediaPlayer.Play(_songList[1]);
                        MediaPlayer.IsRepeating = true;
                        _roomScreen.IsPlayingMusic = true;
                    }
                    break;
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
            switch (currentScreenState)
            {
                case ScreenState.LoginScreen:
                    _spriteBatch.Begin();
                    _loginScreen.Draw(_spriteBatch);
                    _spriteBatch.End();
                    break;
                case ScreenState.LobbyScreen:
                    _lobbyScreen.Draw(_spriteBatch);
                    break;
                case ScreenState.HomeScreen:
                    _homeScreen.Draw(_spriteBatch);
                    break;
                case ScreenState.RoomScreen:
                    _roomScreen.Draw(_spriteBatch);
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
