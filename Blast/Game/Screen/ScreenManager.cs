using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace LowRezJam
{
	public class ScreenManager : DrawableGameComponent
	{
		private readonly List<Screen> Screens = new List<Screen>();
		private readonly List<Screen> ScreensToUpdate = new List<Screen>();
		private readonly InputState Input = new InputState();
		private SpriteBatch spriteBatch;
		public Texture2D BlankTexture;
		private bool Initialized;

		public Viewport Viewport
		{
			get { return base.Game.GraphicsDevice.Viewport; }
		}

		public SpriteBatch SpriteBatch
		{
			get
			{
				return spriteBatch;
			}
		}

		public ScreenManager(Game game) : base(game)
		{
			TouchPanel.EnabledGestures = GestureType.None;
		}

		public override void Initialize()
		{
			base.Initialize();
			Initialized = true;
		}

		protected override void LoadContent()
		{
			AssetManager.LoadContent(Game.Content);

			spriteBatch = new SpriteBatch(base.GraphicsDevice);
			BlankTexture = new Texture2D(base.GraphicsDevice, 1, 1);
			BlankTexture.SetData<Color>(new Color[]
			{
				Color.White
			});

			foreach (Screen current in Screens)
			{
				current.LoadContent();
			}
		}

		protected override void UnloadContent()
		{
			foreach (Screen current in Screens)
			{
				current.UnloadContent();
			}
		}

		public override void Update(GameTime gameTime)
		{
			Input.Update();

			ScreensToUpdate.Clear();
			foreach (Screen current in Screens)
			{
				ScreensToUpdate.Add(current);
			}

			bool OtherScreenHasFocus = !base.Game.IsActive;
			bool CoveredByOtherScreen = false;

			while (ScreensToUpdate.Count > 0)
			{
				Screen screen = ScreensToUpdate[ScreensToUpdate.Count - 1];
				ScreensToUpdate.RemoveAt(ScreensToUpdate.Count - 1);
				screen.Update(gameTime, OtherScreenHasFocus, CoveredByOtherScreen);

				if (screen.ScreenState == ScreenState.TransitionOn || screen.ScreenState == ScreenState.Active)
				{
					if (!OtherScreenHasFocus)
					{
						screen.HandleInput(Input, gameTime);
						OtherScreenHasFocus = true;
					}
					if (!screen.IsPopup)
					{
						CoveredByOtherScreen = true;
					}
				}
			}
		}

		public override void Draw(GameTime gameTime)
		{
			foreach (Screen current in Screens)
			{	
				if (current.ScreenState != ScreenState.Hidden)
				{
					current.Draw(gameTime);
				}
			}
		}

		public void AddScreen(Screen screen)
		{
			screen.ScreenManager = this;
			screen.IsExiting = false;
			bool isInitialized = Initialized;
			if (isInitialized)
			{
				screen.LoadContent();
			}
			Screens.Add(screen);
		}

		public void RemoveScreen(Screen screen)
		{
			bool isInitialized = Initialized;
			if (isInitialized)
			{
				screen.UnloadContent();
			}
			Screens.Remove(screen);
			ScreensToUpdate.Remove(screen);
		}

		public Screen[] GetScreens()
		{
			return Screens.ToArray();
		}

		public void FadeBackBufferToBlack(float alpha)
		{
			Viewport viewport = base.GraphicsDevice.Viewport;
			spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, null);
			spriteBatch.Draw(BlankTexture, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.Black * alpha);
			spriteBatch.End();
		}
	}
}

