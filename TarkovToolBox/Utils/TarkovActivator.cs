using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TarkovToolBox.Utils
{
    public static class TarkovActivator
    {

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);


        public static void ActivateTarkov()
        {
            var prc = Process.GetProcessesByName("EscapeFromTarkov");
            if (prc.Length > 0)
                SetForegroundWindow(prc[0].MainWindowHandle);
        }
    }
}
