using System;
using System.Collections.Generic;
using Project_IC.Screens.Menus;
using Project_IC.Framework.ParticleSystem;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project_IC.Framework.Gui.Controls;
using Project_IC.Framework.ParticleSystem.ParticleEmitters;

namespace Project_IC.Screens {
	class ParticleTestScreen : MenuScreen {
		#region Fields
		ParticleManager particleManager;

		Texture2D background;
		#endregion

		public override void LoadContent() {
			particleManager = new ParticleManager(ScreenManager.Game.Content.Load<Texture2D>("textures/particleSheet"));
			particleManager.MaxParticleCount = 4096;

			background = ScreenManager.Game.Content.Load<Texture2D>("mapOutside");
			
			base.LoadContent();
		}

		public override void Update(GameTime gameTime, bool hasFocus, bool covered) {
			particleManager.Update(gameTime);

			base.Update(gameTime, hasFocus, covered);
		}

		public override void Draw(GameTime gameTime) {
			ScreenManager.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

			ScreenManager.SpriteBatch.Draw(background, Vector2.Zero, null, Color.Gray, 0, Vector2.Zero, 1.5f, 0, 0);

			particleManager.Draw(gameTime, ScreenManager);

			ScreenManager.SpriteBatch.End();
			
			base.Draw(gameTime);
		}

		#region SetGui
		Button SnowButton;
		Button RainButton;
		Button ClearButton;
		Button BackButton;

		protected override void SetGui() {
			SnowButton = new Button(ScreenManager.Res.X - 160, 10, 150, "Start Snow");
			SnowButton.LeftClicked += (s, e) => particleManager.AddParticleEmitters(new List<ParticleEmitter>() { new SnowEmitter(new Rectangle(0, 0, 1700, 768)) });

			RainButton = new Button(ScreenManager.Res.X - 160, 70, 150, "Start Rain");
			RainButton.LeftClicked += (s, e) => particleManager.AddParticleEmitters(new List<ParticleEmitter>() { new RainEmitter(new Rectangle(0, 0, 1700, 768)) });

			ClearButton = new Button(ScreenManager.Res.X - 160, 130, 150, "Clear Emitter");
			ClearButton.LeftClicked += (s, e) => particleManager.ClearParticleEmitters();

			BackButton = new Button(10, ScreenManager.Res.Y - 60, 100, "Back");
			BackButton.LeftClicked += (s, e) => { ExitScreen(); };

			Gui.BaseScreen.AddControls(SnowButton, RainButton, ClearButton, BackButton);
			
			base.SetGui();
		}
		#endregion
	}
}
