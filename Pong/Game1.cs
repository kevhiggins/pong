using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Paddle player1Paddle;
        Paddle player2Paddle;
        Ball ball;
        int player1Score;
        int player2Score;

        Vector2 player1ScorePosition;
        Vector2 player2ScorePosition;

        SpriteFont scoreFont;

        Round round;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            var player1PaddleTexture = GeneratePaddleTexture();
            var player2PaddleTexture = GeneratePaddleTexture();
            player1Paddle = new Paddle(player1PaddleTexture, GeneratePaddle1StartPosition(player1PaddleTexture), PlayerIndex.One);
            player2Paddle = new Paddle(player2PaddleTexture, GeneratePaddle2StartPosition(player2PaddleTexture), PlayerIndex.Two);
            var ballTexture = GenerateBallTexture();
            ball = new Ball(ballTexture, GetScreenCenterPosition(ballTexture));
            round = new Round();
            scoreFont = Content.Load<SpriteFont>("Arial");
            player1ScorePosition = new Vector2((float)GraphicsDevice.Viewport.Width * 1/3, 50);
            player2ScorePosition = new Vector2((float)GraphicsDevice.Viewport.Width * 2 / 3, 50);

            base.Initialize();
        }

        private Texture2D GeneratePaddleTexture()
        {
            var width = 15;
            var height = 80;
            var texture = new Texture2D(GraphicsDevice, width, height);

            // Fill the texture with red pixels
            var pixelCount = width * height;
            Color[] colorData = new Color[pixelCount];
            for (int i = 0; i < pixelCount; i++)
            {
                colorData[i] = Color.White;
            }
            texture.SetData(colorData);

            return texture;
        }

        private Texture2D GenerateBallTexture()
        {
            var width = 16;
            var height = 16;
            var texture = new Texture2D(GraphicsDevice, width, height);

            // Fill the texture with red pixels
            var pixelCount = width * height;
            Color[] colorData = new Color[pixelCount];
            for (int i = 0; i < pixelCount; i++)
            {
                colorData[i] = Color.White;
            }
            texture.SetData(colorData);

            return texture;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            UpdateInput();

            // Check if the ball is going off the top or bottom of the screen
            if (ball.Position.Y + ball.Texture.Height > GraphicsDevice.Viewport.Height)
            {
                ball.Direction.Y = 0 - ball.Direction.Y;
            }
            else if (ball.Position.Y < 0)
            {
                ball.Direction.Y = 0 - ball.Direction.Y;
            }

            // Check if a goal has been made.
            // On score, Reset positions.
            if (ball.Position.X + ball.Texture.Width > GraphicsDevice.Viewport.Width)
            {
                player1Score++;
                Reset();
            } else if(ball.Position.X < 0)
            {
                player2Score++;
                Reset();
            }

            base.Update(gameTime);
        }

        private void Reset()
        {
            ball.Position = GetScreenCenterPosition(ball.Texture);
            ball.Direction = new Vector2(1, 0);
        }

        private Vector2 GetScreenCenterPosition(Texture2D texture)
        {
            return new Vector2((GraphicsDevice.Viewport.Width / 2) - (texture.Width / 2), (GraphicsDevice.Viewport.Height / 2) - (texture.Height / 2));
        }

        private Vector2 GeneratePaddle1StartPosition(Texture2D texture)
        {
            return new Vector2(5, (GraphicsDevice.Viewport.Height / 2) - texture.Height / 2);
        }

        private Vector2 GeneratePaddle2StartPosition(Texture2D texture)
        {
            return new Vector2(GraphicsDevice.Viewport.Width - texture.Width - 5, (GraphicsDevice.Viewport.Height / 2) - texture.Height / 2);
        }




        private void UpdateInput()
        {
            player1Paddle.UpdatePosition(GraphicsDevice);
            player2Paddle.UpdatePosition(GraphicsDevice);
            ball.UpdatePosition(player1Paddle, player2Paddle);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(player1Paddle.Texture, player1Paddle.Position);
            spriteBatch.Draw(player2Paddle.Texture, player2Paddle.Position);
            spriteBatch.Draw(ball.Texture, ball.Position);
            spriteBatch.DrawString(scoreFont, player1Score.ToString(), player1ScorePosition, Color.Black);
            spriteBatch.DrawString(scoreFont, player2Score.ToString(), player2ScorePosition, Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
