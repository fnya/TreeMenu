using System;
using UIKit;
using Xamarin.SideMenu;

namespace TreeMenu
{

    class SideMenuSample : UIViewController
    {
        SideMenuManager _sideMenuManager;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationItem.SetLeftBarButtonItem(
                 new UIBarButtonItem("Left Menu", UIBarButtonItemStyle.Plain, (sender, e) =>
                 {
                     PresentViewController(_sideMenuManager.LeftNavigationController, true, null);
                 }),
                false);

            _sideMenuManager = new SideMenuManager();

            _sideMenuManager.PresentMode = (SideMenuManager.MenuPresentMode)0;//Slide In

            View.BackgroundColor = UIColor.White;
            Title = "Tree Menu";


            SetupSideMenu();

            SetDefaults();
        }

        void SetupSideMenu()
        {
            _sideMenuManager.LeftNavigationController = new UISideMenuNavigationController(_sideMenuManager, new SampleTableView(), leftSide: true);

            _sideMenuManager.AddScreenEdgePanGesturesToPresent(toView: this.NavigationController?.View);

        }

        void SetDefaults()
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
