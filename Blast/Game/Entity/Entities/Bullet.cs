using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LowRezJam
{
	public class Bullet : Entity
	{
		public float Speed { get; set; }
		public bool IsEnemyBullet { get; set; }
		public bool ShouldRemove { get; set; }
		private float StartY { get; set; }
		public float MaxTravelDistance { get; set; }

		public Bullet(Rectangle Bounds, Texture2D Sprite, Color Tint, float Speed, bool IsEnemyBullet = false, float MaxTravelDistance = 64)
			: base(Bounds, Sprite, Tint)
		{
			this.Speed = Speed;
			this.IsEnemyBullet = IsEnemyBullet;
			this.ShouldRemove = false;
			this.StartY = Y;
			this.MaxTravelDistance = MaxTravelDistance;
		}

		public override void Update(GameTime gameTime)
		{
			if (IsEnemyBullet)
			{
				Y += Speed;
			}
			else
			{
				Y -= Speed;
			}

			if (Math.Abs(Y - StartY) >= MaxTravelDistance)
				ShouldRemove = true;
			
			base.Update(gameTime);
		}
	}
}

