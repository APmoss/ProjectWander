using System;
using System.Collections.Generic;
using Project_IC.Framework.Gui.Controls;
using Microsoft.Xna.Framework;
using Project_IC.Screens.Menus;
using Microsoft.Xna.Framework.Graphics;

namespace Project_IC.Screens {
	class GuiTestScreen : MenuScreen {
		#region SetGui
		Window Window1, Window2, Window3;
		Panel Panel1;
		Image Pic;
		Button TintChange;
		Button BackButton;

		protected override void SetGui() {
			Point center = new Point(ScreenManager.Res.X / 2, ScreenManager.Res.Y / 2);

			Window1 = new Window(10, 100, 300, 300, "Window 1", true);
			Window1.AddControls(new Image(ScreenManager.Game.Content.Load<Texture2D>("pic"), 25, 40, 250, 250));

			Window2 = new Window(center.X - 150, 100, 300, 300, "Window 2", true);
			Window2.PrimaryTint = Color.Blue;

			Window3 = new Window(ScreenManager.Res.X - 310, 300, 300, 300, "Window 3", true);
			Window3.PrimaryTint = Color.Red;
			Window3.AddControls(new Window(10, 40, 200, 200, "Inside Window", false) { PrimaryTint = Color.LightGreen });

			Panel1 = new Panel(300, 10, 500, 80);
			Panel1.AddControls(new Label(10, 10, "This is a Panel.", 1), new Button(200, 10, 100, "A Button"));

			Pic = new Image(ScreenManager.Game.Content.Load<Texture2D>("pic"), 1000, 10, 200, 200);

			TintChange = new Button(1000, 220, 200, "Change Tint");
			TintChange.LeftClicked += (s, e) => {
				if (Pic.PrimaryTint == Color.White) {
					Pic.PrimaryTint = Color.Red;
				}
				else if (Pic.PrimaryTint == Color.Red) {
					Pic.PrimaryTint = Color.Green;
				}
				else if (Pic.PrimaryTint == Color.Green) {
					Pic.PrimaryTint = Color.Blue;
				}
				else {
					Pic.PrimaryTint = Color.White;
				}
			};

			BackButton = new Button(10, ScreenManager.Res.Y - 60, 100, "Back");
			BackButton.LeftClicked += (s, e) => { ExitScreen(); };

			Gui.BaseScreen.AddControls(Window1, Window2, Window3, Panel1, Pic, TintChange, BackButton);

			base.SetGui();
		}
		#endregion
	}
}
