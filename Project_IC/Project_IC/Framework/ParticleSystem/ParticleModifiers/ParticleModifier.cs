using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Project_IC.Framework.ParticleSystem.ParticleModifiers {
	class ParticleModifier {
		#region Fields
		protected Particle Particle;
		#endregion

		public virtual void ModifyParticle(GameTime gameTime) {
			
		}
	}
}
