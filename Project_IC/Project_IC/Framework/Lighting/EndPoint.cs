using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Project_IC.Framework.Lighting {
	class EndPoint {
		#region Fields
		Vector2 Position = Vector2.Zero;
		#endregion

		public EndPoint() { }
		public EndPoint(float x, float y) {
			this.Position = new Vector2(x, y);
		}
	}
}
