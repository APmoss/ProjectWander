using System;
using System.Collections.Generic;
using Project_IC.Framework.GSM;
using Project_IC.Framework.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_IC.Screens.Menus {
	class MenuScreen : Screen {
		protected GuiManager Gui;

		public override void LoadContent() {
			Gui = new GuiManager(new DarkThemeVisuals(ScreenManager));
			Gui.BaseScreen.Bounds = new Rectangle(0, 0, ScreenManager.Res.X, ScreenManager.Res.Y);
			SetGui();
			
			base.LoadContent();
		}

		public override void Update(GameTime gameTime, bool hasFocus, bool covered) {
			Gui.Update(gameTime);

			base.Update(gameTime, hasFocus, covered);
		}

		public override void UpdateInput(InputManager input) {
			Gui.UpdateInput(input);

			base.UpdateInput(input);
		}

		public override void Draw(GameTime gameTime) {
			ScreenManager.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
			Gui.Draw(gameTime, ScreenManager);
			ScreenManager.SpriteBatch.End();

			base.Draw(gameTime);
		}

		protected virtual void SetGui() {

		}
	}
}
