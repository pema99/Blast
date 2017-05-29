using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LowRezJam
{
    public class Entity
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Texture2D Sprite { get; set; }
        public Color Tint { get; set; }
		public Rectangle Bounds { get { return new Rectangle((int)X, (int)Y, Width, Height); } }

        public Entity(Rectangle Bounds, Texture2D Sprite, Color Tint)
        {
            this.X = Bounds.X;
            this.Y = Bounds.Y;
            this.Width = Bounds.Width;
            this.Height = Bounds.Height;
            this.Sprite = Sprite;
            this.Tint = Tint;
        }

        public virtual void Update(GameTime gameTime) 
        {
			
        }

		public virtual void HandleInput(InputState Input, GameTime gameTime)
        {

        }

		public virtual void HandleInput(InputState Input)
		{

		}

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, new Rectangle((int)X, (int)Y, Width, Height), Tint);
        }
    }
}
