using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project_IC.Framework.GSM {
	public enum MouseButton {
		Left, Middle, Right, XButton1, XButton2
	}

	class InputManager {
		#region Field
		public const int MAX_INPUTS = 4;

		public KeyboardState KeyboardState;
		public GamePadState[] GamePadStates;
		public MouseState MouseState;

		public KeyboardState LastKeyboardState;
		public GamePadState[] LastGamePadStates;
		public MouseState LastMouseState;
		#endregion

		public InputManager() {
			KeyboardState = new KeyboardState();
			GamePadStates = new GamePadState[MAX_INPUTS];
			MouseState = new MouseState();
			
			LastKeyboardState = new KeyboardState();
			LastGamePadStates = new GamePadState[MAX_INPUTS];
			LastMouseState = new MouseState();
		}

		#region Methods
		public void Update() {
			LastKeyboardState = KeyboardState;
			LastMouseState = MouseState;

			KeyboardState = Keyboard.GetState();
			MouseState = Mouse.GetState();

			for (int i = 0; i < MAX_INPUTS; i++) {
				LastGamePadStates[i] = GamePadStates[i];
				
				GamePadStates[i] = GamePad.GetState((PlayerIndex)i);
			}
		}

		public bool IsKeyDown(Keys key) {
			return KeyboardState.IsKeyDown(key);
		}
		public bool IsButtonDown(Buttons button, PlayerIndex gamePad) {
			return GamePadStates[(int)gamePad].IsButtonDown(button);
		}
		public bool IsMouseDown(MouseButton mouseButton) {
			switch (mouseButton) {
				case MouseButton.Left:
					return MouseState.LeftButton == ButtonState.Pressed;
				case MouseButton.Middle:
					return MouseState.MiddleButton == ButtonState.Pressed;
				case MouseButton.Right:
					return MouseState.RightButton == ButtonState.Pressed;
				case MouseButton.XButton1:
					return MouseState.XButton1 == ButtonState.Pressed;
				case MouseButton.XButton2:
					return MouseState.XButton2 == ButtonState.Pressed;
				default:
					return false;
			}
		}

		public bool IsKeyPressed(Keys key) {
			return IsKeyDown(key) && LastKeyboardState.IsKeyUp(key);
		}
		public bool IsButtonPressed(Buttons button, PlayerIndex gamePad) {
			return IsButtonDown(button, gamePad) && LastGamePadStates[(int)gamePad].IsButtonUp(button);
		}
		public bool IsMousePressed(MouseButton mouseButton) {
			switch (mouseButton) {
				case MouseButton.Left:
					return IsMouseDown(mouseButton) && LastMouseState.LeftButton == ButtonState.Released;
				case MouseButton.Middle:
					return IsMouseDown(mouseButton) && LastMouseState.MiddleButton == ButtonState.Released;
				case MouseButton.Right:
					return IsMouseDown(mouseButton) && LastMouseState.RightButton == ButtonState.Released;
				case MouseButton.XButton1:
					return IsMouseDown(mouseButton) && LastMouseState.XButton1 == ButtonState.Released;
				case MouseButton.XButton2:
					return IsMouseDown(mouseButton) && LastMouseState.XButton2 == ButtonState.Released;
				default:
					return false;
			}
		}
		#endregion
	}
}
