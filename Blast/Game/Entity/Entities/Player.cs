using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace LowRezJam
{
    public class Player : Entity
    {
		private GameScreen Parent { get; set; }
        public float Speed { get; set; }
        public Vector2 Velocity { get; set; }
        public float MaxVel { get; set; }
        public float Dampening { get; set; }
		public double ShootInterval { get; set; }
		public List<Bullet> Bullets { get; set; }
		private double CurrentTime { get; set; }
		public float ShotSpeed { get; set; }

		private readonly Point[] ShootPoints = new Point[]
		{
			new Point(1, 2),
			//new Point(6, 0),
			new Point(12, 2),
		};

		public Player(GameScreen Parent, Rectangle Bounds, Texture2D Sprite, Color Tint, float Speed, float MaxVel, float Dampening, double ShootInterval, float ShotSpeed)
            : base(Bounds, Sprite, Tint)
        {
			this.Parent = Parent;
            this.Speed = Speed;
            this.Velocity = Vector2.Zero;
            this.MaxVel = MaxVel;
            this.Dampening = Dampening;
			this.ShootInterval = ShootInterval;
			this.CurrentTime = 0;
			this.Bullets = new List<Bullet>();
			this.ShotSpeed = ShotSpeed;
        }

        public override void Update(GameTime gameTime)
        {
			foreach (Bullet b in Bullets.ToArray())
			{
				b.Update(gameTime);

				if (b.Y < 0 || b.ShouldRemove)
					Bullets.Remove(b);
			}

            if (Velocity.X > MaxVel)
                Velocity = new Vector2(MaxVel, Velocity.Y);
            if (Velocity.X < -MaxVel)
                Velocity = new Vector2(-MaxVel, Velocity.Y);
            if (Velocity.Y > MaxVel)
                Velocity = new Vector2(Velocity.X, MaxVel);
            if (Velocity.Y < -MaxVel)
                Velocity = new Vector2(Velocity.X, -MaxVel);

            X += Velocity.X;
            Y += Velocity.Y;

            Velocity *= (1f - Dampening);

            if (X < 64+1)
                X = 64+1;
            if (X + Width > 64-1+192)
				X = 64-1+192 - Width;
			if (Y < 64+1)
				Y = 64+1;
			if (Y + Height > 64-1+192)
				Y = 64-1+192 - Height;

			CurrentTime += gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

		public override void Draw(SpriteBatch spriteBatch)
		{
			foreach (Bullet b in Bullets)
			{
				b.Draw(spriteBatch);
			}

			base.Draw(spriteBatch);
		}

		public override void HandleInput(InputState Input, GameTime gameTime)
        {
			float DeltaTime = (float)Math.Round(gameTime.ElapsedGameTime.TotalSeconds, 2);
			float DeltaSpeed = Speed;// * DeltaTime;

            if (Input.IsNewKeyPress(Globals.KeyUp))
            {
				Velocity -= new Vector2(0, DeltaSpeed);
            }
            if (Input.IsNewKeyPress(Globals.KeyDown))
            {
				Velocity += new Vector2(0, DeltaSpeed);
            }
            if (Input.IsNewKeyPress(Globals.KeyLeft))
            {
				Velocity -= new Vector2(DeltaSpeed, 0);
            }
            if (Input.IsNewKeyPress(Globals.KeyRight))
            {
				Velocity += new Vector2(DeltaSpeed, 0);
            }

			//Shoot
			if (Input.IsNewKeyPress(Globals.KeyShoot) && CurrentTime >= ShootInterval)
			{
				CurrentTime = 0f;

				AssetManager.PlaySoundRandomPitch("Shoot", 1, 0.3f, -0.3f);

				for (int i = 0; i < ShootPoints.Length; i++)
					Bullets.Add(new Bullet(new Rectangle(ShootPoints[i].X+(int)X, ShootPoints[i].Y+(int)Y, 3, 4), AssetManager.Sprites["Bullet"], Color.White, ShotSpeed));
			
				Parent.Camera.ScreenShake(5, 0.1f);
			}
        }
    }
}
