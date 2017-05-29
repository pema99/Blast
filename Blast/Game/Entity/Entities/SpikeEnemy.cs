using System;
using Microsoft.Xna.Framework;

namespace LowRezJam
{
	public class SpikeEnemy : AnimatedEntity, IEnemy
	{
		public int Health { get; set; }
		public float FakeY;
		public EnemyManager Parent { get; set; }
		public double SinNum;

		public SpikeEnemy(Rectangle Bounds, EnemyManager Parent)
			: base(Bounds, AssetManager.Sprites["SpikeEnemy"], Color.White, new int[] { 2 }, 0.2)
		{
			this.Health = 3;
			this.Parent = Parent;
			this.FakeY = Bounds.Y;
			this.SinNum = 0;
			this.Play(0);
		}

		public override void Update(GameTime gameTime)
		{
			SinNum+=0.1;
			
			FakeY += 0.2f;
			Y = FakeY + (float)Math.Sin(SinNum)*5;

			if (Y > 320)
				Y = 0;

			base.Update(gameTime);
		}
	}
}

