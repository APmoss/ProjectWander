using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project_IC.Framework.GSM;

namespace Project_IC.Framework.Gui.Controls {
	class Image : Control {
		#region Fields
		public Texture2D Texture;
		#endregion

		public Image(Texture2D texture, int x, int y)
			: this(texture, x, y, texture.Width, texture.Height) { }
		public Image(Texture2D texture, int x, int y, int width, int height) {
			this.Texture = texture;
			this.Bounds.X = x;
			this.Bounds.Y = y;
			this.Bounds.Width = width;
			this.Bounds.Height = height;
		}

		public override void Draw(GameTime gameTime, ScreenManager screenManager) {
			screenManager.SpriteBatch.Draw(Texture, Bounds, PrimaryTint);

			base.Draw(gameTime, screenManager);
		}
	}
}
