using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace square_chase
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        Random rand = new Random(); //used to generate random coords
        Texture2D squareTexture; // holds the texture for the squares
        Rectangle currentSquare; //stores a area of the display, stores location of squares
        int playerscore = 0; // player score
        float timeRemaining = 0.0f; //how may seconds a square will remain active after being generated
        const float TimePerSquare = 0.75f; //amount of time before a square runs away from a player
        Color[] colors = new Color[9] //This array of colors will be used when a circle is drawn
        { Color.Red,
          Color.Green, 
          Color.Blue,
          Color.Yellow,
          Color.Orange,
          Color.SeaShell,
          Color.Olive,
          Color.MistyRose,
          Color.Navy
        }; 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            squareTexture = Content.Load<Texture2D>(@"SQUARE"); //Assigns the variable squareTexture a image
        }

        
        protected override void UnloadContent()
        {
            
        }

        
        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (timeRemaining == 0.0f)
            {
                currentSquare = new Rectangle(
                    rand.Next(0, this.Window.ClientBounds.Width - 25),
                    rand.Next(0, this.Window.ClientBounds.Height - 25),
                    25, 25);
                timeRemaining = TimePerSquare;
            }

            MouseState mouse = Mouse.GetState();
            if ((mouse.LeftButton == ButtonState.Pressed) &&
                (currentSquare.Contains(mouse.X, mouse.Y)))
            {
                playerscore++;
                timeRemaining = 0.0f;
            }
            timeRemaining = MathHelper.Max(0, timeRemaining -
                (float)gameTime.ElapsedGameTime.TotalSeconds);

            this.Window.Title = "Score : " + playerscore.ToString();



            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            spriteBatch.Begin();
            spriteBatch.Draw(
                squareTexture,
                currentSquare,
                colors[playerscore % 9]);
            spriteBatch.End();

            

            base.Draw(gameTime);
        }
    }
}
