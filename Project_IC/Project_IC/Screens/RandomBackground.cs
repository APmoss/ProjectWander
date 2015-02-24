﻿using System;
using System.Collections.Generic;
using Project_IC.Framework.GSM;
using Microsoft.Xna.Framework;

namespace Project_IC.Screens {
	class RandomBackground : Screen {
		float squareSize = 16;
		Vector2 squareDisp = Vector2.Zero;

		public override void Update(GameTime gameTime, bool hasFocus, bool covered) {
			squareSize = (float)(Math.Sin(gameTime.TotalGameTime.TotalSeconds / 4) + 1) * 16 + 16;
			squareDisp.X = (int)(Math.Cos(gameTime.TotalGameTime.TotalSeconds * 2) * squareSize);
			squareDisp.Y = (int)(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 2) * squareSize);
			
			base.Update(gameTime, hasFocus, covered);
		}

		public override void Draw(GameTime gameTime) {
			int loops = 100;
			
			ScreenManager.SpriteBatch.Begin();

			for (int i = -2; i < loops; i++) {
				for (int j = -2; j < loops; j++) {
					if (Math.Abs(i % 2) == Math.Abs(j % 2)) {
						ScreenManager.SpriteBatch.Draw(ScreenManager.Blank, new Vector2(squareSize * i + squareDisp.X, squareSize * j + squareDisp.Y), null, Color.Cyan, 0, Vector2.Zero, squareSize, 0, 0);
						//ScreenManager.SpriteBatch.Draw(ScreenManager.Blank, new Rectangle((int)(squareSize * i + squareDisp.X), (int)(squareSize * j + squareDisp.Y), squareSize, squareSize), Color.Red);
					}
					else {
						ScreenManager.SpriteBatch.Draw(ScreenManager.Blank, new Vector2(squareSize * i + squareDisp.X, squareSize * j + squareDisp.Y), null, Color.LightSteelBlue, 0, Vector2.Zero, squareSize, 0, 0);
					}
				}
			}

			ScreenManager.SpriteBatch.End();
			
			base.Draw(gameTime);
		}
	}
}