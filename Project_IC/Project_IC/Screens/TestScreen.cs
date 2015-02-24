using System;
using System.Collections.Generic;
using Project_IC.Framework.GSM;
using Microsoft.Xna.Framework;
using Project_IC.Framework.Gui;
using Project_IC.Framework.Gui.Controls;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project_IC.Framework.Gameplay;
using Project_IC.Framework.ParticleSystem;
using Project_IC.Framework.ParticleSystem.ParticleModifiers;
using Project_IC.Framework.ParticleSystem.ParticleEmitters;

namespace Project_IC.Screens {
	class TestScreen : Screen {
		GuiManager gui;
		ParticleManager part;
		Camera cam;

		Label labbell = new Label(0, 100, "THIS IS A TEST THAT MOVES REALLY FAST THAT YOU CAN'T READ AT FULL SPEED UNTIL I PRESS A BUTTONTHAT SLOWS DOWN TIME BECAUSE I CAN CONTROL " +
											"TIME ISN'T THAT AWESOME I BET YOUR GAME IS NOT AS COOL AS THIS ONE HAHAHAH", 1);
		float labbellX = 0;
		Window pannell = new Window(0, 300, 500, 300, "Title test thing blah", true);
		Button buttonn = new Button(100, 40, 300, "asd");

		Texture2D texturree;
		Texture2D sampleBackground;
		Effect e;
		Random r = new Random();

		RenderTarget2D renderr;
		VertexBuffer vb;
		BasicEffect be;
		VertexPositionColor[] verts = new VertexPositionColor[6] {new VertexPositionColor(new Vector3(-640, 260, 0), Color.Red),
																	new VertexPositionColor(new Vector3(-540, 360, 0), Color.Green),
																	new VertexPositionColor(new Vector3(0, 0, 0), Color.Blue),
																	new VertexPositionColor(new Vector3(640, 260, 0), Color.Red),
																	new VertexPositionColor(new Vector3(540, 360, 0), Color.Green),
																	new VertexPositionColor(new Vector3(0, 0, 0), Color.Blue)};

		public TestScreen() {
			
		}

		public override void LoadContent() {
			gui = new GuiManager(new TealThemeVisuals(ScreenManager));
			//TODO: FIX THIS WITH SCREEN RESOLUTION
			gui.BaseScreen.Bounds = new Rectangle(0, 0, ScreenManager.Res.X, ScreenManager.Res.Y);
			gui.BaseScreen.Hidden = true;
			labbell.Bounds.Height = labbell.Bounds.Width = 100;
			pannell.AddControls(buttonn);

			gui.BaseScreen.AddControls(labbell, pannell);

			part = new ParticleManager(ScreenManager.Game.Content.Load<Texture2D>("textures/particleSheet"));
			part.MaxParticleCount = 99999;

			part.AddParticleEmitters(new List<ParticleEmitter>() { new SnowEmitter(new Rectangle(0, -200, 1280 + 600, 720)) });

			cam = new Camera(ScreenManager.Res.X, ScreenManager.Res.Y);
			cam.DestPosition = cam.TransPosition = new Vector2(500, 300);

			texturree = ScreenManager.Game.Content.Load<Texture2D>("textures/darkThemeGuiSheet");
			sampleBackground = ScreenManager.Game.Content.Load<Texture2D>("city");
			e = ScreenManager.Game.Content.Load<Effect>("effect");

			TmxMap tmxMap = TmxMap.Load("Content/testMap.tmx");

			renderr = new RenderTarget2D(ScreenManager.GraphicsDevice, 1280, 720);

			vb = new VertexBuffer(ScreenManager.GraphicsDevice, typeof(VertexPositionColor), verts.Length, BufferUsage.WriteOnly);

			be = new BasicEffect(ScreenManager.GraphicsDevice);
			be.World = Matrix.Identity;
			be.View = Matrix.CreateLookAt(Vector3.Backward, Vector3.Forward, Vector3.Up);
			//be.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 1280 / 720f, 0.01f, 100f);
			be.Projection = Matrix.CreateOrthographic(1280, 720, -1, 1);
			//be.Projection = Matrix.CreateOrthographicOffCenter(-640f, 640f, -360f, 360f, -1000f, 1000f);
			be.VertexColorEnabled = true;
			
			base.LoadContent();
		}

		public override void Update(GameTime gameTime, bool hasFocus, bool covered) {
			cam.Update();

			part.Update(gameTime);

			labbellX -= 700 * Stcs.PPS1(gameTime);
			if (labbellX < -500) {
				labbellX = ScreenManager.Res.X + 300; ;
			}
			labbell.Bounds.X = (int)labbellX;

			pannell.Title = "Title test - Particle Count: " + part.ParticleCount;

			DebugOverlay.DebugText.Append(" :: TC - ").Append(Stcs.TC).AppendLine();

			gui.Update(gameTime);

			base.Update(gameTime, hasFocus, covered);
		}

		public override void UpdateInput(InputManager input) {
			gui.UpdateInput(input);

			if (input.IsKeyPressed(Keys.H)) {
				gui.BaseScreen.Hidden = !gui.BaseScreen.Hidden;
			}
			if (input.IsKeyPressed(Keys.V)) {
				ScreenManager.VSync = !ScreenManager.VSync;
			}
			if (input.IsKeyPressed(Keys.M)) {
				ScreenManager.MultiSampling = !ScreenManager.MultiSampling;
			}
			if (input.IsKeyPressed(Keys.Z)) {
				Stcs.TC = 0;
			}
			if (input.IsKeyPressed(Keys.G)) {
				pannell.Visuals = new DarkThemeVisuals(ScreenManager);
			}
			if (input.IsKeyPressed(Keys.F)) {
				//labbell.Hidden = !labbell.Hidden;
				ScreenManager.Res = ScreenManager.Res == new Point(1920, 1080) ? new Point(1280, 720) : new Point(1920, 1080);
				ScreenManager.FullScreen = !ScreenManager.FullScreen;
			}
			if (input.IsKeyPressed(Keys.Escape)) {
				ExitScreen();
			}
			Stcs.TC += (.001f * input.MouseScrollDelta);
			if (input.IsKeyDown(Keys.P)) {
				for (int i = 0; i < 10; i++) {
					Vector2 v = new Vector2((float)r.NextDouble(), (float)r.NextDouble());
					v.Normalize();

					part.AddParticles(new List<Particle>() {
						new Particle() {
							SourceRec = new Rectangle(0, 0, 16, 16),
							Velocity = v,
							Scale = (float)r.NextDouble() / 2 + .25f,
							Tint = new Color(r.Next(5, 250), r.Next(5, 250), r.Next(5, 250)),
							//Tint = new Color(0, 0, r.Next(100, 200)),
							Modifiers = new List<ParticleModifier>() {new BasicModifier()}
						}
					});
				}
			}

			if (input.IsKeyDown(Keys.W)) {
				cam.DestPosition.Y -= 5;
			}
			if (input.IsKeyDown(Keys.A)) {
				cam.DestPosition.X -= 5;
			}
			if (input.IsKeyDown(Keys.S)) {
				cam.DestPosition.Y += 5;
			}
			if (input.IsKeyDown(Keys.D)) {
				cam.DestPosition.X += 5;
			}

			verts[2].Position.X = input.MouseState.X - 640;
			verts[2].Position.Y = -input.MouseState.Y + 360;
			verts[5].Position.X = input.MouseState.X - 640;
			verts[5].Position.Y = -input.MouseState.Y + 360;

			base.UpdateInput(input);
		}

		public override void Draw(GameTime gameTime) {
			ScreenManager.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, e, cam.Transformation);
			
			ScreenManager.SpriteBatch.Draw(sampleBackground, new Rectangle(-128, -128, 1024 + 512, 1024 + 512), Color.Gray);

			part.Draw(gameTime, ScreenManager);

			ScreenManager.SpriteBatch.End();


			ScreenManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);

			gui.Draw(gameTime, ScreenManager);

			ScreenManager.SpriteBatch.End();


			vb.SetData<VertexPositionColor>(verts);
			ScreenManager.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
			ScreenManager.GraphicsDevice.SetVertexBuffer(vb);

			be.World = Matrix.Invert(cam.Transformation);

			foreach (var pass in be.CurrentTechnique.Passes) {
				pass.Apply();
				//ScreenManager.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, verts.Length / 3);
			}

			base.Draw(gameTime);
		}
	}
}
