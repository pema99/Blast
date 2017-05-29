using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace LowRezJam
{
	public class GameScreen : Screen
	{
		public Random Rand = new Random();

		public GameFeelManager GFM { get; set; }
		public Background Background { get; set; }
        public AnimatedEntity Door { get; set; }
        public Player Player { get; set; }
		public Camera Camera { get; set; }
		public Numbers Score { get; set; }
		public EnemyManager EnemyManager { get; set; }
		public Health Health { get; set; }
		public PickupManager PickupManager { get; set; }

        public GameScreen(ScreenManager ScreenManager)
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
			//Transition out
			if (Door.CurrentAnimation == 0)
			{
				Background.Update(gameTime);
				Player.Update(gameTime);
				Camera.Update(gameTime);
				Score.Update(gameTime);
				EnemyManager.Update(gameTime);
				Health.Update(gameTime);
				GFM.Update(gameTime);
				PickupManager.Update(gameTime);
			}
			else
			{
				if (Door.FinishedOnce)
				{
					ScreenManager.AddScreen(new DeathScreen(ScreenManager, Score.Value));
					ScreenManager.RemoveScreen(this);
				}
			}
			Door.Update(gameTime);

			base.Update(gameTime, OtherScreenHasFocus, CoveredByOtherScreen);
		}

		public override void Draw(GameTime gameTime)
		{
			//Background
			spriteBatch.Begin((SpriteSortMode)0, null, SamplerState.PointClamp, null, null, null, Camera.Transform);
			Background.Draw(spriteBatch);
			EnemyManager.Draw(spriteBatch);
			PickupManager.Draw(spriteBatch);
			Player.Draw(spriteBatch);
			GFM.Draw(spriteBatch);
			spriteBatch.End();

			//Foreground
			spriteBatch.Begin((SpriteSortMode)0, null, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(4f));
			Score.Draw(spriteBatch);
			Health.Draw(spriteBatch);
            Door.Draw(spriteBatch);
			spriteBatch.End();

			base.Draw(gameTime);
		}

		public override void HandleInput(InputState Input, GameTime gameTime)
		{
			if (Input.IsNewKeyPress(Keys.F10))
				Camera.ScreenShake(30, 0.009f);
			if (Input.IsNewKeyStroke(Keys.F9))
				Health.Value = 0;
			if (Input.IsNewKeyStroke(Keys.F8))
				GFM.SpawnExplosion(Player);
			
            Player.HandleInput(Input, gameTime);
			base.HandleInput(Input, gameTime);
		}

		public override void LoadContent()
		{
			Door = new AnimatedEntity(new Rectangle(0, 0, 64, 64), AssetManager.Sprites["Door"], Color.White, new int[] { 19, 26 }, 0.05);
            Door.Play(0, false);

            Player = new Player(this, new Rectangle(78, 78, 16, 12), AssetManager.Sprites["SpaceShip"], Color.White, 0.1f, 1.1f, 0.1f, 0.25, 2);

			Camera = new Camera(ScreenManager, Player, 0.78f, 4);

			Background = new Background(Camera);

			Score = new Numbers(new Point(1, 1));

			EnemyManager = new EnemyManager(this);

			Health = new Health(new Point(64-1-23, 1), this);

			GFM = new GameFeelManager(this);

			PickupManager = new PickupManager(this);

			base.LoadContent();
		}

		public void Die()
		{
			//TODO: Change this part
			this.Door.Play(1, false);
			this.TransitionPosition = 1f;
			this.Input = new InputState();
		}
	}
}

