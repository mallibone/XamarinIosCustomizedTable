using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CustomTableHeader
{
    public partial class CustomTableViewController : UITableViewController
    {
        public CustomTableViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var lines = File.ReadLines("People.txt");
            List<string> peopleNames = new List<string>();
            foreach (var l in lines)
            {
                peopleNames.Add(l);
            }
            peopleNames.Sort((x, y) => { return x.CompareTo(y); });


            var tableSource = new CustomTableViewSource(peopleNames);
            PeopleTableView.Source = tableSource;
        }

        public override bool PrefersStatusBarHidden()
        {
            return true;
        }
    }

    public class CustomTableViewSource : UITableViewSource
    {
        private readonly Dictionary<string, List<string>> _groupedNames;
        string[] _keys;


        public CustomTableViewSource(IList<string> names)
        {
            if (names == null) throw new ArgumentNullException(nameof(names));

            _groupedNames = new Dictionary<string, List<string>>();
            foreach (var name in names)
            {
                if (_groupedNames.ContainsKey(name[0].ToString()))
                {
                    _groupedNames[name[0].ToString()].Add(name);
                }
                else {
                    _groupedNames.Add(name[0].ToString(), new List<string> { name });
                }
            }

            _keys = _groupedNames.Keys.ToArray();
        }

        internal string GetItem(NSIndexPath indexPath)
        {
            if (indexPath == null) return null;
            return _groupedNames[_keys[indexPath.Section]][indexPath.Row];
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _groupedNames[_keys[section]].Count;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return _keys.Length;
        }

        public override String[] SectionIndexTitles(UITableView tableView)
        {
            return _groupedNames.Keys.ToArray();
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            return _keys[section];
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            var cell = (HeaderCell)tableView.DequeueReusableCell("HeaderCell");

            cell.TitleLabel.Text = $"The {TitleForHeader(tableView, section)}'s";

            return cell;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (CursiveTextCell)tableView.DequeueReusableCell("CursiveTextCell");

            cell.CursiveTextLabel.Text = _groupedNames[_keys[indexPath.Section]][indexPath.Row];

            return cell;
        }
    }
}