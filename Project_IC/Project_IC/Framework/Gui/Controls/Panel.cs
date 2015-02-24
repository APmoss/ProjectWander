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
			DrawPanel(screenManager.SpriteBatch, Visuals.ControlSrcRecs["corner"], Visuals.ControlSrcRecs["side"], Visuals.ControlSrcRecs["fill"]);
			
			base.Draw(gameTime, screenManager);
		}
	}
}
