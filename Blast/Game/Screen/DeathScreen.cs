using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LowRezJam
{
	public class DeathScreen : Screen
	{
		public AnimatedEntity Door { get; set; }
		public AnimatedEntity Menu { get; set; }
		public Numbers Score { get; set; }

		private bool CanPressUp { get; set; }
		private bool CanPressDown { get; set; }

		public DeathScreen(ScreenManager ScreenManager, int Score)
		{
			this.IsPopup = false;
			this.ScreenManager = ScreenManager;
			this.ScreenState = ScreenState.TransitionOn;
			this.TransitionOffTime = TimeSpan.FromSeconds(1);
			this.TransitionOnTime = TimeSpan.FromSeconds(1);
			this.TransitionPosition = 1f;
			this.IsExiting = false;
			this.Input = new InputState();
			this.Score = new Numbers(new Point(21, 38), Score);
		}

		public override void Update(GameTime gameTime, bool OtherScreenHasFocus, bool CoveredByOtherScreen)
		{
			Door.Update(gameTime);
			Menu.Update(gameTime);
			Score.Update(gameTime);

			if (Door.CurrentAnimation == 1)
			{
				if (Door.FinishedOnce)
				{
					if (Menu.CurrentFrame == 0)
						ScreenManager.AddScreen(new GameScreen(ScreenManager));
					else
						ScreenManager.AddScreen(new MenuScreen(ScreenManager));

					ScreenManager.RemoveScreen(this);
				}
			}
			base.Update(gameTime, OtherScreenHasFocus, CoveredByOtherScreen);
		}

		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin((SpriteSortMode)0, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(4f));

			Menu.Draw(spriteBatch);
			Score.Draw(spriteBatch);
			Door.Draw(spriteBatch);

			spriteBatch.End();
			base.Draw(gameTime);
		}

		public override void HandleInput(InputState Input, GameTime gameTime)
		{
			if (Door.CurrentAnimation == 0 && Door.FinishedOnce)
			{
				if (Input.IsNewKeyPress(Globals.KeyUp) && CanPressUp)
				{
					CanPressUp = false;
					if (Menu.CurrentFrame == 0)
					{
						Menu.CurrentFrame = 1;
					}
					else
					{
						Menu.CurrentFrame = 0;
					}
				}
				else if (Input.IsNewKeyPress(Globals.KeyDown) && CanPressDown)
				{
					CanPressDown = false;
					if (Menu.CurrentFrame == 0)
					{
						Menu.CurrentFrame = 1;
					}
					else
					{
						Menu.CurrentFrame = 0;
					}
				}
				else if (Input.IsNewKeyStroke(Globals.KeyShoot))
				{
					Door.Play(1, false);
				}

				if (Input.CurrentKeyboardState.IsKeyUp(Globals.KeyUp))
					CanPressUp = true;
				if (Input.CurrentKeyboardState.IsKeyUp(Globals.KeyDown))
					CanPressDown = true;
			}

			base.HandleInput(Input, gameTime);
		}

		public override void LoadContent()
		{
			Door = new AnimatedEntity(new Rectangle(0, 0, 64, 64), AssetManager.Sprites["Door"], Color.White, new int[] { 19, 26 }, 0.05);
			Door.Play(0, false);

			CanPressUp = true;
			CanPressDown = true;

			Menu = new AnimatedEntity(new Rectangle(0, 0, 64, 64), AssetManager.Sprites["ScoreMenu"], Color.White, new int[] { 2 }, 0);
			base.LoadContent();
		}
	}
}

