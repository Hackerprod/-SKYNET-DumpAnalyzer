using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class modCommon
{
    public static void Show(object msg)
    {
        MessageBox.Show(msg.ToString());
    }
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
    private const int WM_VSCROLL = 277;
    private const int SB_PAGEBOTTOM = 7;

    public static bool Hackerprod { get { return Environment.UserName == "Hackerprod"; } }

    public static void ScrollToBottom(RichTextBox MyRichTextBox)
    {
        SendMessage(MyRichTextBox.Handle, WM_VSCROLL, (IntPtr)SB_PAGEBOTTOM, IntPtr.Zero);
    }
    public static string GetPath()
    {
        Process currentProcess;
        try
        {
            currentProcess = Process.GetCurrentProcess();
            return new FileInfo(currentProcess.MainModule.FileName).Directory?.FullName;
        }
        finally { currentProcess = null; }
    }
    public static void EnsureDirectoryExists(string filePath, bool isFileName = false)
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            filePath = filePath.Trim().Replace("\0", string.Empty);
            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    string text = isFileName ? Path.GetDirectoryName(filePath) : filePath;
                    if (Path.IsPathRooted(filePath))
                    {
                        text = text.Trim();
                        if (!Directory.Exists(text))
                        {
                            try
                            {
                                Directory.CreateDirectory(text);
                            }
                            catch { }
                        }
                    }
                }
                catch (Exception exception)
                {

                }
            }
        }
    }

}
