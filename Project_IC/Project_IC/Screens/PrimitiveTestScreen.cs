using System;
using System.Collections.Generic;
using Project_IC.Screens.Menus;
using Project_IC.Framework.Gui.Controls;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project_IC.Screens {
	class PrimitiveTestScreen : MenuScreen {
		const int MAX_SIDES = 500;
		const int CIRCLE_RADIUS = 300;

		#region Fields
		int sides = 3;
		bool follow = false;
		FillMode fillMode = FillMode.Solid;
		Vector2 mousePos = Vector2.Zero;

		List<VertexPositionColor> vertices = new List<VertexPositionColor>();

		VertexBuffer vertexBuffer;
		BasicEffect basicEffect;
		Effect fade;
		#endregion

		public override void LoadContent() {
			vertices.Add(new VertexPositionColor(new Vector3(0, 0, 0), Color.White));
			vertices.Add(new VertexPositionColor(new Vector3(0, 0, 0), Color.White));
			vertices.Add(new VertexPositionColor(new Vector3(0, 0, 0), Color.White));

			vertexBuffer = new VertexBuffer(ScreenManager.GraphicsDevice, typeof(VertexPositionColor), MAX_SIDES * 3, BufferUsage.WriteOnly);
			basicEffect = new BasicEffect(ScreenManager.GraphicsDevice);
			basicEffect.World = Matrix.Identity;
			basicEffect.View = Matrix.CreateLookAt(Vector3.Backward, Vector3.Forward, Vector3.Up);
			basicEffect.Projection = Matrix.CreateOrthographic(1280, 720, -1, 1);
			basicEffect.VertexColorEnabled = true;

			fade = ScreenManager.Game.Content.Load<Effect>("effect");
			
			base.LoadContent();
		}

		public override void Update(GameTime gameTime, bool hasFocus, bool covered) {
			vertices.Clear();
			for (int i = 0; i < sides; i++) {
				vertices.Add(new VertexPositionColor(new Vector3(GetSideVertex(i, sides, gameTime), 0), Color.Red));
				vertices.Add(new VertexPositionColor(new Vector3(GetSideVertex(i + 1, sides, gameTime), 0), Color.Green));
				if (follow) {
					vertices.Add(new VertexPositionColor(new Vector3(new Vector2(mousePos.X, -mousePos.Y) + new Vector2(-ScreenManager.Res.X / 2, ScreenManager.Res.Y / 2), 0), Color.Blue));
				}
				else {
					vertices.Add(new VertexPositionColor(new Vector3(0, 0, 0), Color.Blue));
				}
			}

			base.Update(gameTime, hasFocus, covered);
		}

		public override void UpdateInput(Framework.GSM.InputManager input) {
			mousePos = new Vector2(input.MouseState.X, input.MouseState.Y);

			if (input.IsKeyPressed(Keys.OemMinus)) {
				sides = (int)MathHelper.Clamp(sides - 1, 3, MAX_SIDES);

			}
			else if (input.IsKeyPressed(Keys.OemPlus)) {
				sides = (int)MathHelper.Clamp(sides + 1, 3, MAX_SIDES);
			}

			sides = (int)MathHelper.Clamp(sides + input.MouseScrollDelta / 100, 3, MAX_SIDES);
			
			base.UpdateInput(input);
		}

		public override void Draw(GameTime gameTime) {
			vertexBuffer.SetData<VertexPositionColor>(vertices.ToArray());

			ScreenManager.GraphicsDevice.SetVertexBuffer(vertexBuffer);
			ScreenManager.GraphicsDevice.RasterizerState = new RasterizerState() {
				CullMode = CullMode.None,
				FillMode = fillMode
			};

			foreach (var pass in basicEffect.CurrentTechnique.Passes) {
				pass.Apply();
				//fade.CurrentTechnique.Passes[0].Apply();
				ScreenManager.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, vertices.Count / 3);
			}

			ScreenManager.SpriteBatch.Begin();
			foreach (var vert in vertices) {
				var pos = new Vector2(ScreenManager.Res.X / 2, ScreenManager.Res.Y / 2) + new Vector2(vert.Position.X, -vert.Position.Y);
				ScreenManager.SpriteBatch.Draw(ScreenManager.Blank, pos, null, Color.White, 0, Vector2.Zero, 3, 0, 0);
			}
			ScreenManager.SpriteBatch.End();

			base.Draw(gameTime);
		}

		protected Vector2 GetSideVertex(int currentVertex, int totalSideVertices, GameTime gameTime) {
			Vector2 pos = Vector2.Zero;

			float frac = ((float)currentVertex / (float)totalSideVertices) * MathHelper.TwoPi;

			pos.X = (float)Math.Cos(frac + gameTime.TotalGameTime.TotalSeconds / 2) * CIRCLE_RADIUS;
			pos.Y = (float)Math.Sin(frac + gameTime.TotalGameTime.TotalSeconds / 2) * CIRCLE_RADIUS;

			return pos;
		}

		#region SetGui
		Button MultiSampleButton;
		Button WireButton;
		Button FollowButton;
		Button DecreaseButton, IncreaseButton;
		Button BackButton;

		protected override void SetGui() {
			MultiSampleButton = new Button(ScreenManager.Res.X - 160, ScreenManager.Res.Y - 300, 150, "Quad MultiSampling");
			MultiSampleButton.LeftClicked += (s, e) => ScreenManager.MultiSampling = !ScreenManager.MultiSampling;

			WireButton = new Button(ScreenManager.Res.X - 160, ScreenManager.Res.Y - 240, 150, "Wireframe");
			WireButton.LeftClicked += (s, e) => fillMode = fillMode == FillMode.Solid ? FillMode.WireFrame : FillMode.Solid;

			FollowButton = new Button(ScreenManager.Res.X - 160, ScreenManager.Res.Y - 180, 150, "Follow Mouse");
			FollowButton.LeftClicked += (s, e) => follow = !follow;

			DecreaseButton = new Button(ScreenManager.Res.X - 160, ScreenManager.Res.Y - 120, 150, "Decrease Complexity");
			DecreaseButton.LeftClicked += (s, e) => sides = (int)MathHelper.Clamp(sides - 1, 3, MAX_SIDES);

			IncreaseButton = new Button(ScreenManager.Res.X - 160, ScreenManager.Res.Y - 60, 150, "Increase Complexity");
			IncreaseButton.LeftClicked += (s, e) => sides = (int)MathHelper.Clamp(sides + 1, 3, MAX_SIDES);

			BackButton = new Button(10, ScreenManager.Res.Y - 60, 100, "Back");
			BackButton.LeftClicked += (s, e) => { ExitScreen(); };

			Gui.BaseScreen.AddControls(MultiSampleButton, WireButton, FollowButton, DecreaseButton, IncreaseButton, BackButton);
			
			base.SetGui();
		}
		#endregion
	}
}
