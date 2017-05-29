using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LowRezJam
{
	//TODO: REFACTOR THIS CLASS
	public class EnemyManager
	{
		public List<IEnemy> Enemies { get; set; }
		private List<IEnemy> ToRemove { get; set; }
		public List<Bullet> Bullets { get; set; }
		public GameScreen Parent { get; set; }
		public Random Rand { get; set; }

		private double CurrentTime;
		public double SpawnInterval;

		public EnemyManager(GameScreen Parent)
		{
			this.Parent = Parent;
			this.Enemies = new List<IEnemy>();
			this.Rand = new Random();
			this.ToRemove = new List<IEnemy>();
			this.Bullets = new List<Bullet>();

			this.SpawnInterval = 2;
			this.CurrentTime = SpawnInterval;
		}

		public void Update(GameTime gameTime)
		{
			foreach (Bullet b in Bullets.ToArray())
			{
				b.Update(gameTime);
				if (b.Bounds.Intersects(Parent.Player.Bounds))
				{
					Parent.Health.Value--;
					AssetManager.PlaySound("Hit", 1f);
					Parent.GFM.SpawnMiniExplosion(b);
					Bullets.Remove(b);
				}
				else if (b.ShouldRemove)
				{					
					Parent.GFM.SpawnMiniExplosion(b);
					Bullets.Remove(b);
				}
			}

			CurrentTime += gameTime.ElapsedGameTime.TotalSeconds;
			if (CurrentTime >= SpawnInterval)
			{
				CurrentTime = 0;
				for (int i = 0; i < Rand.Next(5); i++)
				{
					//Spawn at top of screen, choose random enemy
					int R = Rand.Next(4);
					if (R == 1)
					{
						Enemies.Add(new ShooterEnemy(new Rectangle(Rand.Next(64, 64 + 192 - 9), Rand.Next(-32, 0), 7, 14), this));
					}
					else if (R == 2)
					{
						Enemies.Add(new FighterEnemy(new Rectangle(Rand.Next(64, 64 + 192 - 9), Rand.Next(-32, 0), 9, 13), this));
					}
					else if (R == 3)
					{
						Enemies.Add(new FollowEnemy(new Rectangle(Rand.Next(64, 64 + 192 - 9), Rand.Next(-32, 0), 9, 9), this));
					}
					else
					{
						Enemies.Add(new SpikeEnemy(new Rectangle(Rand.Next(64, 64 + 192 - 9), Rand.Next(-32, 0), 13, 13), this));
					}
				}
			}

			ToRemove.Clear();
			foreach (IEnemy E in Enemies)
			{
				E.Update(gameTime);

				if (E.Bounds.Intersects(Parent.Player.Bounds))
				{
					ToRemove.Add(E);
					Parent.Health.Value--;
				}

				foreach (Bullet B in Parent.Player.Bullets.ToArray())
				{
					if (B.Bounds.Intersects(E.Bounds))
					{
						E.Health--;
						Parent.GFM.SpawnMiniExplosion(E);
						if (E.Health <= 0)
						{
							Parent.Score.Value++;
							ToRemove.Add(E);
						}
						else
							AssetManager.PlaySound("Hit");
						Parent.Player.Bullets.Remove(B);
					}
				}
			}

			foreach (IEnemy E in ToRemove)
			{
				if (Enemies.Contains(E))
				{
					Parent.GFM.SpawnExplosion(E);
					Parent.Camera.ScreenShake(20, 0.075f);
					Enemies.Remove(E);	
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (IEnemy E in Enemies)
				E.Draw(spriteBatch);
			foreach (Bullet b in Bullets)
				b.Draw(spriteBatch);
		}
	}
}

