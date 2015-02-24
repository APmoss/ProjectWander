using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Project_IC.Framework.GSM;
using Microsoft.Xna.Framework;

namespace Project_IC.Framework.Gui {
	class DarkThemeVisuals : Visuals {
		public DarkThemeVisuals(ScreenManager screenManager) {
			this.Font = screenManager.FontLibrary.GetFont("segoeUI");
			this.GuiSheet = screenManager.Game.Content.Load<Texture2D>("textures/darkThemeGuiSheet");

			ControlSrcRecs.Clear();
			ControlSrcRecs.Add("corner", new Rectangle(0, 0, 16, 16));
			ControlSrcRecs.Add("side", new Rectangle(0, 16, 16, 16));
			ControlSrcRecs.Add("fill", new Rectangle(16, 16, 16, 16));

			ControlSrcRecs.Add("cornerFocus", new Rectangle(0, 48, 16, 16));
			ControlSrcRecs.Add("sideFocus", new Rectangle(0, 64, 16, 16));
			ControlSrcRecs.Add("fillFocus", new Rectangle(16, 64, 16, 16));

			ControlSrcRecs.Add("underlineCorner", new Rectangle(16, 0, 16, 16));
			ControlSrcRecs.Add("underline", new Rectangle(32, 0, 16, 16));
		}
	}
}
