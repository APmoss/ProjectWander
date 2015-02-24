using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using Microsoft.Xna.Framework;
using Project_IC.Framework.GSM;
using Microsoft.Xna.Framework.Graphics;

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
						var l = new TmxLayer(subElement);
						Layers.Add(l.Name, l);
						break;
				}
			}
		}

		public void Draw(ScreenManager screenManager) {
			foreach (var layer in Layers) {
				layer.Value.Draw(screenManager, Tilesets.Values.ToList());
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

		public Texture2D Texture;
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
		#region Fields
		public string Name = string.Empty;
		public float Opacity = 1;
		public bool Hidden = false;

		public List<short> Tiles = new List<short>();
		#endregion

		public TmxLayer(XElement layerElement) {
			foreach (var attribute in layerElement.Attributes()) {
				switch (attribute.Name.LocalName.ToLower()) {
					case "name":
						Name = attribute.Value;
						break;
					case "opacity":
						Opacity = float.Parse(attribute.Value);
						break;
					case "visible":
						Hidden = attribute.Value == "0";
						break;
				}
			}

			if (layerElement.Element("data") != null) {
				var data = layerElement.Element("data");

				foreach (var subElement in data.Elements()) {
					if (subElement.Name.LocalName.ToLower() == "tile") {
						Tiles.Add(short.Parse(subElement.Attribute("gid").Value));
					}
				}
			}
		}

		public void Draw(ScreenManager screenManager, List<TmxTileset> tilesets) {
			
		}
	}
}
