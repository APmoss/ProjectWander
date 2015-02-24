using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project_IC.Framework.GSM;
using Project_IC.Framework.ParticleSystem.ParticleEmitters;

namespace Project_IC.Framework.ParticleSystem {
	class ParticleManager {
		#region Fields
		public Texture2D ParticleSheet;
		int maxParticleCount = 32;

		List<Particle> particles = new List<Particle>();
		List<ParticleEmitter> particleEmitters = new List<ParticleEmitter>();
		#endregion

		#region Properties
		public int ParticleCount {
			get { return particles.Count; }
		}
		public int MaxParticleCount {
			get { return maxParticleCount; }
			set {
				if (value >= 0 && value < int.MaxValue) {
					maxParticleCount = value;

					if (particles.Count > maxParticleCount) {
						particles.RemoveRange(maxParticleCount, particles.Count - maxParticleCount);
					}
				}
			}
		}
		#endregion

		public ParticleManager(Texture2D particleSheet) {
			this.ParticleSheet = particleSheet;
		}

		public void Update(GameTime gameTime) {
			var particlesToUpdate = new Stack<Particle>(particles);
			var particleEmittersToUpdate = new Stack<ParticleEmitter>(particleEmitters);

			while (particlesToUpdate.Count > 0) {
				var particle = particlesToUpdate.Pop();

				particle.Update(gameTime);
			}

			while (particleEmittersToUpdate.Count > 0) {
				var particleEmitter = particleEmittersToUpdate.Pop();

				particleEmitter.Update(gameTime);
			}
		}

		public void Draw(GameTime gameTime, ScreenManager screenManager) {
			foreach (var particle in particles) {
				screenManager.SpriteBatch.Draw(ParticleSheet, particle.Position, particle.SourceRec, particle.Tint, particle.Rotation, Vector2.Zero, particle.Scale, 0, 0);
			}
		}

		public void AddParticles(List<Particle> newParticles) {
			foreach (var particle in newParticles) {
				if (particles.Count + 1 < MaxParticleCount) {
					particle.ParticleManager = this;

					particles.Add(particle);
				}
			}
		}

		public void AddParticleEmitters(List<ParticleEmitter> newParticleEmitters) {
			foreach (var particleEmitter in newParticleEmitters) {
				particleEmitter.ParticleManager = this;

				particleEmitters.Add(particleEmitter);
			}
		}

		public void RemoveParticle(Particle particle) {
			particles.Remove(particle);
		}
	}
}
