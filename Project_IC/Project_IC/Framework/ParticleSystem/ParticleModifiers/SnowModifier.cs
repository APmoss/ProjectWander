using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Project_IC.Framework.ParticleSystem.ParticleModifiers {
	class SnowModifier : BasicModifier {
		public override void Apply(GameTime gameTime, Particle particle) {
			particle.Velocity.X -= .05f;

			base.Apply(gameTime, particle);
		}
	}
}
