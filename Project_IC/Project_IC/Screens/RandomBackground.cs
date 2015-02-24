using System;
using System.Collections.Generic;
using Project_IC.Framework.GSM;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_IC.Screens {
	class RandomBackground : Screen {
		float squareSize = 16;
		int loops = 100;
		Vector2 squareDisp = Vector2.Zero;

		Vector2 mousePos = Vector2.Zero;
		Texture2D light;

		public override void Update(GameTime gameTime, bool hasFocus, bool covered) {
			squareSize = (float)(Math.Sin(gameTime.TotalGameTime.TotalSeconds / 4) + 1) * 16 + 16;
			squareDisp.X = (int)(Math.Cos(gameTime.TotalGameTime.TotalSeconds * 2) * squareSize);
			squareDisp.Y = (int)(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 2) * squareSize);

			light = ScreenManager.Game.Content.Load<Texture2D>("vignette");

			InputFallThrough = true;
			
			base.Update(gameTime, hasFocus, false);
		}

		public override void UpdateInput(InputManager input) {
			mousePos.X = input.MouseState.X;
			mousePos.Y = input.MouseState.Y;

			base.UpdateInput(input);
		}

		public override void Draw(GameTime gameTime) {
			ScreenManager.SpriteBatch.Begin();

			for (int i = -2; i < loops; i++) {
				for (int j = -2; j < loops; j++) {
					if (Math.Abs(i % 2) == Math.Abs(j % 2)) {
						ScreenManager.SpriteBatch.Draw(ScreenManager.Blank, new Vector2(squareSize * i + squareDisp.X, squareSize * j + squareDisp.Y), null,
							//Color.DarkSalmon, 0, Vector2.Zero, squareSize, 0, 0);
							Color.Black, 0, Vector2.Zero, squareSize, 0, 0);
					}
					else {
						ScreenManager.SpriteBatch.Draw(ScreenManager.Blank, new Vector2(squareSize * i + squareDisp.X, squareSize * j + squareDisp.Y), null,
							//Color.LightSalmon, 0, Vector2.Zero, squareSize, 0, 0);
							Color.DarkBlue, 0, Vector2.Zero, squareSize, 0, 0);
					}
				}
			}

			ScreenManager.SpriteBatch.Draw(light, mousePos, null, Color.LightGray * .75f, 0, new Vector2(light.Width / 2, light.Height / 2), 5, 0, 0);

			ScreenManager.SpriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
