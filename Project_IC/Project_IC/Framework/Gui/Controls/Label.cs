﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project_IC.Framework.GSM;
using Microsoft.Xna.Framework.Graphics;

namespace Project_IC.Framework.Gui.Controls {
	class Label : Control {
		public string Text = string.Empty;
		public float Scale = 1;

		public Label(int x, int y, string text, float scale) {
			this.Text = text;
			this.Bounds.X = x;
			this.Bounds.Y = y;
			this.Scale = scale;
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
			var centerDisplacement = new Vector2(Bounds.Width / 2, Bounds.Height / 2) - Visuals.Font.MeasureString(Text) / 2;

			screenManager.SpriteBatch.DrawString(Visuals.Font, Text, GlobalPosition + centerDisplacement, PrimaryTint, 0, Vector2.Zero, Scale, 0, 0);

			base.Draw(gameTime, screenManager);
		}
	}
}
