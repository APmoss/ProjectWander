using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Project_IC.Framework.GSM {
	class ScreenManager : DrawableGameComponent {
		#region Fields
		List<Screen> screens = new List<Screen>();

		InputManager input = new InputManager();
		GraphicsDeviceManager GraphicsDeviceManager;

		public SpriteBatch SpriteBatch;
		public FontLibrary FontLibrary = new FontLibrary();
		public Texture2D Blank;

		bool initialized = false;
		#endregion

		#region Properties
		public Point Res {
			get { return new Point(GraphicsDeviceManager.PreferredBackBufferWidth, GraphicsDeviceManager.PreferredBackBufferHeight); }
			set {
				GraphicsDeviceManager.PreferredBackBufferWidth = value.X;
				GraphicsDeviceManager.PreferredBackBufferHeight = value.Y;
				GraphicsDeviceManager.ApplyChanges();
			}
		}
		public bool FullScreen {
			get { return GraphicsDeviceManager.IsFullScreen; }
			set {
				GraphicsDeviceManager.IsFullScreen = value;
				GraphicsDeviceManager.ApplyChanges();
			}
		}
		#endregion

		public ScreenManager(Game game, GraphicsDeviceManager graphics)
			: base(game) {

			this.GraphicsDeviceManager = graphics;
		}

		public override void Initialize() {
			base.Initialize();

			initialized = true;
		}

		protected override void LoadContent() {
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			FontLibrary.LoadFonts(Game.Content);
			Blank = new Texture2D(GraphicsDevice, 1, 1);
			Blank.SetData<Color>(new Color[] { Color.White });

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

			var screensToUpdate = new List<Screen>();
			screensToUpdate.AddRange(screens);

			bool hasFocus = Game.IsActive;
			bool covered = false;

			Screens.DebugOverlay.DebugText.Append("-Screens (").Append(screens.Count).Append("): { ");

			while (screensToUpdate.Count > 0) {
				var screen = screensToUpdate[screensToUpdate.Count - 1];
				screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

				Screens.DebugOverlay.DebugText.AppendLine().AppendLine(screen.GetType().Name);

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

				Screens.DebugOverlay.DebugText.Append("(").Append(sw.Elapsed.TotalMilliseconds).Append("ms)").AppendLine();
			}

			Screens.DebugOverlay.DebugText.AppendLine().Append("}");
			
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
		public void AddScreen(Screen screen, bool topmost) {
			screen.ScreenManager = this;
			if (initialized) {
				screen.LoadContent();
			}

			// Add to end (on top)
			if (topmost || screens.Count == 0) {
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
		}
	}
}
