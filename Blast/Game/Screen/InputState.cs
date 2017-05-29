using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;

namespace LowRezJam
{
	public class InputState
	{
		public MouseState CurrentMouseState;

		public KeyboardState CurrentKeyboardState;

		public GamePadState CurrentGamePadState;

		public MouseState LastMouseState;

		public KeyboardState LastKeyboardState;

		public GamePadState LastGamePadState;

		public readonly bool[] GamePadWasConnected;

		public TouchCollection TouchState;

		public readonly List<GestureSample> Gestures = new List<GestureSample>();

		public InputState()
		{
			this.CurrentKeyboardState = default(KeyboardState);
			this.CurrentMouseState = default(MouseState);
			this.LastKeyboardState = default(KeyboardState);
			this.LastMouseState = default(MouseState);
		}

		public void Update()
		{
			this.LastKeyboardState = this.CurrentKeyboardState;
			this.LastMouseState = this.CurrentMouseState;
			this.CurrentKeyboardState = Keyboard.GetState();
			this.CurrentMouseState = Mouse.GetState();
			this.TouchState = TouchPanel.GetState();
			this.Gestures.Clear();
			while (TouchPanel.IsGestureAvailable)
			{
				this.Gestures.Add(TouchPanel.ReadGesture());
			}
		}

		public bool IsNewKeyStroke(Keys key)
		{
			return this.CurrentKeyboardState.IsKeyDown(key) && this.LastKeyboardState.IsKeyUp(key);
		}

		public bool IsNewKeyPress(Keys key)
		{
			return this.CurrentKeyboardState.IsKeyDown(key);
		}

		public bool IsNewMouseLeftPress(ButtonState button)
		{
			return this.CurrentMouseState.LeftButton == button && this.LastMouseState.LeftButton != button;
		}

		public bool IsNewMouseRightPress(ButtonState button)
		{
			return this.CurrentMouseState.RightButton == button && this.LastMouseState.RightButton != button;
		}
	}
}
