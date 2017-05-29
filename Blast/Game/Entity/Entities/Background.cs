using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LowRezJam
{
	public class Background
	{
		private Entity BG { get; set; }
		private const int Size = 320;
		public float ParallaxFactor { get; set; }
		private Camera Camera { get; set; }

		public Background(Camera Camera, float ParallaxFactor = 0.2f)
		{
			BG = new Entity(new Rectangle(0, 0, Size, Size), AssetManager.Sprites["Background"], Color.White);
			this.Camera = Camera;
			this.ParallaxFactor = ParallaxFactor;
		}

		public void Update(GameTime gameTime)
		{
			BG.X = Camera.CamPos.X * ParallaxFactor;
			BG.Y = Camera.CamPos.Y * ParallaxFactor;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			BG.Draw(spriteBatch);
			spriteBatch.Draw(AssetManager.Sprites["Outline"], new Rectangle(64, 64, 192, 192), Color.White);
		}
	}
}

