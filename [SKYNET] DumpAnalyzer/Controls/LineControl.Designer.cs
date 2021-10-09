namespace SKYNET.Controls
{
    partial class LineControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Time = new System.Windows.Forms.Label();
            this.MessageType = new System.Windows.Forms.Label();
            this.Message = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Time
            // 
            this.Time.AutoSize = true;
            this.Time.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Time.Location = new System.Drawing.Point(-3, -1);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(12, 15);
            this.Time.TabIndex = 0;
            this.Time.Text = "T";
            // 
            // MessageType
            // 
            this.MessageType.AutoSize = true;
            this.MessageType.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageType.Location = new System.Drawing.Point(90, -1);
            this.MessageType.Name = "MessageType";
            this.MessageType.Size = new System.Drawing.Size(24, 15);
            this.MessageType.TabIndex = 1;
            this.MessageType.Text = "MT";
            // 
            // Message
            // 
            this.Message.AutoSize = true;
            this.Message.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Message.Location = new System.Drawing.Point(142, -1);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(17, 15);
            this.Message.TabIndex = 2;
            this.Message.Text = "M";
            // 
            // LineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.Message);
            this.Controls.Add(this.MessageType);
            this.Controls.Add(this.Time);
            this.Name = "LineControl";
            this.Size = new System.Drawing.Size(466, 15);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label Time;
        public System.Windows.Forms.Label MessageType;
        public System.Windows.Forms.Label Message;
    }
}
