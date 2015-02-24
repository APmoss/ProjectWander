using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace Project_IC.Framework.Audio {
	class Sound {
		public SoundEffectInstance Instance;
		
		public Sound(SoundEffect soundEffect) {
			this.Instance = soundEffect.CreateInstance();
		}
	}
}
