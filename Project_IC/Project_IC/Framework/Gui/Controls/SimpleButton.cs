using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project_IC.Framework.GSM;

namespace Project_IC.Framework.Gui.Controls {
	class SimpleButton : Control {
		#region Fields
		public Rectangle NormalSrcRec = Rectangle.Empty;
		public Rectangle FocusSrcRec = Rectangle.Empty;
		#endregion

		public SimpleButton(int x, int y, Rectangle normalSrcRec, Rectangle focusSrcRec) {
			this.Bounds = new Rectangle(x, y, normalSrcRec.Width, normalSrcRec.Height);
			this.NormalSrcRec = normalSrcRec;
			this.FocusSrcRec = focusSrcRec;
		}

		public override void Draw(GameTime gameTime, ScreenManager screenManager) {
			if (ContainsMouse) {
				screenManager.SpriteBatch.Draw(Visuals.GuiSheet, Bounds, FocusSrcRec, PrimaryTint);
			}
			else {
				screenManager.SpriteBatch.Draw(Visuals.GuiSheet, Bounds, NormalSrcRec, PrimaryTint);
			}

			base.Draw(gameTime, screenManager);
		}
	}
}
