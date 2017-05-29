using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LowRezJam
{
	public interface IEnemy
	{
		int Health { get; set; }
		void Update(GameTime gameTime);
		void Draw(SpriteBatch spriteBatch);
		Rectangle Bounds { get; }
		EnemyManager Parent { get; set; }
	}
}

