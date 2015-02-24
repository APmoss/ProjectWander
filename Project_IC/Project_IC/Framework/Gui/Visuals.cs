using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Project_IC.Framework.Gui {
	class Visuals {
		#region Fields
		public SpriteFont Font;
		public Texture2D GuiSheet;

		public Color TextTint = Color.White;
		public int Padding = 5;

		public Rectangle corner = Rectangle.Empty;
		#endregion

		public Visuals(SpriteFont font, Texture2D guiSheet) {
			this.Font = font;
			this.GuiSheet = guiSheet;
		}
	}
}
