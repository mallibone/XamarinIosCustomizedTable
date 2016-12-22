using Foundation;
using System;
using UIKit;

namespace CustomTableHeader
{
    public partial class HeaderCell : UITableViewCell
    {
        public UILabel TitleLabel => this.Title;
        public HeaderCell (IntPtr handle) : base (handle)
        {
        }
    }
}