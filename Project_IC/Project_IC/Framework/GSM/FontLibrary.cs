using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace Project_IC.Framework.GSM {
	class FontLibrary {
		#region Fields
		const string FONT_DIR = "fonts";

		Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
		#endregion

		#region Methods
		public void LoadFonts(ContentManager content) {
			if (content != null) {
				var fontsBackup = new Dictionary<string, SpriteFont>(fonts);

				try {
					fonts.Clear();

					string[] fontDirs = Directory.GetFiles(content.RootDirectory + "\\" + FONT_DIR, "*.xnb", SearchOption.AllDirectories);

					foreach (var dir in fontDirs) {
						// Remove the "Content\\" directory and the file extension
						string loadDir = dir.Replace(content.RootDirectory + "\\", string.Empty).Replace(".xnb", string.Empty);

						string key = Path.GetFileNameWithoutExtension(dir).ToLower();
						SpriteFont value = content.Load<SpriteFont>(loadDir);

						fonts.Add(key, value);
					}
				}
				catch (Exception ex) {
					//TODO: Write error
					fonts = new Dictionary<string, SpriteFont>(fontsBackup);
				}
			}
		}

		public SpriteFont GetFont(string fontName) {
			if (fonts.ContainsKey(fontName.ToLower())) {
				return fonts[fontName];
			}
			// Return a default font if not found
			return fonts["segoeui"];
		}
		#endregion
	}
}
