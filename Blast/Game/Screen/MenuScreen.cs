using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LowRezJam
{
	public class MenuScreen : Screen
	{
		public AnimatedEntity Door { get; set; }
		public AnimatedEntity Menu { get; set; }

		private bool CanPressUp { get; set; }
		private bool CanSelect { get; set; }
		private bool CanPressDown { get; set; }

		private bool IsDoorClosing { get; set; }

		public MenuScreen(ScreenManager ScreenManager)
		{
			this.IsPopup = false;
			this.ScreenManager = ScreenManager;
			this.ScreenState = ScreenState.TransitionOn;
			this.TransitionOffTime = TimeSpan.FromSeconds(1);
			this.TransitionOnTime = TimeSpan.FromSeconds(1);
			this.TransitionPosition = 1f;
			this.IsExiting = false;
			this.Input = new InputState();
		}

		public override void Update(GameTime gameTime, bool OtherScreenHasFocus, bool CoveredByOtherScreen)
		{
			Door.Update(gameTime);

			if (Door.FinishedOnce == true && Door.CurrentAnimation == 1)
			{
				ScreenManager.AddScreen(new GameScreen(ScreenManager));
				ScreenManager.RemoveScreen(this);
			}

			base.Update(gameTime, OtherScreenHasFocus, CoveredByOtherScreen);
		}

		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin((SpriteSortMode)0, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(4f));

			Menu.Draw(spriteBatch);
			Door.Draw(spriteBatch);

			spriteBatch.End();

			base.Draw(gameTime);
		}

		public override void HandleInput(InputState Input, GameTime gameTime)
		{
			if (Door.FinishedOnce && !IsDoorClosing)
			{
				if (Input.IsNewKeyPress(Globals.KeyUp) && CanPressUp)
				{
					CanPressUp = false;
					int Frame = Menu.CurrentFrame;
					if (Frame == 0)
						Frame = 1;
					else
						Frame--;
					Menu.CurrentFrame = Frame;
				}
				else if (Input.IsNewKeyPress(Globals.KeyDown) && CanPressDown)
				{
					CanPressDown = false;
					int Frame = Menu.CurrentFrame;
					if (Frame == 1)
						Frame = 0;
					else
						Frame++;
					Menu.CurrentFrame = Frame;
				}
				else if (Input.IsNewKeyPress(Globals.KeyShoot) && CanSelect)
				{
					CanSelect = false;
					switch (Menu.CurrentFrame)
					{
						case 0:
							IsDoorClosing = true;
							Door.Play(1, false);
							break;

						case 1:
							System.Diagnostics.Process.Start("http://pema99.net");
							break;
					}
				}


				if (Input.CurrentKeyboardState.IsKeyUp(Globals.KeyUp))
					CanPressUp = true;
				if (Input.CurrentKeyboardState.IsKeyUp(Globals.KeyDown))
					CanPressDown = true;
				if (Input.CurrentKeyboardState.IsKeyUp(Globals.KeyShoot))
					CanSelect = true;
			}
			base.HandleInput(Input, gameTime);
		}

		public override void LoadContent()
		{
			Door = new AnimatedEntity(new Rectangle(0, 0, 64, 64), AssetManager.Sprites["Door"], Color.White, new int[] { 19, 26 }, 0.05f);
			Door.Play(0, false);

			Menu = new AnimatedEntity(new Rectangle(0, 0, 64, 64), AssetManager.Sprites["BlastMenu"], Color.White, new int[] { 2 }, 0f);

			CanSelect = true;
			CanPressUp = true;
			CanPressDown = true;

			IsDoorClosing = false;

			base.LoadContent();
		}

		public override void UnloadContent()
		{
			base.UnloadContent();
		}
	}
}

