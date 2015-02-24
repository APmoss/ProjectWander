using System;
using System.Collections.Generic;
using Project_IC.Framework.GSM;
using Microsoft.Xna.Framework;

namespace Project_IC.Screens {
	class TestScreen : Screen {
		public TestScreen() {

		}

		public override void Update(GameTime gameTime, bool hasFocus, bool covered) {
			for (int i = 0; i < 100000; i++) ;

			base.Update(gameTime, hasFocus, covered);
		}

		public override void UpdateInput(InputManager input) {
			
			
			base.UpdateInput(input);
		}
	}
}
