using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LowRezJam
{
    public class AnimatedEntity : Entity
    {
        public int CurrentFrame { get; set; }
        public int[] FrameAmounts { get; set; }
        public double TimePerFrame { get; set; }
        public int CurrentAnimation { get; protected set; }
        public bool Looping { get; set; }
        public bool Paused { get; set; }
		public bool FinishedOnce { get; set; }
        
		private double TotalElapsed { get; set; }

        public AnimatedEntity(Rectangle Bounds, Texture2D Sprite, Color Tint, int[] FrameAmounts, double TimePerFrame)
            : base(Bounds, Sprite, Tint)
        {
            this.CurrentFrame = 0;
            this.FrameAmounts = FrameAmounts;
            this.TimePerFrame = TimePerFrame;
            this.CurrentAnimation = 0;
            this.Looping = false;
            this.Paused = true;
            this.FinishedOnce = false;
            this.TotalElapsed = 0;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle SourceRect = new Rectangle(Width * CurrentFrame, CurrentAnimation * Height, Width, Height);
            spriteBatch.Draw(Sprite, new Rectangle((int)X, (int)Y, Width, Height), SourceRect, Tint);
        }

        public override void Update(GameTime gameTime)
        {
            TotalElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            if (TotalElapsed > TimePerFrame)
            {
                if (!Paused)
                {
                    if (!FinishedOnce || (FinishedOnce && Looping))
                    {
                        CurrentFrame++;
                        // Keep the Frame between 0 and the total frames, minus one.
                        CurrentFrame = CurrentFrame % FrameAmounts[CurrentAnimation];
                        TotalElapsed -= TimePerFrame;

                        //If this is last frame
                        if (CurrentFrame == FrameAmounts[CurrentAnimation] - 1)
                        {
                            FinishedOnce = true;
                        }
                    }
                }
            }

            base.Update(gameTime);
        }

        public void Play(int Animation, bool Loop = true)
        {
            Play(Animation, this.TimePerFrame, Loop);
        }

        public void Play(int Animation, double TimePerFrame, bool Loop = true)
        {
            Looping = Loop;
            CurrentFrame = 0;
            CurrentAnimation = Animation;
            FinishedOnce = false;
            Paused = false;
			TotalElapsed = 0;
        }
    }
}
