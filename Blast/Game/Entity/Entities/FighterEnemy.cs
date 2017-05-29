using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LowRezJam
{
	public class FighterEnemy : AnimatedEntity, IEnemy
	{
		private double InitialX { get; set; }
		private double SinNum { get; set; }
		public int Health { get; set; }
		public EnemyManager Parent { get; set; }

		public FighterEnemy(Rectangle Bounds, EnemyManager Parent)
			: base(Bounds, AssetManager.Sprites["FighterEnemy"], Color.White, new int[] { 7 }, 0.2f)
		{
			this.InitialX = Bounds.X;
			this.SinNum = 0;
			this.Play(0, true);
			this.Health = 6;
			this.Parent = Parent;
		}

		public override void Update(GameTime gameTime)
		{
			Y += 0.4f;
			X = (float)(InitialX + Math.Sin(SinNum) * 10);
			SinNum += 0.1;

			if (Y > 320)
				Y = 0;

			base.Update(gameTime);
		}
	}
}

