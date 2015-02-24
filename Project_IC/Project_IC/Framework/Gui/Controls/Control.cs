using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project_IC.Framework.GSM;
using Microsoft.Xna.Framework.Graphics;

namespace Project_IC.Framework.Gui.Controls {
	class Control {
		#region Fields
		protected internal GuiManager GuiManager;
		protected internal Control Parent;
		protected List<Control> Children = new List<Control>();
		protected internal Visuals Visuals = new Visuals();

		public Rectangle Bounds = Rectangle.Empty;

		bool initialized = false;
		public bool Enabled = true;
		public bool ContainsMouse = false;
		#endregion

		#region Properties
		public Vector2 GlobalPosition {
			get {
				if (Parent != null) {
					return Parent.GlobalPosition + new Vector2(Bounds.X, Bounds.Y);
				}
				return new Vector2(Bounds.X, Bounds.Y);
			}
		}
		public Rectangle GlobalIntersection {
			get {
				if (Parent != null) {
					return Rectangle.Intersect(Bounds, Parent.GlobalIntersection);
				}
				return Bounds;
			}
		}
		#endregion

		#region Events
		public event EventHandler<EventArgs> MouseEntered;
		public event EventHandler<EventArgs> MouseExited;
		public event EventHandler<EventArgs> LeftClicked;
		public event EventHandler<EventArgs> MiddleClicked;
		public event EventHandler<EventArgs> RightClicked;
		#endregion

		public virtual void Initialize() {
			foreach (var child in Children) {
				child.GuiManager = this.GuiManager;
				child.Visuals = this.Visuals;
				child.Initialize();
			}

			initialized = true;
		}

		public virtual void Update(GameTime gameTime) {
			foreach (var child in Children) {
				child.Update(gameTime);
			}
		}

		public virtual void UpdateInput(InputManager input) {
			if (Enabled && GlobalIntersection != Rectangle.Empty && !GuiManager.MouseRecieved) {
				var lastMousePoint = new Point(input.MouseState.X, input.MouseState.Y);
				var currentMousePoint = new Point(input.LastMouseState.X, input.LastMouseState.Y);

				if (GlobalIntersection.Contains(currentMousePoint)) {
					ContainsMouse = true;
					if (!GlobalIntersection.Contains(lastMousePoint)) {
						if (MouseEntered != null) {
							MouseEntered.Invoke(this, EventArgs.Empty);
						}
					}

					GuiManager.MouseRecieved = true;
				}
				else {
					ContainsMouse = false;
					if (GlobalIntersection.Contains(lastMousePoint)) {
						if (MouseExited != null) {
							MouseExited.Invoke(this, EventArgs.Empty);
						}
					}
				}

				if (ContainsMouse) {
					if (input.IsMousePressed(MouseButton.Left)) {
						if (LeftClicked != null) {
							LeftClicked.Invoke(this, EventArgs.Empty);
						}
					}
					if (input.IsMousePressed(MouseButton.Middle)) {
						if (MiddleClicked != null) {
							MiddleClicked.Invoke(this, EventArgs.Empty);
						}
					}
					if (input.IsMousePressed(MouseButton.Right)) {
						if (RightClicked != null) {
							RightClicked.Invoke(this, EventArgs.Empty);
						}
					}
				}
			}

			foreach (var child in Children) {
				child.UpdateInput(input);
			}
		}

		public virtual void Draw(GameTime gameTime, ScreenManager screenManager) {
			foreach (var child in Children) {
				child.Draw(gameTime, screenManager);
			}
		}
		protected void DrawPanel(SpriteBatch spriteBatch) {
			// Access source rectangles needed for drawing the panel
			var corner = Visuals.ControlSrcRecs["corner"];
			var side = Visuals.ControlSrcRecs["side"];
			var fill = Visuals.ControlSrcRecs["fill"];

			// Calculate where each item of the panel should be drawn
			// Corners
			var topLeft = new Vector2(GlobalPosition.X, GlobalPosition.Y);
			spriteBatch.Draw(Visuals.GuiSheet, topLeft, corner, Color.White);

			var bottomLeft = new Vector2(GlobalPosition.X, GlobalPosition.Y + Bounds.Height - corner.Height);
			spriteBatch.Draw(Visuals.GuiSheet, bottomLeft, corner, Color.Red, 0, Vector2.Zero, 1, SpriteEffects.FlipVertically, 0);

			var topRight = new Vector2(GlobalPosition.X + Bounds.Width - corner.Height, GlobalPosition.Y);
			spriteBatch.Draw(Visuals.GuiSheet, topRight, corner, Color.Green, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);

			var bottomRight = new Vector2(GlobalPosition.X + Bounds.Width - corner.Height, GlobalPosition.Y + Bounds.Height - corner.Height);
			spriteBatch.Draw(Visuals.GuiSheet, bottomRight, corner, Color.Blue, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically, 0);

			// Sides
			var left = new Rectangle((int)topLeft.X, (int)topLeft.Y + corner.Height, side.Width, (int)bottomLeft.Y - (int)topLeft.Y - corner.Height);
			spriteBatch.Draw(Visuals.GuiSheet, left, side, Color.White);
			// Width and height are flipped because it stretches before rotation
			var top = new Rectangle((int)topRight.X, (int)topRight.Y, side.Width, (int)topRight.X - (int)topLeft.X - corner.Width);
			spriteBatch.Draw(Visuals.GuiSheet, top, side, Color.Green, MathHelper.PiOver2, Vector2.Zero, 0, 0);
			// W/H for right and bottom must be equal to W/H of left and right
			var right = new Rectangle((int)bottomRight.X + corner.Width, (int)bottomRight.Y, left.Width, left.Height);
			spriteBatch.Draw(Visuals.GuiSheet, right, side, Color.Blue, MathHelper.Pi, Vector2.Zero, 0, 0);
			var bottom = new Rectangle((int)bottomLeft.X + corner.Width, (int)bottomLeft.Y + corner.Height, top.Width, top.Height);
			spriteBatch.Draw(Visuals.GuiSheet, bottom, side, Color.Red, -MathHelper.PiOver2, Vector2.Zero, 0, 0);

			// Center fill
			//asdasdasdadsadasd
		}

		public void AddControls(params Control[] controls) {
			foreach (var control in controls) {
				control.Parent = this;

				if (initialized) {
					control.GuiManager = this.GuiManager;
					control.Visuals = this.Visuals;
					control.Initialize();
				}

				Children.Add(control);
			}
		}
	}
}
