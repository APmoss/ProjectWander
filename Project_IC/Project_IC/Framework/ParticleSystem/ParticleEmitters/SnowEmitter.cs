﻿using System;
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

			fogElapsed += Stcs.DilateTime(gameTime.ElapsedGameTime);
			if (fogElapsed > fogTarget) {
				fogElapsed = TimeSpan.Zero;

				particles.Add(new Particle(new Rectangle(16, 0, 32, 32), new SnowModifier()) {
												Position = new Vector2(r.Next(Bounds.Width), -8),
												Velocity = new Vector2(r.Next(4, 50), r.Next(120, 300)),
												Scale = 20f,
												LifeSpan = TimeSpan.FromSeconds(4),
												Tint = Color.White * .2f
											});
			}

			snowElapsed += Stcs.DilateTime(gameTime.ElapsedGameTime);
			if (snowElapsed > snowTarget) {
				snowElapsed = TimeSpan.Zero;
				particles.Add(new Particle(new Rectangle(0, 0, 16, 16), new SnowModifier()) {
												Position = new Vector2(r.Next(Bounds.Width), -8),
												Velocity = new Vector2(r.Next(20, 100), r.Next(180, 420)),
												Scale = (float)r.NextDouble() / 4 + .5f,
												LifeSpan = TimeSpan.FromSeconds(4)
											});
			}
			
			ParticleManager.AddParticles(particles);
			
			base.Update(gameTime);
		}
	}
}
