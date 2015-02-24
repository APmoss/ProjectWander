using System;
using Microsoft.Xna.Framework;

namespace Project_IC.Framework.Lighting {
	class RadialLight {
		#region Fields
		public Vector2 Position = Vector2.Zero;
		public float Radius = 0;
		public Color Tint = Color.White;
		#endregion

		public RadialLight(float x, float y, float radius, Color tint) {
			this.Position.X = x;
			this.Position.Y = y;
			this.Radius = radius;
			this.Tint = tint;
		}
	}
}
