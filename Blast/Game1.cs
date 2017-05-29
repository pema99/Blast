using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace LowRezJam
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

		SoundEffect BackgroundMusic { get; set; }

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 256;
            graphics.PreferredBackBufferHeight = 256;
            graphics.ApplyChanges();
            
            this.IsMouseVisible = true;

            ScreenManager SM = new ScreenManager(this);
			SM.AddScreen(new MenuScreen(SM));

            Components.Add(SM);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }

		protected override void LoadContent()
		{
			BackgroundMusic = Content.Load<SoundEffect>("song");
			var Temp = BackgroundMusic.CreateInstance();
			Temp.IsLooped = true;
			Temp.Volume = 0.7f;
			Temp.Play();
		}
    }
}
