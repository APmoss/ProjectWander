using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Project_IC.Framework.GSM;
using Microsoft.Xna.Framework;
using Project_IC.Framework.Gameplay;

namespace Project_IC.Framework.Lighting {
	struct Triangle {
		public Vector2 P1;
		public Vector2 P2;
		public Vector2 P3;

		public Triangle(Vector2 p1, Vector2 p2, Vector2 p3) {
			this.P1 = p1;
			this.P2 = p2;
			this.P3 = p3;
		}

		public static Triangle Zero {
			get { return new Triangle(Vector2.Zero, Vector2.Zero, Vector2.Zero); }
		}
	}

	class LightManager {
		#region Fields
		public Color AmbientLight = Color.Black * .5f;

		List<RadialLight> RadialLights = new List<RadialLight>();

		GraphicsDevice graphicsDevice;
		RenderTarget2D shadowTarget;
		VertexBuffer vertexBuffer;
		BasicEffect basicEffect;
		Camera camera;
		#endregion
		public LightManager(GraphicsDevice graphicsDevice, Camera camera) {
			this.graphicsDevice = graphicsDevice;
			shadowTarget = new RenderTarget2D(graphicsDevice, camera.ViewportWidth, camera.ViewportHeight);
			vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), 0, BufferUsage.WriteOnly);
			basicEffect = new BasicEffect(graphicsDevice);
			basicEffect.World = Matrix.Identity;
			basicEffect.View = Matrix.CreateLookAt(Vector3.Backward, Vector3.Forward, Vector3.Up);
			basicEffect.Projection = Matrix.CreateOrthographic(camera.ViewportWidth, camera.ViewportHeight, -1, 1);

			this.camera = camera;
		}

		public void DrawShadowMask(ScreenManager screenManager) {
			screenManager.GraphicsDevice.SetRenderTarget(shadowTarget);
			screenManager.GraphicsDevice.Clear(Color.Black);
			
			var shadowSpace = new Rectangle();
			screenManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transformation);
			//screenManager.SpriteBatch.Draw(screenManager.Blank, shadowSpace, null, )

			screenManager.SpriteBatch.Draw(shadowTarget, Vector2.Zero, Color.White);
			screenManager.SpriteBatch.End();
		}

		public void ResetShadowTarget() {
			graphicsDevice.SetRenderTarget(shadowTarget);
			graphicsDevice.Clear(Color.Black);
			graphicsDevice.SetRenderTarget(null);
		}

		public IEnumerable<Triangle> EnumerateLightMask() {
			yield return new Triangle(Vector2.Zero, Vector2.Zero, Vector2.Zero);
		}
	}
}
