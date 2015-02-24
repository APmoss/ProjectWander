using System;
using System.Collections.Generic;
using Project_IC.Framework.GSM;
using Microsoft.Xna.Framework;
using Project_IC.Framework.Gui;
using Project_IC.Framework.Gui.Controls;
using Microsoft.Xna.Framework.Graphics;

namespace Project_IC.Screens {
	class TestScreen : Screen {
		GuiManager gui;

		public TestScreen() {
			
		}

		public override void LoadContent() {
			gui = new GuiManager(new DarkThemeVisuals(ScreenManager));
			//TODO: FIX THIS WITH SCREEN RESOLUTION
			gui.BaseScreen.Bounds = ScreenManager.Game.GraphicsDevice.Viewport.Bounds;
			Label labbel = new Label(0, 100, "asdasdasdasdasdasdadsasdasd");
			labbel.Bounds.Height = labbel.Bounds.Width = 500;
			gui.BaseScreen.AddControls(new Label(0, 0, "Tessssssssssst"), labbel);
			
			base.LoadContent();
		}

		public override void Update(GameTime gameTime, bool hasFocus, bool covered) {
			for (int i = 0; i < 100000; i++) ;

			gui.Update(gameTime);

			base.Update(gameTime, hasFocus, covered);
		}

		public override void UpdateInput(InputManager input) {
			gui.UpdateInput(input);
			
			base.UpdateInput(input);
		}

		public override void Draw(GameTime gameTime) {
			ScreenManager.SpriteBatch.Begin();

			gui.Draw(gameTime, ScreenManager);

			ScreenManager.SpriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
