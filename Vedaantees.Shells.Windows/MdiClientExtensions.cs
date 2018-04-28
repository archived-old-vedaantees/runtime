using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Vedaantees.Shells.Windows
{
    public static class MdiClientExtensions
    {
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        private const int GwlExstyle = -20;
        private const int WsExClientedge = 0x200;
        private const uint SwpNosize = 0x0001;
        private const uint SwpNomove = 0x0002;
        private const uint SwpNozorder = 0x0004;
        private const uint SwpNoactivate = 0x0010;
        private const uint SwpFramechanged = 0x0020;
        private const uint SwpNoownerzorder = 0x0200;

        public static bool SetBevel(this Form form, bool show)
        {

            var client = form.Controls.OfType<MdiClient>().FirstOrDefault();
            if (client == null) return false;
            var windowLong = GetWindowLong(client.Handle, GwlExstyle);

            if (show)
                windowLong |= WsExClientedge;
            else
                windowLong &= ~WsExClientedge;
                
            SetWindowLong(client.Handle, GwlExstyle, windowLong);
            SetWindowPos(client.Handle, IntPtr.Zero, 0, 0, 0, 0, SwpNoactivate | SwpNomove | SwpNosize | SwpNozorder | SwpNoownerzorder | SwpFramechanged);
            return true;
        }

    }
}