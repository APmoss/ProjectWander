using System;
using System.Collections.Generic;
using Project_IC.Framework.GSM;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Project_IC.Screens {
	class DebugOverlay : Screen {
		#region Fields
		SpriteFont font;

		int frameRate = 0;
		int frameCounter = 0;
		TimeSpan elapsed = TimeSpan.Zero;

		public static StringBuilder DebugText = new StringBuilder();
		public static bool AllowDebug = true;
		public static bool Visible = false;
		#endregion

		public DebugOverlay() {
			IsPopup = true;
		}

		#region Methods
		public override void LoadContent() {
			font = ScreenManager.FontLibrary.GetFont("consolas");
			
			base.LoadContent();
		}

		public override void Update(GameTime gameTime, bool hasFocus, bool covered) {
			elapsed += gameTime.ElapsedGameTime;

			if (elapsed.TotalSeconds > 1) {
				elapsed -= TimeSpan.FromSeconds(1);
				frameRate = frameCounter;
				frameCounter = 0;
			}

			base.Update(gameTime, hasFocus, covered);
		}

		public override void UpdateInput(InputManager input) {
			if (input.IsKeyPressed(Keys.F3)) {
				Visible = !Visible;
			}

			base.UpdateInput(input);
		}

		public override void Draw(GameTime gameTime) {
			frameCounter++;

			if (Visible) {
				StringBuilder output = new StringBuilder();

				output.AppendLine("Debug-");
				output.Append("FPS: ").Append(frameRate).AppendLine();
				output.Append(DebugText).AppendLine();

				ScreenManager.SpriteBatch.Begin();

				ScreenManager.SpriteBatch.DrawString(font, output, Vector2.Zero, Color.DarkGray, 0, Vector2.Zero, .4f, 0, 0);
				ScreenManager.SpriteBatch.DrawString(font, output, Vector2.One, Color.LightGray, 0, Vector2.Zero, .4f, 0, 0);

				ScreenManager.SpriteBatch.End();
			}

			DebugText.Clear();

			base.Draw(gameTime);
		}
		#endregion
	}
}
