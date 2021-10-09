using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NetHookAnalyzer2
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemsListView = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Search_Folder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.OpenDumps = new FlatButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // itemsListView
            // 
            this.itemsListView.AllowDrop = true;
            this.itemsListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.itemsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader,
            this.columnHeader6,
            this.columnHeader3,
            this.columnHeader4});
            this.itemsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemsListView.FullRowSelect = true;
            this.itemsListView.GridLines = true;
            this.itemsListView.HideSelection = false;
            this.itemsListView.Location = new System.Drawing.Point(0, 55);
            this.itemsListView.MultiSelect = false;
            this.itemsListView.Name = "itemsListView";
            this.itemsListView.Size = new System.Drawing.Size(425, 486);
            this.itemsListView.TabIndex = 0;
            this.itemsListView.UseCompatibleStateImageBehavior = false;
            this.itemsListView.View = System.Windows.Forms.View.Details;
            this.itemsListView.SelectedIndexChanged += new System.EventHandler(this.OnItemsListViewSelectedIndexChanged);
            this.itemsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ItemsListView_MouseDoubleClick);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "";
            this.columnHeader5.Width = 0;
            // 
            // columnHeader
            // 
            this.columnHeader.Text = "#";
            this.columnHeader.Width = 39;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Timestamp";
            this.columnHeader6.Width = 123;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Message";
            this.columnHeader3.Width = 82;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Inner Message";
            this.columnHeader4.Width = 228;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.searchTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(425, 55);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Search";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.OpenDumps);
            this.panel2.Controls.Add(this.Search_Folder);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(425, 24);
            this.panel2.TabIndex = 5;
            // 
            // Search_Folder
            // 
            this.Search_Folder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Search_Folder.Location = new System.Drawing.Point(50, 2);
            this.Search_Folder.Name = "Search_Folder";
            this.Search_Folder.Size = new System.Drawing.Size(265, 20);
            this.Search_Folder.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Path:";
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.ForeColor = System.Drawing.Color.LightGray;
            this.searchTextBox.Location = new System.Drawing.Point(50, 29);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(265, 20);
            this.searchTextBox.TabIndex = 1;
            this.searchTextBox.Text = "Search...";
            this.searchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            this.searchTextBox.Enter += new System.EventHandler(this.OnSearchTextBoxEnter);
            this.searchTextBox.Leave += new System.EventHandler(this.OnSearchTextBoxLeave);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(571, 541);
            this.webBrowser1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.itemsListView);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.webBrowser1);
            this.splitContainer1.Size = new System.Drawing.Size(998, 541);
            this.splitContainer1.SplitterDistance = 425;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 3;
            // 
            // b_Import
            // 
            this.OpenDumps.BackColor = System.Drawing.Color.DarkGray;
            this.OpenDumps.BackColorMouseOver = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(61)))), ((int)(((byte)(75)))));
            this.OpenDumps.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OpenDumps.Dock = System.Windows.Forms.DockStyle.Right;
            this.OpenDumps.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.OpenDumps.ForeColor = System.Drawing.Color.White;
            this.OpenDumps.ForeColorMouseOver = System.Drawing.Color.Empty;
            this.OpenDumps.ImageAlignment = FlatButton._ImgAlign.Left;
            this.OpenDumps.ImageIcon = null;
            this.OpenDumps.Location = new System.Drawing.Point(319, 0);
            this.OpenDumps.Name = "b_Import";
            this.OpenDumps.Rounded = false;
            this.OpenDumps.Size = new System.Drawing.Size(106, 24);
            this.OpenDumps.Style = FlatButton._Style.TextOnly;
            this.OpenDumps.TabIndex = 27;
            this.OpenDumps.Text = "Open";
            this.OpenDumps.Click += new System.EventHandler(this.OpenDumps_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(998, 541);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NetHook2 Dump Analyzer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private const string SearchTextBoxPlaceholderText = "Search...";

        private Color SearchTextBoxPlaceholderColor = Color.LightGray;

        private Color SearchTextBoxUserTextColor = Color.Black;

        private ListViewItem selectedListViewItem;

        private ContextMenuStrip contextMenuStrip1;

        private TextBox searchTextBox;

        private ListView itemsListView;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private Panel panel1;
        private TextBox Search_Folder;
        private Label label2;
        private Panel panel2;
        private FlatButton OpenDumps;
        private Label label1;
        private WebBrowser webBrowser1;
        private SplitContainer splitContainer1;
    }
}