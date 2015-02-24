using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Project_IC.Framework.ParticleSystem.ParticleEmitters {
	class ParticleEmitter {
		#region Fields
		protected internal ParticleManager ParticleManager;

		protected TimeSpan elapsedLife = TimeSpan.Zero;
		public TimeSpan LifeSpan = TimeSpan.FromSeconds(5);
		#endregion

		#region Properties
		public TimeSpan ElapsedLife {
			get { return elapsedLife; }
		}
		#endregion

		public virtual void Update(GameTime gameTime) {
			elapsedLife += gameTime.ElapsedGameTime;

			if (LifeSpan != TimeSpan.Zero && elapsedLife > LifeSpan) {
				ParticleManager.Remove(this);
			}
		}
	}
}
