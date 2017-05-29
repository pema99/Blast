using System;
using Microsoft.Xna.Framework;

namespace LowRezJam
{
	public class HealthPickup : Entity
	{
		public HealthPickup(Point Pos)
			: base(new Rectangle(Pos.X, Pos.Y, 9, 9), AssetManager.Sprites["HealthPickup"], Color.White)
		{
		}
	}
}

