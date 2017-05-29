using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LowRezJam
{
	public class Numbers
	{
		public Point Position { get; set; }
		public int Value { get; set; }
		private AnimatedEntity[] NumberEntity { get; set; }

		public Numbers(Point Position, int Value = 0)
		{
			this.Position = Position;
			this.Value = Value;
			this.NumberEntity = new AnimatedEntity[] 
			{
				new AnimatedEntity(new Rectangle(Position.X, Position.Y, 3, 5), AssetManager.Sprites["Numbers"], Color.White, new int[] { 10 }, 0),
				new AnimatedEntity(new Rectangle(Position.X + 1 + 3, Position.Y, 3, 5), AssetManager.Sprites["Numbers"], Color.White, new int[] { 10 }, 0),
				new AnimatedEntity(new Rectangle(Position.X + 2 + 6, Position.Y, 3, 5), AssetManager.Sprites["Numbers"], Color.White, new int[] { 10 }, 0),
			};
		}

		public void Update(GameTime gameTime)
		{
			if (Value > 999)
				Value = 999;

			string ValueString = Value.ToString();
			while (ValueString .Length < 3)
			{
				ValueString = "0" + ValueString;
			}
				
			NumberEntity[0].CurrentFrame = int.Parse(ValueString.Substring(0, 1));
			NumberEntity[1].CurrentFrame = int.Parse(ValueString.Substring(1, 1));
			NumberEntity[2].CurrentFrame = int.Parse(ValueString.Substring(2, 1));
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < 3; i++)
			{
				NumberEntity[i].Draw(spriteBatch);
			}
		}
	}
}

