using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project_IC.Framework.GSM;

namespace Project_IC.Framework.Gui.Controls {
	class Control {
		#region Fields
		protected internal GuiManager GuiManager;
		protected internal Control Parent;
		protected List<Control> Children;
		protected internal Visuals Visuals;

		public Rectangle Bounds = Rectangle.Empty;

		bool initialized = false;
		public bool Enabled = true;
		public bool ContainsMouse = false;
		#endregion

		#region Properties
		public Rectangle GlobalBounds {
			get {
				if (Parent != null) {
					return Rectangle.Intersect(Bounds, Parent.GlobalBounds);
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
			if (Enabled && GlobalBounds != Rectangle.Empty && !GuiManager.MouseRecieved) {
				var lastMousePoint = new Point(input.MouseState.X, input.MouseState.Y);
				var currentMousePoint = new Point(input.LastMouseState.X, input.LastMouseState.Y);

				if (GlobalBounds.Contains(currentMousePoint)) {
					ContainsMouse = true;
					if (!GlobalBounds.Contains(lastMousePoint)) {
						if (MouseEntered != null) {
							MouseEntered.Invoke(this, EventArgs.Empty);
						}
					}

					GuiManager.MouseRecieved = true;
				}
				else {
					ContainsMouse = false;
					if (GlobalBounds.Contains(lastMousePoint)) {
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

		public void AddControls(params Control[] controls) {
			foreach (var control in controls) {
				control.Parent = this;

				if (initialized) {
					control.GuiManager = this.GuiManager;
					control.Visuals = this.Visuals;
				}

				Children.Add(control);
			}
		}
	}
}
