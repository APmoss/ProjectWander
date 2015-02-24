﻿using System;
using System.Collections.Generic;
using Project_IC.Framework.GSM;
using Microsoft.Xna.Framework;
using Project_IC.Framework.Gui;
using Project_IC.Framework.Gui.Controls;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project_IC.Framework.Gameplay;
using Project_IC.Framework.ParticleSystem;

namespace Project_IC.Screens {
	class TestScreen : Screen {
		GuiManager gui;
		ParticleManager part;

		Label labbell = new Label(0, 100, "asdasdasdasdasdasdadsasdasd");
		Window pannell = new Window(0, 300, 500, 300, "Title test thing blah", true);
		Button buttonn = new Button(100, 40, 300, "asd");

		Texture2D texturree;
		Random r = new Random();

		public TestScreen() {
			
		}

		public override void LoadContent() {
			gui = new GuiManager(new DarkThemeVisuals(ScreenManager));
			//TODO: FIX THIS WITH SCREEN RESOLUTION
			gui.BaseScreen.Bounds = ScreenManager.Game.GraphicsDevice.Viewport.Bounds;
			labbell.Bounds.Height = labbell.Bounds.Width = 100;
			pannell.AddControls(buttonn);

			gui.BaseScreen.AddControls(new Label(0, 0, "Tessssssssssst"), labbell, pannell,
										new Button(550, 100, 500, "1111111111111111111111"),
										new Button(550, 150, 500, "2222222222222222222222"),
										new Button(550, 200, 500, "3333333333333333333333"),
										new Button(550, 250, 500, "4444444444444444444444"),
										new Button(550, 300, 500, "5555555555555555555555"),
										new Button(550, 350, 500, "6666666666666666666666"),
										new Button(550, 400, 500, "7777777777777777777777"),
										new Button(550, 450, 500, "8888888888888888888888"),
										new Button(550, 500, 500, "9999999999999999999999"));

			part = new ParticleManager(ScreenManager.Game.Content.Load<Texture2D>("textures/particleSheet"));
			part.MaxParticleCount = 99999;

			texturree = ScreenManager.Game.Content.Load<Texture2D>("textures/darkThemeGuiSheet");

			TmxMap tmxMap = TmxMap.Load("Content/testMap.tmx");

			base.LoadContent();
		}

		public override void Update(GameTime gameTime, bool hasFocus, bool covered) {
			//for (int i = 0; i < 1000000; i++) ;

			part.Update(gameTime);

			pannell.Title = "Title test thing blah - " + part.ParticleCount;

			gui.Update(gameTime);

			base.Update(gameTime, hasFocus, covered);
		}

		public override void UpdateInput(InputManager input) {
			gui.UpdateInput(input);

			if (input.IsKeyPressed(Keys.F)) {
				//labbell.Hidden = !labbell.Hidden;
				ScreenManager.Res = ScreenManager.Res == new Point(1920, 1080) ? new Point(1280, 720) : new Point(1920, 1080);
				ScreenManager.FullScreen = !ScreenManager.FullScreen;
			}
			if (input.IsKeyPressed(Keys.Escape)) {
				ExitScreen();
			}
			if (input.IsKeyDown(Keys.P)) {
				for (int i = 0; i < 55; i++) {
					Vector2 v = new Vector2((float)r.NextDouble(), (float)r.NextDouble());
					v.Normalize();

					part.AddParticles(new List<Particle>() {
						new Particle() {
							SourceRec = new Rectangle(0, 0, 16, 16),
							Velocity = v,
							Scale = (float)r.NextDouble() / 2 + .25f,
							Tint = new Color(r.Next(5, 250), r.Next(5, 250), r.Next(5, 250))
						}
					});
				}
			}
			
			base.UpdateInput(input);
		}

		public override void Draw(GameTime gameTime) {
			ScreenManager.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

			part.Draw(gameTime, ScreenManager);

			gui.Draw(gameTime, ScreenManager);

			ScreenManager.SpriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
