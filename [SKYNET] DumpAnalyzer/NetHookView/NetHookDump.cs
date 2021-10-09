using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetHookAnalyzer2
{
	internal class NetHookDump
	{
		private List<NetHookItem> items;

		public IQueryable<NetHookItem> Items => items.AsQueryable();

		public NetHookDump()
		{
			items = new List<NetHookItem>();
		}

		public void LoadFromDirectory(string directory)
		{
            try
            {
                items.Clear();
                foreach (FileInfo item in new DirectoryInfo(directory).EnumerateFiles("*.bin", SearchOption.AllDirectories))
                {
                    NetHookItem netHookItem = new NetHookItem();
                    if (netHookItem.LoadFromFile(item))
                    {
                        items.Add(netHookItem);
                    }
                }
            }
            catch (System.Exception)
            {
            }
		}

		public NetHookItem AddItemFromPath(string path)
		{
			FileInfo fileInfo = new FileInfo(path);
			NetHookItem netHookItem = new NetHookItem();
			if (!netHookItem.LoadFromFile(fileInfo))
			{
				return null;
			}
			items.Add(netHookItem);
			return netHookItem;
		}

		public NetHookItem RemoveItemWithPath(string path)
		{
			NetHookItem netHookItem = items.SingleOrDefault((NetHookItem x) => x.FileInfo.FullName == path);
			if (netHookItem == null)
			{
				return null;
			}
			items.Remove(netHookItem);
			return netHookItem;
		}
	}
}
