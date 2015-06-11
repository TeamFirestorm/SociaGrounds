using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SociaGroundsEngine.PlayerFolder;
using SociaGroundsEngine.Screens;

namespace SociaGroundsEngine
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Texture2D texture;

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
        private LoginScreen loginScreen;
        private HomeScreen homeScreen;
        private LobbyScreen lobbyScreen;
        private RoomScreen roomScreen;
        
        /// <summary>
        /// 
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = Content.Load<Texture2D>("Personas/Gyllion_Character");

            players = new List<CPlayer>
            {
                new MyPlayer(new Vector2(0, 0), Content.Load<Texture2D>("Personas/Chris_Character"))
            };

            // Screens initialize
            loginScreen = new LoginScreen(Content);
            roomScreen = new RoomScreen(Content,GraphicsDevice);
            homeScreen = new HomeScreen(Content);

            currentScreenState = ScreenState.LoginScreen;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Switch statement to determine the screen update logic
            switch (currentScreenState)
            {
                case ScreenState.LoginScreen:
                    loginScreen.Update();

                    if (loginScreen.ToHomeScreen(gameTime))
                    {
                        //Alleen voor het testen
                        currentScreenState = ScreenState.RoomScreen;

                        //currentScreenState = ScreenState.LobbyScreen;
                        //lobbyScreen = new LobbyScreen();
                    }
                    break;
                case ScreenState.LobbyScreen:
                        lobbyScreen.Update();
                    break;

                case ScreenState.HomeScreen:
                    homeScreen.Update();
                    break;

                case ScreenState.RoomScreen:
                    roomScreen.Update(gameTime,GraphicsDevice);
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
                    spriteBatch.Begin();
                    loginScreen.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
                case ScreenState.LobbyScreen:
                    lobbyScreen.Draw(spriteBatch);
                    break;
                case ScreenState.HomeScreen:
                    homeScreen.Draw(spriteBatch);
                    break;
                case ScreenState.RoomScreen:
                    roomScreen.Draw(spriteBatch);
                    break;
            }
            base.Draw(gameTime);
        }
    }
}
