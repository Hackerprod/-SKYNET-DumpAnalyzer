using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SKYNET
{
    public partial class TXTMessage : UserControl
    {
        public TXTMessage()
        {
            InitializeComponent();
        }
        public int Lines
        {
            get { return textBox.Lines.Count(); }
        }
        private void TexBox_TextChanged(object sender, EventArgs e)
        {
            textBox.ForeColor = this.ForeColor;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                lblSearch.Visible = true;
            }
            else
            {
                lblSearch.Visible = false;
            }
            Text = textBox.Text;
            base.OnTextChanged(e);
        }

        private void ClearBox_Click(object sender, EventArgs e)
        {
            textBox.Text = "";
            lblSearch.Visible = true;
        }

        private void TexBox_Enter(object sender, EventArgs e)
        {
        }


        private void TextBox_Leave(object sender, EventArgs e)
        {
        }

        private void LblSearch_Click(object sender, EventArgs e)
        {
            textBox.Focus();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        internal void Clear()
        {
            textBox.Lines = new string[0];
            textBox.Text = string.Empty;

            textBox.Multiline = false;
            textBox.Multiline = true;

        }

        private void TXTMessage_BackColorChanged(object sender, EventArgs e)
        {
            lblSearch.BackColor = this.BackColor;
            textBox.BackColor = this.BackColor;
            panel1.BackColor = this.BackColor;
            panel2.BackColor = this.BackColor;
            panel3.BackColor = this.BackColor;
            panel4.BackColor = this.BackColor;
            panel5.BackColor = this.BackColor;
            panel6.BackColor = this.BackColor;
        }
    }
}
