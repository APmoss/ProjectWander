using System;
using System.Collections.Generic;
using Project_IC.Framework.GSM;
using Microsoft.Xna.Framework;
using Project_IC.Framework.Gui;

namespace Project_IC.Screens {
	class TestScreen : Screen {
		GuiManager gui;

		public TestScreen() {
			
		}

		public override void LoadContent() {
			//gui = new GuiManager(new Visuals())
			
			base.LoadContent();
		}

		public override void Update(GameTime gameTime, bool hasFocus, bool covered) {
			for (int i = 0; i < 100000; i++) ;

			Screens.DebugOverlay.DebugText.Append(Rectangle.Intersect(new Rectangle(0, 0, 50, 50), new Rectangle(45, 45, 50, 50))).AppendLine();

			base.Update(gameTime, hasFocus, covered);
		}

		public override void UpdateInput(InputManager input) {
			
			
			base.UpdateInput(input);
		}
	}
}
