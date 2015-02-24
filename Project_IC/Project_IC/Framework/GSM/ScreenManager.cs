﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

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

			Screens.DebugOverlay.DebugText.Append("-Screens (").Append(screens.Count).Append("): { ");

			while (screensToUpdate.Count > 0) {
				var screen = screensToUpdate[screensToUpdate.Count - 1];
				screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

				var sw = Stopwatch.StartNew();
				screen.Update(gameTime, hasFocus, covered);
				sw.Stop();

				if (screen.ScreenState == ScreenState.TransitionOn || screen.ScreenState == ScreenState.Active) {
					if (screen.InputFallThrough) {
						screen.UpdateInput(input);
					}
					else if (hasFocus) {
						screen.UpdateInput(input);

						hasFocus = false;
					}

					if (!screen.IsPopup) {
						covered = true;
					}
				}

				Screens.DebugOverlay.DebugText.Append(screen.GetType().Name).Append(" (").Append(sw.Elapsed.TotalMilliseconds).Append("ms) ");
			}

			Screens.DebugOverlay.DebugText.Append("}");
			
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
			// Default to adding to end of collection
			AddScreen(screen, false);
		}
		public void AddScreen(Screen screen, bool addToTop) {
			screen.ScreenManager = this;
			if (initialized) {
				screen.LoadContent();
			}

			// Add to end (on top)
			if (addToTop || screens.Count == 0) {
				screens.Add(screen);
			}
			// Add to end - 1 (just below the top) 
			else {
				screens.Insert(screens.Count - 1, screen);
			}
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