
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
public class FlatTabControl : TabControl
{
    protected override void CreateHandle()
    {
        base.CreateHandle();
    }

    public FlatTabControl()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
        DoubleBuffered = true;
        Font = new Font("Segoe UI", 10f);
        base.SizeMode = TabSizeMode.Fixed;
        base.ItemSize = new Size(120, 40);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Helpers.B = new Bitmap(base.Width, base.Height);
        Helpers.G = Graphics.FromImage(Helpers.B);
        Graphics g = Helpers.G;
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        g.Clear(Color.FromArgb(14, 22, 33));
        try
        {
            base.SelectedTab.BackColor = Color.FromArgb(14, 22, 33);
        }
        catch (Exception projectError)
        {
            ProjectData.SetProjectError(projectError);
            ProjectData.ClearProjectError();
        }
        checked
        {
            g.FillRectangle(new SolidBrush(ColorSystem.BackLight), new Rectangle(2, 0, base.Width - 4, base.ItemSize.Height + 4));
        }
        g = null;
        base.OnPaint(e);
        Helpers.G.Dispose();
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        e.Graphics.DrawImageUnscaled(Helpers.B, 0, 0);
        Helpers.B.Dispose();
    }
}
