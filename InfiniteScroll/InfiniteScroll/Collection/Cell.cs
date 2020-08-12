using System;

using Foundation;
using UIKit;

namespace InfiniteScroll.Collection
{
    public partial class Cell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("Cell");
        public static readonly UINib Nib;

        static Cell()
        {
            Nib = UINib.FromName("Cell", NSBundle.MainBundle);
        }

        public void SetupCell(int number)
        {
            _lblTitle.Text = number.ToString();
        }

        protected Cell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
