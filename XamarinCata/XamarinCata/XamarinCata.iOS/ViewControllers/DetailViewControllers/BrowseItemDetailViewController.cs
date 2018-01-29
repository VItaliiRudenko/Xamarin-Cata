using System;
using UIKit;

using XamarinCata.ViewModel;

namespace XamarinCata.iOS
{
    public partial class BrowseItemDetailViewController : UIViewController
    {
		public ItemDetailViewModel ViewModel { get; set; }
		public BrowseItemDetailViewController(IntPtr handle) : base(handle) { }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = ViewModel.Title;
			ItemNameLabel.Text = ViewModel.Item.Text;
			ItemDescriptionLabel.Text = ViewModel.Item.Description;

		}


    }
}