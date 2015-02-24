using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project_IC.Framework.GSM;
using Project_IC.Framework.ParticleSystem.Particles;
using Project_IC.Framework.ParticleSystem.ParticleEmitters;

namespace Project_IC.Framework.ParticleSystem {
	class ParticleManager {
		#region Fields
		public Texture2D ParticleSheet;

		List<Particle> particles = new List<Particle>();
		List<ParticleEmitter> particleEmitters = new List<ParticleEmitter>();
		#endregion

		public ParticleManager(Texture2D particleSheet) {
			this.ParticleSheet = particleSheet;
		}

		public void Update(GameTime gameTime) {
			var particlesToUpdate = new List<Particle>();
			var particleEmittersToUpdate = new List<ParticleEmitter>();
			
			particlesToUpdate.AddRange(particles);
			particleEmittersToUpdate.AddRange(particleEmitters);

			while (particlesToUpdate.Count > 0) {
				var particle = particlesToUpdate[0];
				particlesToUpdate.RemoveAt(0);

				particle.Update(gameTime);
			}

			while (particleEmittersToUpdate.Count > 0) {
				var particleEmitter = particleEmittersToUpdate[0];
				particleEmittersToUpdate.RemoveAt(0);

				particleEmitter.Update(gameTime);
			}
		}

		public void Draw(GameTime gameTime, ScreenManager screenManager) {
			foreach (var particle in particles) {
				particle.Draw(gameTime, screenManager);
			}
		}

		public void AddParticles(List<Particle> particles) {
			foreach (var particle in particles) {
				//if(this.particles.ca)
			}
		}
	}
}
