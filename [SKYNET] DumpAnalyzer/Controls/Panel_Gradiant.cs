using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    class Panel_Gradiant : Panel
    {
        protected override void OnPaint(PaintEventArgs e)
        {

        }
        public Panel_Gradiant()
        {
            PaintGradiant();
        }
        protected void PaintGradiant()
        {
            Color c1 = Color.FromArgb(35, 46, 60);
            Color c2 = Color.FromArgb(14, 22, 33);

            LinearGradientBrush gradBrush = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), c1, c2, 0f);


            Bitmap bmp = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(gradBrush, new Rectangle(0, 0, Width, Height));
            BackgroundImage = bmp;
            BackgroundImageLayout = ImageLayout.Stretch;

        }

    }
}
