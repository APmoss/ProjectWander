using System;
using Microsoft.Xna.Framework;

namespace Project_IC.Framework.GSM {
	public enum ScreenState {
		TransitionOn, Active, TransitionOff, Hidden
	}

	abstract class Screen {
		#region Fields
		public bool IsPopup = false;
		protected TimeSpan TransitionOn = TimeSpan.FromSeconds(.5);
		protected TimeSpan TransitionOff = TimeSpan.FromSeconds(.5);
		protected float TransitionPosition = 1f;

		public ScreenState ScreenState = ScreenState.TransitionOn;
		protected internal bool Exiting = false;
		protected internal bool InputFallThrough = false;
		protected bool HasFocus = true;

		protected internal ScreenManager ScreenManager;
		#endregion

		#region Properties
		public bool Active {
			get { return HasFocus && (ScreenState == GSM.ScreenState.TransitionOn ||
								  ScreenState == GSM.ScreenState.Active); }
		}
		#endregion

		public virtual void LoadContent() { }

		public virtual void UnloadContent() { }

		public virtual void Update(GameTime gameTime, bool hasFocus, bool covered) {
			this.HasFocus = hasFocus;

			if (Exiting) {
				ScreenState = GSM.ScreenState.TransitionOff;

				if (!updateTransition(gameTime, TransitionOff, 1)) {
					ScreenManager.RemoveScreen(this);
				}
			}
			else if (covered) {
				if (updateTransition(gameTime, TransitionOff, 1)) {
					ScreenState = GSM.ScreenState.TransitionOff;
				}
				else {
					ScreenState = GSM.ScreenState.Hidden;
				}
			}
			else {
				if (updateTransition(gameTime, TransitionOn, -1)) {
					ScreenState = GSM.ScreenState.TransitionOn;
				}
				else {
					ScreenState = GSM.ScreenState.Active;
				}
			}
		}

		public virtual void UpdateInput(InputManager input) { }

		public virtual void Draw(GameTime gameTime) { }

		bool updateTransition(GameTime gameTime, TimeSpan transitionTime, int direction) {
			float transitionDelta = 0;

			if (transitionTime == TimeSpan.Zero) {
				transitionDelta = 1;
			}
			else {
				transitionDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / transitionTime.TotalMilliseconds);
			}

			TransitionPosition = MathHelper.Clamp(TransitionPosition + (transitionDelta * direction), 0, 1);

			if (TransitionPosition == 0 || TransitionPosition == 1) {
				// Returns false if we are done transitioning
				return false;
			}

			// Returns true if we still need to transition
			return true;
		}

		public void ExitScreen() {
			if (TransitionOff == TimeSpan.Zero) {
				ScreenManager.RemoveScreen(this);
			}
			else {
				Exiting = true;
			}
		}
	}
}
