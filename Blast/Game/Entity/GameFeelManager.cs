using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LowRezJam
{
	public class GameFeelManager
	{
		public GameScreen Parent { get; set; }
		public List<AnimatedEntity> Explosions { get; set; }
		
		public GameFeelManager(GameScreen Parent)
		{
			this.Parent = Parent;
			this.Explosions = new List<AnimatedEntity>();
		}

		public void Update(GameTime gameTime)
		{
			foreach (AnimatedEntity e in Explosions.ToArray())
			{
				e.Update(gameTime);
				if (e.FinishedOnce)
					Explosions.Remove(e);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (AnimatedEntity e in Explosions.ToArray())
			{
				e.Draw(spriteBatch);
			}
		}

		public void SpawnExplosion(Entity Target)
		{
			SpawnExplosion(Target.Bounds.Center.X, Target.Bounds.Center.Y);
		}

		public void SpawnMiniExplosion(Entity Target)
		{
			SpawnMiniExplosion(Target.Bounds.Center.X, Target.Bounds.Center.Y);
		}

		public void SpawnExplosion(IEnemy Target)
		{
			SpawnExplosion(Target.Bounds.Center.X, Target.Bounds.Center.Y);
		}

		public void SpawnMiniExplosion(IEnemy Target)
		{
			SpawnMiniExplosion(Target.Bounds.Center.X, Target.Bounds.Center.Y);
		}

		public void SpawnExplosion(int X, int Y)
		{
			AnimatedEntity Explosion = new AnimatedEntity(new Rectangle(X - 12, Y - 12, 25, 24), AssetManager.Sprites["Explosion"], Color.White, new int[] { 8 }, 0.1f);
			Explosion.Play(0, false);
			Explosions.Add(Explosion);
			AssetManager.PlaySoundRandomPitch("Explosion", 1, 0.2f, -0.2f);
		}

		public void SpawnMiniExplosion(int X, int Y)
		{
			AnimatedEntity Explosion = new AnimatedEntity(new Rectangle(X - 3, Y - 3, 7, 7), AssetManager.Sprites["MiniExplosion"], Color.White, new int[] { 2 }, 0.1f);
			Explosion.Play(0, false);
			Explosions.Add(Explosion);
		}
	}
}

