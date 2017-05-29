using System;
using Microsoft.Xna.Framework;

namespace LowRezJam
{
	public class ShooterEnemy : AnimatedEntity, IEnemy
	{
		private double InitialX { get; set; }
		public int Health { get; set; }
		public EnemyManager Parent { get; set; }
		public double ShootTime { get; set; }
		public double CurrentTime { get; set; }
		public bool HasShot { get; set; }

		public ShooterEnemy(Rectangle Bounds, EnemyManager Parent)
			: base(Bounds, AssetManager.Sprites["ShooterEnemy"], Color.White, new int[] { 9 }, 0.2f)
		{
			this.InitialX = Bounds.X;
			this.Play(0, true);
			this.Health = 2;
			this.Parent = Parent;
			this.ShootTime = Parent.Rand.Next(1, 4);
			this.CurrentTime = 0;
			this.HasShot = false;
		}

		public override void Update(GameTime gameTime)
		{
			Y += 0.4f;

			if (Y > 320)
				Y = 0;

			CurrentTime += gameTime.ElapsedGameTime.TotalSeconds;
			if (CurrentTime > ShootTime && !HasShot)
			{
				HasShot = true;
				Parent.Bullets.Add(new Bullet(new Rectangle((int)X+2,(int)Y+13, 3, 4), AssetManager.Sprites["EnemyBullet"], Color.White, 1, true, 32));
				if (Parent.Parent.Camera.IsOnScreen(this.Bounds))
					AssetManager.PlaySoundRandomPitch("Shoot", 0.3f, 0.3f, -0.3f);
			}

			if (HasShot)
			{
				HasShot = false;
				ShootTime = Parent.Rand.Next(1, 4);
				CurrentTime = 0;
			}

			base.Update(gameTime);
		}
	}
}

