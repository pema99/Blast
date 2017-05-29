using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LowRezJam
{
	public class PickupManager
	{
		private GameScreen Parent { get; set; }
		private Random Rand { get; set; }
		public List<HealthPickup> HealthPickups { get; set; }
		private double Time = 0;

		public PickupManager(GameScreen Parent)
		{
			this.Parent = Parent;
			this.Rand = new Random();
			this.HealthPickups = new List<HealthPickup>();
		}

		public void Update(GameTime gameTime)
		{
			Time -= gameTime.ElapsedGameTime.TotalSeconds;

			if (Time < 0)
				Time = 0;
			
			if (HealthPickups.Count == 0 && Time <= 0)
				HealthPickups.Add(new HealthPickup(new Point(Rand.Next(64, 64 + 192 - 9), Rand.Next(64, 64 + 192 - 9))));

			foreach (HealthPickup H in HealthPickups.ToArray())
			{
				H.Update(gameTime);

				if (H.Bounds.Intersects(Parent.Player.Bounds))
				{
					if (Parent.Health.Value < 4)
						Parent.Health.Value++;
					HealthPickups.Remove(H);
					Time = 20;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (HealthPickup H in HealthPickups)
				H.Draw(spriteBatch);
		}
	}
}

