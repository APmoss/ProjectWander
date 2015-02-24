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
			Label labbell = new Label(0, 100, "asdasdasdasdasdasdadsasdasd");
			labbell.Bounds.Height = labbell.Bounds.Width = 100;
			Panel pannell = new Panel(0, 300, 500, 300);
			Button buttonn = new Button(100, 10, 300, "asd");
			pannell.AddControls(buttonn);

			gui.BaseScreen.AddControls(new Label(0, 0, "Tessssssssssst"), labbell, pannell,
										new Button(550, 100, 500, "1111111111111111111111"),
										new Button(550, 150, 500, "2222222222222222222222"),
										new Button(550, 200, 500, "3333333333333333333333"),
										new Button(550, 250, 500, "4444444444444444444444"),
										new Button(550, 300, 500, "5555555555555555555555"),
										new Button(550, 350, 500, "6666666666666666666666"),
										new Button(550, 400, 500, "7777777777777777777777"),
										new Button(550, 450, 500, "8888888888888888888888"),
										new Button(550, 500, 500, "9999999999999999999999"));
			
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
			ScreenManager.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

			gui.Draw(gameTime, ScreenManager);

			ScreenManager.SpriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
