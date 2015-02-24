using System;
using Microsoft.Xna.Framework;

namespace Project_IC {
	class Stcs {
		public static float TC = 1f;

		/// <summary>
		/// Returns a value dependent on the delta update time
		/// multiplied by a time constant (can be modified).
		/// </summary>
		public static float PPS1(GameTime gameTime) {
			return (float)(gameTime.ElapsedGameTime.TotalSeconds * TC);
		}

		public static TimeSpan DilateTime(TimeSpan timeSpan) {
			return TimeSpan.FromSeconds(timeSpan.TotalSeconds * Math.Abs(TC));
		}
	}
}
