using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project_IC.Framework.GSM;
using Microsoft.Xna.Framework.Graphics;

namespace Project_IC.Framework.Gui.Controls {
	class Label : Control {
		public string Text = string.Empty;

		public Label(int x, int y, string text) {
			this.Text = text;
			this.Bounds.X = x;
			this.Bounds.Y = y;
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
			screenManager.SpriteBatch.Draw(screenManager.Blank, GlobalIntersection, new Color(10, 50, 10, 100));

			DrawPanel(screenManager.SpriteBatch);
			
			screenManager.SpriteBatch.DrawString(Visuals.Font, Text, GlobalPosition + new Vector2(Visuals.Padding), Visuals.TextTint);

			base.Draw(gameTime, screenManager);
		}
	}
}
