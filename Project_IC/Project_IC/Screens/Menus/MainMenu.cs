using System;
using System.Collections.Generic;
using Project_IC.Framework.Gui.Controls;
using Microsoft.Xna.Framework;
using Project_IC.Framework.Gui;

namespace Project_IC.Screens.Menus {
	class MainMenu : MenuScreen {
		#region SetGui
		Label Title;
		Button SinglePlayerButton;
		Button MultiPlayerButton;
		Button OptionsButton;
		Button AchievementsButton;
		Button QuitButton;
		Button TestScreenButton;

		Button GuiTestButton;
		Button PrimitiveTestButton;
		Button ParticleTestButton;

		protected override void SetGui() {
			Point center = new Point(ScreenManager.Res.X / 2, ScreenManager.Res.Y / 2);

			Title = new Label(220, 200, "PROJECT WANDER: Engine Demo", 1);
			Title.PrimaryTint = Color.LightBlue;
			Title.Visuals = new DarkThemeVisuals(ScreenManager) {
				Font = ScreenManager.FontLibrary.GetFont("calibri")
			};

			SinglePlayerButton = new Button(center.X - 100, ScreenManager.Res.Y - 250, 200, "Single Player");

			MultiPlayerButton = new Button(center.X - 100, ScreenManager.Res.Y - 200, 200, "Multi Player");

			OptionsButton = new Button(center.X - 100, ScreenManager.Res.Y - 150, 200, "Options");

			AchievementsButton = new Button(center.X - 100, ScreenManager.Res.Y - 100, 200, "Achievements");

			QuitButton = new Button(center.X - 100, ScreenManager.Res.Y - 50, 200, "Quit");
			QuitButton.PrimaryTint = Color.Red;
			QuitButton.LeftClicked += (s, e) => ScreenManager.Game.Exit();

			TestScreenButton = new Button(100, center.X, 0, "Test Screen");
			TestScreenButton.LeftClicked += (s, e) => ScreenManager.AddScreen(new TestScreen());

			GuiTestButton = new Button(100, 30, 200, "Gui Test");
			GuiTestButton.LeftClicked += (s, e) => ScreenManager.AddScreen(new GuiTestScreen());

			PrimitiveTestButton = new Button(100, 90, 200, "Primitive Rendering Test");
			PrimitiveTestButton.LeftClicked += (s, e) => ScreenManager.AddScreen(new PrimitiveTestScreen());

			ParticleTestButton = new Button(100, 150, 200, "Particle Engine Test");
			ParticleTestButton.LeftClicked += (s, e) => ScreenManager.AddScreen(new ParticleTestScreen());

			Gui.BaseScreen.AddControls(Title, /*SinglePlayerButton, MultiPlayerButton, OptionsButton, AchievementsButton,*/ QuitButton, TestScreenButton, GuiTestButton, PrimitiveTestButton, ParticleTestButton);

			base.SetGui();
		}
		#endregion
	}
}
