using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace SkynetChat.Controles
{
    public class StyleTextBox : UserControl
    {
        private IContainer components;
        private string _waterMarkText;

        private TextBox TBox;

        private Label Label1;

        [Category("Appearance")]
        public string WaterMarkText
        {
            get
            {
                return this._waterMarkText;
            }
            set
            {
                this.TBox.Text = value;
                this._waterMarkText = value;
                if (string.IsNullOrEmpty(value))
                    return;
                this.TBox.ForeColor = Color.Gray;
            }
        }

        [Category("Behavior")]
        public string PasswordChar
        {
            get
            {
                return Conversions.ToString(this.TBox.PasswordChar);
            }
            set
            {
                this.TBox.PasswordChar = Conversions.ToChar(value);
            }
        }

        public StyleTextBox()
        {
            this.InitializeComponent();
            TBox.GotFocus += new EventHandler(this.TBox_GotFocus);
            TBox.LostFocus += new EventHandler(this.TBox_LostFocus);

        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!disposing || this.components == null)
                    return;
                this.components.Dispose();
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.TBox = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TBox
            // 
            this.TBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBox.Location = new System.Drawing.Point(15, 5);
            this.TBox.Name = "TBox";
            this.TBox.Size = new System.Drawing.Size(275, 22);
            this.TBox.TabIndex = 91;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(139)))), ((int)(((byte)(202)))));
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(0, 0);
            this.Label1.Margin = new System.Windows.Forms.Padding(0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(6, 31);
            this.Label1.TabIndex = 93;
            this.Label1.Text = " ";
            // 
            // StyleTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.TBox);
            this.Name = "StyleTextBox";
            this.Size = new System.Drawing.Size(290, 31);
            this.Load += new System.EventHandler(this.StyleTextBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void TBox_GotFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.WaterMarkText) || !this.TBox.Text.Equals(this.WaterMarkText))
                return;
            this.TBox.Text = string.Empty;
            this.TBox.ForeColor = Color.Black;
        }

        private void TBox_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.WaterMarkText) || !string.IsNullOrEmpty(this.TBox.Text))
                return;
            this.TBox.Text = this.WaterMarkText;
            this.TBox.ForeColor = Color.Gray;
        }

        private void StyleTextBox_Load(object sender, EventArgs e)
        {

        }
    }
}
