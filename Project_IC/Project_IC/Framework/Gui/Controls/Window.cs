using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project_IC.Framework.GSM;
using Microsoft.Xna.Framework.Graphics;

namespace Project_IC.Framework.Gui.Controls {
	class Window : Panel {
		#region Fields
		public bool Moveable = true;
		public string Title = string.Empty;
		#endregion

		public Window(int x, int y, int width, int height, string title, bool moveable)
			: base(x, y, width, height) {

			this.Title = title;
			this.Moveable = moveable;
		}

		public override void Draw(GameTime gameTime, ScreenManager screenManager) {			
			base.Draw(gameTime, screenManager);
			
			var underlineCorner = Visuals.ControlSrcRecs["underlineCorner"];
			var underline = Visuals.ControlSrcRecs["underline"];

			// Underline
			// Left
			var leftUnderline = new Vector2(GlobalPosition.X, GlobalPosition.Y + 14);
			screenManager.SpriteBatch.Draw(Visuals.GuiSheet, leftUnderline, underlineCorner, PrimaryTint);
			// Right
			var rightUnderline = new Vector2(GlobalPosition.X + Bounds.Width - underlineCorner.Width, GlobalPosition.Y + 14);
			screenManager.SpriteBatch.Draw(Visuals.GuiSheet, rightUnderline, underlineCorner, PrimaryTint, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
			// Center
			var centerUnderline = new Rectangle((int)leftUnderline.X + underlineCorner.Width, (int)leftUnderline.Y, (int)rightUnderline.X - (int)leftUnderline.X - underlineCorner.Width, underlineCorner.Height);
			screenManager.SpriteBatch.Draw(Visuals.GuiSheet, centerUnderline, underline, PrimaryTint);

			// Title
			var centerDisplacement = new Vector2(Bounds.Width / 2 - Visuals.Font.MeasureString(Title).X / 2, 3);
			screenManager.SpriteBatch.DrawString(Visuals.Font, Title, GlobalPosition + centerDisplacement, Visuals.TextTint);
		}
	}
}
