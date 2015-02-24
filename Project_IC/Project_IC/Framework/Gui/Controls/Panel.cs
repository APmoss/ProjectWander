using System;
using System.Collections.Generic;
using Project_IC.Framework.GSM;
using Microsoft.Xna.Framework;

namespace Project_IC.Framework.Gui.Controls {
	class Panel : Control {
		public Panel(int x, int y, int width, int height) {
			this.Bounds.X = x;
			this.Bounds.Y = y;
			this.Bounds.Width = width;
			this.Bounds.Height = height;
		}

		public override void Draw(GameTime gameTime, ScreenManager screenManager) {
			DrawSurround(screenManager.SpriteBatch, Visuals.ControlSrcRecs["panelCorner"], Visuals.ControlSrcRecs["panelSide"], Visuals.ControlSrcRecs["panelFill"]);
			
			base.Draw(gameTime, screenManager);
		}
	}
}
