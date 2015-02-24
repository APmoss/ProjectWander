using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_IC.Framework.GSM {
	class ScreenManager : DrawableGameComponent {
		#region Fields
		List<Screen> screens = new List<Screen>();
		List<Screen> screensToUpdate = new List<Screen>();

		InputManager input = new InputManager();

		public SpriteBatch SpriteBatch;
		public FontLibrary FontLibrary = new FontLibrary();

		bool initialized = false;
		#endregion

		public ScreenManager(Game game)
			: base(game) { }

		#region Methods
		public override void Initialize() {
			base.Initialize();

			initialized = true;
		}

		protected override void LoadContent() {
			//TODO: Load stuff for fonts, spritebatch, etc.
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			FontLibrary.LoadFonts(Game.Content);

			foreach (var screen in screens) {
				screen.LoadContent();
			}

			base.LoadContent();
		}

		protected override void UnloadContent() {
			foreach (var screen in screens) {
				screen.UnloadContent();
			}
			
			base.UnloadContent();
		}

		public override void Update(GameTime gameTime) {
			input.Update();

			screensToUpdate.Clear();
			screensToUpdate.AddRange(screens);

			bool hasFocus = Game.IsActive;
			bool covered = false;

			while (screensToUpdate.Count > 0) {
				var screen = screensToUpdate[screensToUpdate.Count - 1];
				screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

				screen.Update(gameTime, hasFocus, covered);

				if (screen.ScreenState == ScreenState.TransitionOn || screen.ScreenState == ScreenState.Active) {
					if (hasFocus) {
						screen.UpdateInput(input);

						hasFocus = false;
					}

					if (!screen.IsPopup) {
						covered = true;
					}
				}
			}
			
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime) {
			foreach (var screen in screens) {
				if (screen.ScreenState != ScreenState.Hidden) {
					screen.Draw(gameTime);
				}
			}
			
			base.Draw(gameTime);
		}

		public void AddScreen(Screen screen) {
			screen.ScreenManager = this;
			if (initialized) {
				screen.LoadContent();
			}

			screens.Add(screen);
		}

		public void RemoveScreen(Screen screen) {
			if (initialized) {
				screen.UnloadContent();
			}

			screens.Remove(screen);
			screensToUpdate.Remove(screen);
		}
		#endregion
	}
}
