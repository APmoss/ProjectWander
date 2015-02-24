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
			ControlSrcRecs.Add("buttonCorner", new Rectangle(0, 0, 16, 16));
			ControlSrcRecs.Add("buttonSide", new Rectangle(0, 16, 16, 16));
			ControlSrcRecs.Add("buttonFill", new Rectangle(16, 16, 16, 16));

			ControlSrcRecs.Add("buttonCornerFocus", new Rectangle(0, 48, 16, 16));
			ControlSrcRecs.Add("buttonSideFocus", new Rectangle(0, 64, 16, 16));
			ControlSrcRecs.Add("buttonFillFocus", new Rectangle(16, 64, 16, 16));

			ControlSrcRecs.Add("panelCorner", new Rectangle(48, 0, 16, 16));
			ControlSrcRecs.Add("panelSide", new Rectangle(48, 16, 16, 16));
			ControlSrcRecs.Add("panelFill", new Rectangle(64, 16, 16, 16));

			ControlSrcRecs.Add("underlineCorner", new Rectangle(64, 0, 16, 16));
			ControlSrcRecs.Add("underline", new Rectangle(80, 0, 16, 16));

			ControlSrcRecs.Add("windowClose", new Rectangle(48, 48, 16, 16));
			ControlSrcRecs.Add("windowMinimize", new Rectangle(64, 48, 16, 16));
			ControlSrcRecs.Add("windowCloseFocus", new Rectangle(48, 64, 16, 16));
			ControlSrcRecs.Add("windowMinimizeFocus", new Rectangle(64, 64, 16, 16));
		}
	}
}
