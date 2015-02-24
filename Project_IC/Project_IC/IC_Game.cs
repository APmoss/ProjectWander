using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_IC.Framework.GSM;

namespace Project_IC {
	public class IC_Game : Game {
		GraphicsDeviceManager graphics;
		ScreenManager screenManager;

		public IC_Game() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			InitSettings();

			screenManager = new ScreenManager(this);
			Components.Add(screenManager);
		}

		protected override void Initialize() {
			InitScreens();

			base.Initialize();
		}

		protected override void LoadContent() {
			
		}

		protected override void Update(GameTime gameTime) {
			Screens.DebugOverlay.DebugText.AppendLine("-I still like jellybeans!");

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);



			base.Draw(gameTime);
		}

		void InitSettings() {
			IsMouseVisible = true;

			TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);

			graphics.PreferredBackBufferWidth = 1280;
			graphics.PreferredBackBufferHeight = 720;
		}

		void InitScreens() {
			screenManager.AddScreen(new Screens.TestScreen());
			screenManager.AddScreen(new Screens.RandomBackground());
			screenManager.AddScreen(new Screens.DebugOverlay(), true);
		}
	}
}
