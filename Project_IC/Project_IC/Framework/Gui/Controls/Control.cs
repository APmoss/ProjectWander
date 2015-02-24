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
		public bool Hidden = false;
		public Color PrimaryTint = Color.White;
		public bool RecievesMouse = false;

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
					return Rectangle.Intersect(new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, Bounds.Width, Bounds.Height), Parent.GlobalIntersection);
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
			var childrenToUpdate = new Stack<Control>(Children);

			while (childrenToUpdate.Count > 0) {
				Control child = childrenToUpdate.Pop();

				if (!child.Hidden) {
					child.Update(gameTime);
				}
			}
		}

		public virtual void UpdateInput(InputManager input) {
			if (Enabled && GlobalIntersection != Rectangle.Empty) {// && !GuiManager.MouseRecieved) {
				var currentMousePoint = new Point(input.MouseState.X, input.MouseState.Y);
				var lastMousePoint = new Point(input.LastMouseState.X, input.LastMouseState.Y);

				if (GlobalIntersection.Contains(currentMousePoint) && !GuiManager.MouseRecieved) {
					ContainsMouse = true;
					if (!GlobalIntersection.Contains(lastMousePoint)) {
						if (MouseEntered != null) {
							MouseEntered.Invoke(this, EventArgs.Empty);
						}
					}

					if (RecievesMouse) {
						GuiManager.MouseRecieved = true;
					}
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

			var childrenToUpdate = new Stack<Control>(Children);

			while (childrenToUpdate.Count > 0) {
				Control child = childrenToUpdate.Pop();

				if (!child.Hidden) {
					child.UpdateInput(input);
				}
			}
		}

		public virtual void Draw(GameTime gameTime, ScreenManager screenManager) {
			foreach (var child in Children) {
				if (!child.Hidden) {
					child.Draw(gameTime, screenManager);
				}
			}
		}
		protected void DrawSurround(SpriteBatch spriteBatch, Rectangle corner, Rectangle side, Rectangle fill) {
			// Calculate where each item of the surrounding panel should be drawn
			// Corners
			var topLeft = new Vector2(GlobalPosition.X, GlobalPosition.Y);
			spriteBatch.Draw(Visuals.GuiSheet, topLeft, corner, PrimaryTint);

			var bottomLeft = new Vector2(GlobalPosition.X, GlobalPosition.Y + Bounds.Height - corner.Height);
			spriteBatch.Draw(Visuals.GuiSheet, bottomLeft, corner, PrimaryTint, 0, Vector2.Zero, 1, SpriteEffects.FlipVertically, 0);

			var topRight = new Vector2(GlobalPosition.X + Bounds.Width - corner.Width, GlobalPosition.Y);
			spriteBatch.Draw(Visuals.GuiSheet, topRight, corner, PrimaryTint, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);

			var bottomRight = new Vector2(GlobalPosition.X + Bounds.Width - corner.Width, GlobalPosition.Y + Bounds.Height - corner.Height);
			spriteBatch.Draw(Visuals.GuiSheet, bottomRight, corner, PrimaryTint, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically, 0);

			// Sides
			var left = new Rectangle((int)topLeft.X, (int)topLeft.Y + corner.Height, side.Width, (int)bottomLeft.Y - (int)topLeft.Y - corner.Height);
			spriteBatch.Draw(Visuals.GuiSheet, left, side, PrimaryTint);
			// Width and height are flipped because it stretches before rotation
			var top = new Rectangle((int)topRight.X, (int)topRight.Y, side.Width, (int)topRight.X - (int)topLeft.X - corner.Width);
			spriteBatch.Draw(Visuals.GuiSheet, top, side, PrimaryTint, MathHelper.PiOver2, Vector2.Zero, 0, 0);
			// W/H for right and bottom must be equal to W/H of left and right
			var right = new Rectangle((int)bottomRight.X + corner.Width, (int)bottomRight.Y, left.Width, left.Height);
			spriteBatch.Draw(Visuals.GuiSheet, right, side, PrimaryTint, MathHelper.Pi, Vector2.Zero, 0, 0);
			var bottom = new Rectangle((int)bottomLeft.X + corner.Width, (int)bottomLeft.Y + corner.Height, top.Width, top.Height);
			spriteBatch.Draw(Visuals.GuiSheet, bottom, side, PrimaryTint, -MathHelper.PiOver2, Vector2.Zero, 0, 0);

			// Center fill
			// We can reuse a lot of the dimensions we already have since it's just a center rectangle
			var center = new Rectangle(bottom.X, left.Y, top.Height, left.Height);
			spriteBatch.Draw(Visuals.GuiSheet, center, fill, PrimaryTint);
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

		public void RemoveControls(params Control[] controls) {
			foreach (var control in controls) {
				Children.Remove(control);
			}
		}
	}
}
