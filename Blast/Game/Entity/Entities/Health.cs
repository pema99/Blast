using System;
using Microsoft.Xna.Framework;

namespace LowRezJam
{
	public class Health : AnimatedEntity
	{
		public int Value { get { return this.CurrentFrame; } set { this.CurrentFrame = value; } }
		private GameScreen Parent { get; set; }

		public Health(Point Position, GameScreen Parent)
			: base(new Rectangle(Position.X, Position.Y, 23, 5), AssetManager.Sprites["Hearts"], Color.White, new int[] { 5 }, 0)
		{
			this.Value = 4;
			this.Parent = Parent;
		}

		public override void Update(GameTime gameTime)
		{
			if (Value <= 0)
				Parent.Die();
			base.Update(gameTime);
		}
	}
}

