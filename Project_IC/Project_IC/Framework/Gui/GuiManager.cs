﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project_IC.Framework.GSM;
using Project_IC.Framework.Gui.Controls;

namespace Project_IC.Framework.Gui {
	class GuiManager {
		#region Fields
		public Visuals Visuals;

		public Control BaseScreen;

		public bool MouseRecieved;
		#endregion

		public GuiManager(Visuals visuals) {
			this.Visuals = visuals;

			BaseScreen = new Control();
			BaseScreen.GuiManager = this;
			BaseScreen.Visuals = this.Visuals;
			BaseScreen.Initialize();
		}

		public void Update(GameTime gameTime) {
			if (!BaseScreen.Hidden) {
				BaseScreen.Update(gameTime);
			}
		}

		public void UpdateInput(InputManager input) {
			MouseRecieved = false;

			if (!BaseScreen.Hidden) {
				BaseScreen.UpdateInput(input);
			}
		}

		public void Draw(GameTime gameTime, ScreenManager screenManager) {
			if (!BaseScreen.Hidden) {
				BaseScreen.Draw(gameTime, screenManager);
			}
		}
	}
}
