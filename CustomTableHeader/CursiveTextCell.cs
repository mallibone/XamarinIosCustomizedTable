using Foundation;
using System;
using UIKit;

namespace CustomTableHeader
{
    public partial class CursiveTextCell : UITableViewCell
    {
        public UILabel CursiveTextLabel => this.CursiveLabel;
        public CursiveTextCell (IntPtr handle) : base (handle)
        {
        }
    }
}