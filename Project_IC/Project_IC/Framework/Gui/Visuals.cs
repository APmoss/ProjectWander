using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Project_IC.Framework.Gui {
	class Visuals {
		#region Fields
		public SpriteFont Font;
		public Texture2D GuiSheet;

		public Dictionary<string, Rectangle> ControlSrcRecs = new Dictionary<string,Rectangle>();

		public Color TextTint = Color.White;
		public int Padding = 5;
		#endregion

		protected Visuals() { }
		public Visuals(SpriteFont font, Texture2D guiSheet) {
			this.Font = font;
			this.GuiSheet = guiSheet;
		}
	}
}
