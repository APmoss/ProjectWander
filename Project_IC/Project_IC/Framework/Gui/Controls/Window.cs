using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project_IC.Framework.GSM;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project_IC.Framework.Gui.Controls {
	class Window : Panel {
		#region Fields
		public bool Moveable = true;
		public string Title = string.Empty;
		public Rectangle TitleBounds = Rectangle.Empty;
		public Rectangle DestBounds = Rectangle.Empty;
		protected bool Grabbed = false;
		protected bool Minimized = false;

		protected SimpleButton CloseButton;
		protected SimpleButton MinimizeButton;
		#endregion

		public Window(int x, int y, int width, int height, string title, bool moveable)
			: base(x, y, width, height) {

			this.Title = title;
			this.Moveable = moveable;

			DestBounds = new Rectangle(x, y, width, height);
		}

		public override void Initialize() {
			TitleBounds.Width = Bounds.Width;
			TitleBounds.Height = (int)Visuals.Font.MeasureString(Title).Y + Visuals.Padding;

			CloseButton = new SimpleButton(0, 0, Visuals.ControlSrcRecs["windowClose"], Visuals.ControlSrcRecs["windowCloseFocus"]);
			CloseButton.Bounds.X = Bounds.Right - TitleBounds.Height / 2 - CloseButton.Bounds.Width / 2;
			CloseButton.Bounds.Y = Bounds.Top + TitleBounds.Height / 2 - CloseButton.Bounds.Height / 2;
			MinimizeButton = new SimpleButton(0, 0, Visuals.ControlSrcRecs["windowMinimize"], Visuals.ControlSrcRecs["windowMinimizeFocus"]);
			MinimizeButton.Bounds.X = CloseButton.Bounds.Left - MinimizeButton.Bounds.Width - Visuals.Padding;
			MinimizeButton.Bounds.Y = CloseButton.Bounds.Y;

			CloseButton.LeftClicked += (s, e) => Parent.RemoveControls(this);
			MinimizeButton.LeftClicked += (s, e) => Minimized = !Minimized;

			Children.Add(CloseButton);
			Children.Add(MinimizeButton);

			base.Initialize();
		}

		public override void Update(GameTime gameTime) {
			TitleBounds.X = (int)GlobalPosition.X;
			TitleBounds.Y = (int)GlobalPosition.Y;

			CloseButton.Bounds = new Rectangle(0, 0, CloseButton.Bounds.Width, CloseButton.Bounds.Height);
			CloseButton.Bounds.X = (int)GlobalPosition.X + Bounds.Width - TitleBounds.Height / 2 - CloseButton.Bounds.Width / 2;
			CloseButton.Bounds.Y = (int)GlobalPosition.Y + TitleBounds.Height / 2 - CloseButton.Bounds.Height / 2;
			MinimizeButton.Bounds = new Rectangle(0, 0, MinimizeButton.Bounds.Width, MinimizeButton.Bounds.Height);
			MinimizeButton.Bounds.X = CloseButton.Bounds.Left - MinimizeButton.Bounds.Width - Visuals.Padding;
			MinimizeButton.Bounds.Y = CloseButton.Bounds.Y;

			if (Minimized) {
				Bounds.Width = (int)MathHelper.Lerp(Bounds.Width, TitleBounds.Width, .1f);
				Bounds.Height = (int)MathHelper.Lerp(Bounds.Height, TitleBounds.Height, .1f);
			}
			else {
				Bounds.Width = (int)MathHelper.Lerp(Bounds.Width, DestBounds.Width, .1f);
				Bounds.Height = (int)MathHelper.Lerp(Bounds.Height, DestBounds.Height, .1f);
			}

			if (!Minimized) {
				base.Update(gameTime);
			}
			else {
				CloseButton.Update(gameTime);
				MinimizeButton.Update(gameTime);
			}
		}

		public override void UpdateInput(InputManager input) {
			var currentMousePoint = new Point(input.MouseState.X, input.MouseState.Y);

			if (!GuiManager.MouseRecieved) {
				if (Moveable && TitleBounds.Contains(currentMousePoint) && input.IsMousePressed(MouseButton.Left)) {
					Grabbed = true;
				}
			}

			if (Grabbed) {
				var md = new Point((int)input.MouseDelta.X, (int)input.MouseDelta.Y);
				Bounds.X += md.X;
				Bounds.Y += md.Y;
				CloseButton.Bounds.X += md.X;
				CloseButton.Bounds.Y += md.Y;
				MinimizeButton.Bounds.X += md.X;
				MinimizeButton.Bounds.Y += md.Y;
			}

			if (Grabbed &&
						input.MouseState.LeftButton == ButtonState.Released &&
						input.LastMouseState.LeftButton == ButtonState.Pressed) {

				Grabbed = false;
			}

			if (!Minimized) {
				base.UpdateInput(input);
			}
			else {
				CloseButton.UpdateInput(input);
				MinimizeButton.UpdateInput(input);
			}
		}

		public override void Draw(GameTime gameTime, ScreenManager screenManager) {
			if (!Minimized) {
				base.Draw(gameTime, screenManager);
			}
			else {
				DrawSurround(screenManager.SpriteBatch, Visuals.ControlSrcRecs["panelCorner"], Visuals.ControlSrcRecs["panelSide"], Visuals.ControlSrcRecs["panelFill"]);
				CloseButton.Draw(gameTime, screenManager);
				MinimizeButton.Draw(gameTime, screenManager);
			}
			
			var underlineCorner = Visuals.ControlSrcRecs["underlineCorner"];
			var underline = Visuals.ControlSrcRecs["underline"];

			// Underline
			// Left
			var leftUnderline = new Vector2(GlobalPosition.X, GlobalPosition.Y + TitleBounds.Height - underlineCorner.Height);
			screenManager.SpriteBatch.Draw(Visuals.GuiSheet, leftUnderline, underlineCorner, PrimaryTint);
			// Right
			var rightUnderline = new Vector2(GlobalPosition.X + Bounds.Width - underlineCorner.Width, GlobalPosition.Y + TitleBounds.Height - underline.Height);
			screenManager.SpriteBatch.Draw(Visuals.GuiSheet, rightUnderline, underlineCorner, PrimaryTint, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
			// Center
			var centerUnderline = new Rectangle((int)leftUnderline.X + underlineCorner.Width, (int)leftUnderline.Y, (int)rightUnderline.X - (int)leftUnderline.X - underlineCorner.Width, underlineCorner.Height);
			screenManager.SpriteBatch.Draw(Visuals.GuiSheet, centerUnderline, underline, PrimaryTint);

			// Title
			var centerDisplacement = new Vector2(Bounds.Width / 2 - Visuals.Font.MeasureString(Title).X / 2, 3);
			screenManager.SpriteBatch.DrawString(Visuals.Font, Title, GlobalPosition + centerDisplacement, Visuals.TextTint);
		}
	}
}
