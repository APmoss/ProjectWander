using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project_IC.Framework.GSM;
using Project_IC.Framework.ParticleSystem.ParticleModifiers;

namespace Project_IC.Framework.ParticleSystem {
	class Particle {
		#region Fields
		protected internal ParticleManager ParticleManager;

		protected TimeSpan elapsedLife = TimeSpan.Zero;
		public TimeSpan LifeSpan = TimeSpan.FromSeconds(5);

		public Vector2 Position = Vector2.Zero;
		public Vector2 Velocity = Vector2.Zero;
		public float Rotation = 0f;
		public Rectangle SourceRec = Rectangle.Empty;
		public float Scale = 1f;
		public Color Tint = Color.White;

		public List<ParticleModifier> Modifiers = new List<ParticleModifier>();
		#endregion

		#region Properties
		public TimeSpan ElapsedLife {
			get { return elapsedLife; }
		}
		#endregion

		public Particle() {
		}
		public Particle(Rectangle sourceRec) {
			this.SourceRec = sourceRec;
		}
		public Particle(Rectangle sourceRec, params ParticleModifier[] modifiers)
			: this(sourceRec) {

			this.Modifiers.AddRange(modifiers);
		}

		public virtual void Update(GameTime gameTime) {
			elapsedLife += gameTime.ElapsedGameTime;

			foreach (var modifier in Modifiers) {
				modifier.Apply(gameTime, this);
			}
		}
	}
}
