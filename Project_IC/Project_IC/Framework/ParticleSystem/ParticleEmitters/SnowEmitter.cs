using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project_IC.Framework.ParticleSystem.ParticleModifiers;

namespace Project_IC.Framework.ParticleSystem.ParticleEmitters {
	class SnowEmitter : ParticleEmitter {
		#region Fields
		public Rectangle Bounds = Rectangle.Empty;

		TimeSpan fogTarget = TimeSpan.FromMilliseconds(100);
		TimeSpan fogElapsed = TimeSpan.Zero;
		TimeSpan snowTarget = TimeSpan.FromMilliseconds(50);
		TimeSpan snowElapsed = TimeSpan.Zero;
		#endregion

		public SnowEmitter(Rectangle bounds) {
			LifeSpan = TimeSpan.Zero;
			this.Bounds = bounds;
		}

		public override void Update(GameTime gameTime) {
			Random r = ParticleManager.R;
			var particles = new List<Particle>();

			fogElapsed += gameTime.ElapsedGameTime;
			if (fogElapsed > fogTarget) {
				fogElapsed -= fogTarget;

				particles.Add(new Particle(new Rectangle(16, 0, 32, 32), new SnowModifier()) {
												Position = new Vector2(r.Next(Bounds.Width), -8),
												Velocity = new Vector2((float)r.NextDouble() / 3 + .1f, r.Next(2, 5)),
												Scale = 20f,
												LifeSpan = TimeSpan.FromSeconds(4),
												Tint = Color.White * .2f
											});
			}

			snowElapsed += gameTime.ElapsedGameTime;
			if (snowElapsed > snowTarget) {
				snowElapsed -= snowTarget;

				particles.Add(new Particle(new Rectangle(0, 0, 16, 16), new SnowModifier()) {
												Position = new Vector2(r.Next(Bounds.Width), -8),
												Velocity = new Vector2((float)r.NextDouble() / 3 + .3f, r.Next(3, 7)),
												Scale = (float)r.NextDouble() / 4 + .5f,
												LifeSpan = TimeSpan.FromSeconds(4)
											});
			}
			
			ParticleManager.AddParticles(particles);
			
			base.Update(gameTime);
		}
	}
}
