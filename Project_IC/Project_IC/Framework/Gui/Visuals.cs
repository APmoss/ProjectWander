using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Project_IC.Framework.Gui {
	class Visuals {
		#region Fields
		public SpriteFont Font;
		public Texture2D GuiSheet;
		#endregion

		public Visuals(SpriteFont font, Texture2D guiSheet) {
			this.Font = font;
			this.GuiSheet = guiSheet;
		}
	}
}
