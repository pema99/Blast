using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LowRezJam
{
    public class Camera
    {
		private const float YOffset = 20;
		private Random Rand;

		public Camera(ScreenManager ScreenManager, Entity Focus, float Speed, float Scale = 1f, float Rotation = 0f)
        {
            View = ScreenManager.GraphicsDevice.Viewport;
            this.Focus = Focus;
			this.Dampening = Speed;
            this.Scale = Scale;
            this.Rotation = Rotation;
			this.CamPos = new Vector2(Focus.X + Focus.Width/2, Focus.Y - YOffset + Focus.Height/2);
			this.Rand = new Random();

            ScreenCenter = new Vector2(View.Width / 2, View.Height / 2);
		}
			
		public float Dampening { get; set; }
		public Viewport View { get; set; }
        public Matrix Transform { get; set; }
        public Entity Focus { get; set; }
        public Vector2 CamPos { get; protected set; } 
        public float Rotation { get; set; }
        public float Scale { get; set; }
        public Vector2 TopLeft { get; protected set; }
        public Vector2 ScreenCenter { get; protected set; }
        public Vector2 Origin { get; protected set; }

		private float ScreenShakeDampening { get; set; }
		private float ScreenShakeSize { get; set; }

        public void Update(GameTime gameTime)
        {
			Vector2 ToCam = new Vector2(Focus.X+Focus.Width/2,Focus.Y-YOffset+Focus.Height/2) - CamPos;
			ToCam *= 1f-Dampening;
			CamPos += ToCam;

            Transform = Matrix.Identity *
                        Matrix.CreateTranslation(-(int)CamPos.X, -(int)CamPos.Y, 0) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
                        Matrix.CreateScale(new Vector3(Scale, Scale, Scale));

			//Screenshake
			float OffsetX = ((float)Rand.NextDouble() * (1 - (-1)) + -1)*ScreenShakeSize;
			float OffsetY = ((float)Rand.NextDouble() * (1 - (-1)) + -1)*ScreenShakeSize;
			Vector2 Offset = new Vector2(OffsetX, OffsetY);
			ScreenShakeSize *= 1f - ScreenShakeDampening;

			Origin = (ScreenCenter + Offset) / Scale;

			TopLeft = new Vector2(CamPos.X - View.Width / 2, CamPos.Y - View.Height / 2);
        }

        public bool IsOnScreen(Rectangle rect)
        {
            //If the object is not within the horizontal bounds of the screen
            if ((rect.X + rect.Width) < (CamPos.X - Origin.X) || (rect.X) > (CamPos.X + Origin.X))
                return false;

            //If the object is not within the vertical bounds of the screen
            if ((rect.Y + rect.Height) < (CamPos.Y - Origin.Y) || (rect.Y) > (CamPos.Y + Origin.Y))
                return false;

            //In View
            return true;
        }

		public void ScreenShake(float Amount, float Dampening)
		{
			if (Amount > ScreenShakeSize)
			{
				this.ScreenShakeSize = Amount;
				this.ScreenShakeDampening = Dampening;
			}
		}
    }
}
