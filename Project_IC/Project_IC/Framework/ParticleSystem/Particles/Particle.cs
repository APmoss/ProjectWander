﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project_IC.Framework.GSM;

namespace Project_IC.Framework.ParticleSystem.Particles {
	class Particle {
		#region Fields
		protected internal ParticleManager ParticleManager;

		protected TimeSpan elapsedLife = TimeSpan.Zero;

		public Vector2 Position = Vector2.Zero;
		public Vector2 Velocity = Vector2.Zero;
		public float Rotation = 0f;
		public Rectangle SourceRec = Rectangle.Empty;
		public float Scale = 1f;
		public Color Tint = Color.White;
		#endregion

		#region Properties
		public TimeSpan ElapsedLife {
			get { return elapsedLife; }
		}
		#endregion

		public virtual void Update(GameTime gameTime) {
			elapsedLife += gameTime.ElapsedGameTime;
		}

		public virtual void Draw(GameTime gameTime, ScreenManager screenManager) {
			screenManager.SpriteBatch.Draw(ParticleManager.ParticleSheet, Position, SourceRec, Tint, Rotation, Vector2.Zero, Scale, 0, 0);
		}
	}
}