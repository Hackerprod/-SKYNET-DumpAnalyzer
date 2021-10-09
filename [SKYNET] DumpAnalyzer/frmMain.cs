using NetHookAnalyzer2.Implementations;
using ProtoBuf;
using ProtoBuf.Meta;
using SKYNET;
using SKYNET.Steam;
using SteamKit2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetHookAnalyzer2
{
    public partial class frmMain : Form
    {
        public static frmMain frm;
        public static string CurrentDump;
        public frmMain()
        {
            InitializeComponent();

            frm = this;
            CheckForIllegalCrossThreadCalls = false;
            Dump = new NetHookDump();
            selectedListViewItem = null;
            RepopulateInterface();
        }
        private NetHookDump Dump
        {
            get;
            set;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 495;

            if (modCommon.Hackerprod)
            {
                Search_Folder.Text = @"D:\Regueros\Compartido\7.26CPLUS\Dumps\StickM4N Dumps";
                Search_Folder.Text = @"C:\Steam\nethook";

                searchTextBox.Text = @"";
            }
        }

        private void OnItemsListViewSelectedIndexChanged(object sender, EventArgs e)
        {

            if (itemsListView.SelectedItems.Count == 1)
            {
                ListViewItem listViewItem = itemsListView.SelectedItems[0];
                if (listViewItem != selectedListViewItem)
                {
                    selectedListViewItem = listViewItem;
                    NetHookItem netHookItem = selectedListViewItem.GetNetHookItem();
                    CurrentDump = netHookItem.FileInfo.FullName;
                    GenerateCode(false);
                }
            }
        }

        private void ItemsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GenerateCode(true);
        }

        private void GenerateCode(bool External)
        {
            byte[] fileBytes = File.ReadAllBytes(CurrentDump);

            IPacketMsg PacketMsg = GetPacketMsg(fileBytes);
            EMsg msg = MsgUtil.GetMsg(PacketMsg.MsgType);
            if (msg != EMsg.k_EMsgClientToGC && msg != EMsg.k_EMsgClientFromGC)
            {
                return;
            }

            ClientMsgProtobuf<CMsgGCClient> clientMsgProtobuf = new ClientMsgProtobuf<CMsgGCClient>(PacketMsg);

            string Code = MsgHelpers_Formated.GetReflectedMsg(clientMsgProtobuf.Body);
            if (!string.IsNullOrEmpty(Code))
            {
                if (External)
                {
                    new frmCode(Code).ShowDialog();
                }
                else
                    webBrowser1.DocumentText = Code;
            }

        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            RepopulateListBox();
        }

        private void OnSearchTextBoxEnter(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "Search...")
            {
                searchTextBox.Text = string.Empty;
                ((Control)searchTextBox).ForeColor = (SearchTextBoxUserTextColor);
            }
            else
            {
                searchTextBox.SelectAll();
            }
        }

        private void OnSearchTextBoxLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                searchTextBox.Text = "Search...";
                ((Control)searchTextBox).ForeColor = (SearchTextBoxPlaceholderColor);
            }
        }
        private void RepopulateInterface()
        {
            RepopulateListBox();
        }
        private void RepopulateListBox()
        {
            string searchTerm = searchTextBox.Text;
            Expression<Func<NetHookItem, bool>> predicate = (!(searchTerm == "Search...") && !string.IsNullOrWhiteSpace(searchTerm)) ? ((Expression<Func<NetHookItem, bool>>)((NetHookItem nhi) => nhi.Name.IndexOf(searchTerm, StringComparison.InvariantCultureIgnoreCase) >= 0 || (nhi.InnerMessageName != (string)null && nhi.InnerMessageName.IndexOf(searchTerm, StringComparison.InvariantCultureIgnoreCase) >= 0))) : ((Expression<Func<NetHookItem, bool>>)((NetHookItem nhi) => true));
            Expression<Func<NetHookItem, bool>> predicate2 = (NetHookItem nhi) => ((int)nhi.Direction == 1 && true) || ((int)nhi.Direction == 0 && true);
            Expression<Func<NetHookItem, bool>> OnlyGC = (NetHookItem nhi) => (nhi.EMsg == EMsg.k_EMsgClientFromGC || nhi.EMsg == EMsg.k_EMsgClientToGC);
            IQueryable<ListViewItem> source = from x in Dump.Items.Where(predicate2)
                                                                  .Where(predicate)
                                                                  .Where(OnlyGC)
                                              select x.AsListViewItem();
            itemsListView.Items.Clear();
            itemsListView.Items.AddRange(source.ToArray());

        }
        private void OpenDumps_Click(object sender, EventArgs e)
        {
            if (modCommon.Hackerprod)
            {
                //searchTextBox.Text = "NotificationsResponse";
            }
            string selectedPath = Search_Folder.Text;
            NetHookDump netHookDump = new NetHookDump();
            netHookDump.LoadFromDirectory(selectedPath);

            Dump = netHookDump;
            Text = $"[SKYNET] Dump Analyzer - [{selectedPath}]";
            selectedListViewItem = null;
            RepopulateInterface();
            if (itemsListView.Items.Count > 0)
            {
                itemsListView.Select();
                itemsListView.Items[0].Selected = true;
            }
        }
        public IPacketMsg GetPacketMsg(byte[] data)
        {
            if (data.Length < 4)
            {
                modCommon.Show(string.Format("PacketMsg too small to contain a message, was only {0} bytes. Message: 0x{1}", data.Length, BitConverter.ToString(data).Replace("-", string.Empty)));
                return null;
            }
            uint msg = BitConverter.ToUInt32(data, 0);
            EMsg msg2 = MsgUtil.GetMsg(msg);
            if ((uint)(msg2 - 1303) <= 2u)
            {
                return new PacketMsg(msg2, new MemoryStream(data));
            }
            try
            {
                if (MsgUtil.IsProtoBuf(msg))
                {
                    return new PacketClientMsgProtobuf(msg2, data);
                }
                return new PacketClientMsg(msg2, new MemoryStream(data));
            }
            catch (Exception ex)
            {
                return null;
            }

        }


    }
}
