using System;
using System.Collections.Generic;
using Project_IC.Framework.Gui.Controls;
using Microsoft.Xna.Framework;

namespace Project_IC.Screens.Menus {
	class MainMenu : MenuScreen {
		#region SetGui
		Button SinglePlayerButton;
		Button MultiPlayerButton;
		Button OptionsButton;
		Button AchievementsButton;
		Button QuitButton;
		Button TestScreenButton;

		protected override void SetGui() {
			Point center = new Point(ScreenManager.Res.X / 2, ScreenManager.Res.Y / 2);
			
			SinglePlayerButton = new Button(center.X - 100, ScreenManager.Res.Y - 250, 200, "Single Player");

			MultiPlayerButton = new Button(center.X - 100, ScreenManager.Res.Y - 200, 200, "Multi Player");

			OptionsButton = new Button(center.X - 100, ScreenManager.Res.Y - 150, 200, "Options");

			AchievementsButton = new Button(center.X - 100, ScreenManager.Res.Y - 100, 200, "Achievements");

			QuitButton = new Button(center.X - 100, ScreenManager.Res.Y - 50, 200, "Quit");
			QuitButton.PrimaryTint = Color.Red;
			QuitButton.LeftClicked += (s, e) => ScreenManager.Game.Exit();

			TestScreenButton = new Button(10, 10, 0, "Test Screen");
			TestScreenButton.LeftClicked += (s, e) => ScreenManager.AddScreen(new TestScreen());

			Gui.BaseScreen.AddControls(SinglePlayerButton, MultiPlayerButton, OptionsButton, AchievementsButton, QuitButton, TestScreenButton);
			
			base.SetGui();
		}
		#endregion
	}
}
