using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace Project_IC.Framework.Gameplay {
	class TmxMap {
		public enum MapOrientation {
			Orthogonal, Isometric, Staggered
		}

		#region Fields
		public string Version = string.Empty;
		public MapOrientation Orientation;
		public int Width = 0;
		public int Height = 0;
		public int TileWidth = 0;
		public int TileHeight = 0;
		public Color BackgroundColor = Color.White;

		public Dictionary<string, TmxTileset> Tilesets = new Dictionary<string, TmxTileset>();
		public Dictionary<string, TmxLayer> Layers = new Dictionary<string, TmxLayer>();
		#endregion

		public TmxMap(XElement mapElement) {
			foreach (var attribute in mapElement.Attributes()) {
				switch (attribute.Name.LocalName.ToLower()) {
					case "version":
						Version = attribute.Value;
						break;
					case "orientation":
						Orientation = (MapOrientation)Enum.Parse(typeof(MapOrientation), attribute.Value, true);
						break;
					case "width":
						Width = int.Parse(attribute.Value);
						break;
					case "height":
						Height = int.Parse(attribute.Value);
						break;
					case "tilewidth":
						TileWidth = int.Parse(attribute.Value);
						break;
					case "tileheight":
						TileHeight = int.Parse(attribute.Value);
						break;
					case "backgroundcolor":
						string hex = attribute.Value.TrimStart('#');
						BackgroundColor = new Color(Convert.ToByte(hex.Substring(0, 2), 16),
															Convert.ToByte(hex.Substring(2, 2), 16),
															Convert.ToByte(hex.Substring(4, 2), 16));
						break;
				}
			}
			foreach (var subElement in mapElement.Elements()) {
				switch (subElement.Name.LocalName.ToLower()) {
					case "tileset":
						var ts = new TmxTileset(subElement);
						Tilesets.Add(ts.Name, ts);
						break;
					case "layer":

						break;
				}
			}
		}

		public static TmxMap Load(string path) {
			try {
				TmxMap tmxMap;
				XDocument doc = XDocument.Load(path);

				tmxMap = new TmxMap(doc.Element("map"));

				return tmxMap;
			}
			catch (Exception ex) {
				//TODO: write error
				return new TmxMap(new XElement("asd"));
			}
		}
	}

	class TmxTileset {
		#region Fields
		public string Name = string.Empty;
		public int FirstGid = 1;
		public int TileWidth = 0;
		public int TileHeight = 0;
		public int Spacing = 0;
		public int Margin = 0;

		public string ImageSrc = string.Empty;
		public int ImageWidth = 0;
		public int ImageHeight = 0;
		#endregion

		public TmxTileset(XElement tilesetElement) {
			foreach (var attribute in tilesetElement.Attributes()) {
				switch (attribute.Name.LocalName.ToLower()) {
					case "firstgid":
						FirstGid = int.Parse(attribute.Value);
						break;
					case "name":
						Name = attribute.Value;
						break;
					case "tilewidth":
						TileWidth = int.Parse(attribute.Value);
						break;
					case "tileheight":
						TileHeight = int.Parse(attribute.Value);
						break;
					case "spacing":
						Spacing = int.Parse(attribute.Value);
						break;
					case "margin":
						Margin = int.Parse(attribute.Value);
						break;
				}
			}

			foreach (var subElement in tilesetElement.Elements()) {
				if (subElement.Name.LocalName.ToLower() == "image") {
					ImageSrc = subElement.Attribute("source").Value ?? ImageSrc;
					ImageWidth = int.Parse(subElement.Attribute("width").Value ?? "0");
					ImageHeight = int.Parse(subElement.Attribute("height").Value ?? "0");
				}
			}
		}
	}

	class TmxLayer {

	}
}
