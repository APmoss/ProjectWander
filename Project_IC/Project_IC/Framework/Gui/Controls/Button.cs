using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project_IC.Framework.GSM;

namespace Project_IC.Framework.Gui.Controls {
	class Button : Control {
		public string Text = string.Empty;

		public Button(int x, int y, int width, string text) {
			this.Bounds.X = x;
			this.Bounds.Y = y;
			this.Bounds.Width = width;
			this.Text = text;
		}

		public override void Initialize() {
			if (Bounds.Width == 0) {
				Bounds.Width = (int)Visuals.Font.MeasureString(Text).X + Visuals.Padding * 2;
			}
			if (Bounds.Height == 0) {
				Bounds.Height = (int)Visuals.Font.MeasureString(Text).Y + Visuals.Padding * 2;
			}

			base.Initialize();
		}

		public override void Draw(GameTime gameTime, ScreenManager screenManager) {
			if (ContainsMouse) {
				DrawPanel(screenManager.SpriteBatch, Visuals.ControlSrcRecs["cornerFocus"], Visuals.ControlSrcRecs["sideFocus"], Visuals.ControlSrcRecs["fillFocus"]);
			}
			else {
				DrawPanel(screenManager.SpriteBatch, Visuals.ControlSrcRecs["corner"], Visuals.ControlSrcRecs["side"], Visuals.ControlSrcRecs["fill"]);
			}

			var centerDisplacement = new Vector2(Bounds.Width / 2, Bounds.Height / 2) - Visuals.Font.MeasureString(Text) / 2;
			screenManager.SpriteBatch.DrawString(Visuals.Font, Text, GlobalPosition + centerDisplacement, Visuals.TextTint);
			
			base.Draw(gameTime, screenManager);
		}
	}
}
