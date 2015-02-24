using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Project_IC.Framework.ParticleSystem.ParticleEmitters {
	class ParticleEmitter {
		#region Fields
		protected internal ParticleManager ParticleManager;

		protected TimeSpan ElapsedLife = TimeSpan.Zero;
		#endregion
		public virtual void Update(GameTime gameTime) {
			ElapsedLife += gameTime.ElapsedGameTime;
		}
	}
}
