using System.Globalization;
using System.Windows.Forms;

namespace NetHookAnalyzer2
{
	internal static class NetHookItemExtensions
	{
		public static ListViewItem AsListViewItem(this NetHookItem item)
		{
			return new ListViewItem(item.Name)
			{
				Tag = item,
				SubItems = 
				{
					new ListViewItem.ListViewSubItem
					{
						Name = "#",
						Text = item.Sequence.ToString()
					},
					new ListViewItem.ListViewSubItem
					{
						Name = "Timestamp",
						Text = item.Timestamp.ToString(CultureInfo.CurrentUICulture)
					},
					new ListViewItem.ListViewSubItem
					{
						Name = "Message",
						Text = item.Name
					},
					new ListViewItem.ListViewSubItem
					{
						Name = "Inner Message",
						Text = item.InnerMessageName
					}
                }
			};
		}

		public static NetHookItem GetNetHookItem(this ListViewItem item)
		{
			return item.Tag as NetHookItem;
		}
	}
}
