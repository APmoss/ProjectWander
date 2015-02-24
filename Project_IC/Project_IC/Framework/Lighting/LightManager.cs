using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Project_IC.Framework.GSM;

namespace Project_IC.Framework.Lighting {
	class LightManager {
		#region Fields
		List<RadialLight> RadialLights = new List<RadialLight>();

		RenderTarget2D shadowTarget;
		#endregion
		public LightManager(GraphicsDevice graphicsDevice, int targetWidth, int targetHeight) {
			shadowTarget = new RenderTarget2D(graphicsDevice, targetWidth, targetHeight);
		}

		public void DrawShadowMask(ScreenManager screenManager) {
			
		}
	}
}
