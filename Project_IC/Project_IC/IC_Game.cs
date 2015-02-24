using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Project_IC {
	public class IC_Game : Game {
		GraphicsDeviceManager graphics;

		public IC_Game() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";


		}

		#region Methods
		protected override void Initialize() {


			base.Initialize();
		}

		protected override void LoadContent() {
			
		}

		protected override void Update(GameTime gameTime) {
			

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);

			

			base.Draw(gameTime);
		}
		#endregion
	}
}
