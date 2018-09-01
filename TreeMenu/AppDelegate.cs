using Foundation;
using UIKit;

namespace TreeMenu
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // create a new window instance based on the screen size
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            var spacePurple = UIColor.FromRGB(64, 0, 128);//Windowのカラー変更

            UIView.Appearance.TintColor = UIColor.White;//ナビゲーションのテキストカラー変更
            UISlider.Appearance.ThumbTintColor = spacePurple;
            UISwitch.Appearance.TintColor = spacePurple;
            UISwitch.Appearance.OnTintColor = spacePurple;
            UINavigationBar.Appearance.BarTintColor = spacePurple;
            UINavigationBar.Appearance.TintColor = UIColor.White;

            var controller = new UIViewController();
            controller.View.BackgroundColor = UIColor.LightGray;
            controller.Title = "My Controller";

            var cvc = new SideMenuSample();

            var navController = new UINavigationController(cvc) { };
            navController.NavigationBar.BarStyle = UIBarStyle.Black;

            Window.RootViewController = navController;

            // make the window visible
            Window.MakeKeyAndVisible();

            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
        }

        public override void DidEnterBackground(UIApplication application)
        {
        }

        public override void WillEnterForeground(UIApplication application)
        {
        }

        public override void OnActivated(UIApplication application)
        {
        }

        public override void WillTerminate(UIApplication application)
        {
        }
    }
}

