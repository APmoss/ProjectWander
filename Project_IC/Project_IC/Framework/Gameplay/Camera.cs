using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Project_IC.Framework.Gameplay {
	class Camera {
		#region Fields
		public int ViewportWidth = 0;
		public int ViewportHeight = 0;

		public Vector2 DestPosition = Vector2.Zero;
		public Vector2 TransPosition = Vector2.Zero;

		public float DestScale = 1f;
		public float TransScale = 1f;

		public float DestRotation = 0f;
		public float TransRotation = 0f;

		public float TransitionStrength = .1f;
		#endregion

		#region Properties
		public Matrix Transformation {
			get; protected set;
		}
		#endregion

		public Camera(int viewportWidth, int viewportHeight) {
			this.ViewportWidth = viewportWidth;
			this.ViewportHeight = viewportHeight;

			Transformation = Matrix.Identity;
		}

		public void Update() {
			TransPosition = Vector2.Lerp(TransPosition, DestPosition, TransitionStrength);

			TransScale = MathHelper.Lerp(TransScale, DestScale, TransitionStrength);

			TransRotation = MathHelper.Lerp(TransRotation, DestRotation, TransitionStrength);

			Transformation = Matrix.CreateTranslation(-TransPosition.X, -TransPosition.Y, 0) *
								Matrix.CreateScale(TransScale) *
								Matrix.CreateRotationZ(TransRotation) *
								Matrix.CreateTranslation(ViewportWidth / 2, ViewportHeight / 2, 0);
		}

		public Vector2 ToWorldPosition(Vector2 screenPosition) {
			return Vector2.Transform(screenPosition, Matrix.Invert(Transformation));
		}

		public Vector2 ToScreenPosition(Vector2 worldPosition) {
			return Vector2.Transform(worldPosition, Transformation);
		}
	}
}
