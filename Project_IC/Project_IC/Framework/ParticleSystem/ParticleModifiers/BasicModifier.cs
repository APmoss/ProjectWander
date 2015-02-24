using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Project_IC.Framework.ParticleSystem.ParticleModifiers {
	class BasicModifier : ParticleModifier {
		public override void Apply(GameTime gameTime, Particle particle) {
			particle.Position += particle.Velocity * Stcs.PPS1(gameTime);

			if (particle.LifeSpan != TimeSpan.Zero && particle.ElapsedLife > particle.LifeSpan) {
				if (particle.ElapsedLife > particle.LifeSpan + particle.TransitionOff) {
					particle.ParticleManager.Remove(particle);
				}
				else {
					particle.Alpha = (float)(1 - (particle.ElapsedLife - particle.LifeSpan).TotalSeconds / particle.TransitionOff.TotalSeconds);
				}
			}

			base.Apply(gameTime, particle);
		}
	}
}
