using System;
using Microsoft.Xna.Framework;

namespace LowRezJam
{
	public class FollowEnemy : Entity, IEnemy
	{
		public int Health { get; set; }
		public EnemyManager Parent { get; set; }

		public FollowEnemy(Rectangle Bounds, EnemyManager Parent)
			: base(Bounds, AssetManager.Sprites["FollowEnemy"], Color.White)
		{
			this.Health = 6;
			this.Parent = Parent;
		}

		public override void Update(GameTime gameTime)
		{
			Y += 0.3f;
			if (Bounds.Center.X > Parent.Parent.Player.Bounds.Center.X)
				X-=0.3f;
			if (Bounds.Center.X < Parent.Parent.Player.Bounds.Center.X)
				X+=0.3f;

			if (Y > 320)
				Y = 0;

			base.Update(gameTime);
		}
	}
}

