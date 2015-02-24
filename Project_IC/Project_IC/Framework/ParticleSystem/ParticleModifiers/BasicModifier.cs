using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Project_IC.Framework.ParticleSystem.ParticleModifiers {
	class BasicModifier : ParticleModifier {
		const float ALPHA_DECAY = .7f;

		public override void Apply(GameTime gameTime, Particle particle) {
			particle.Position += particle.Velocity;

			if (particle.LifeSpan != TimeSpan.Zero && particle.ElapsedLife > particle.LifeSpan) {
				if (particle.Tint.A * ALPHA_DECAY <= 0) {
					particle.ParticleManager.Remove(particle);
				}
				else {
					particle.Tint *= ALPHA_DECAY;
				}
			}

			base.Apply(gameTime, particle);
		}
	}
}
