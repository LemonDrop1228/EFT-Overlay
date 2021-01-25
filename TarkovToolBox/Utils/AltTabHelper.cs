using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace TarkovToolBox.Utils
{
    public static class AltTabHelper 
    {

        internal const int GwlExstyle = -20;
        internal const int WsExToolwindow = 0x80;

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        private static extern IntPtr GetWindowLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        private static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, uint value);

        internal static void RemoveFromAltTab(IntPtr hwnd)
        {
            uint ws = (uint)GetWindowLong32(hwnd, GwlExstyle).ToInt64();
            SetWindowLong(hwnd, GwlExstyle, ws | WsExToolwindow);
        }

        internal static void RemoveFromAltTab(Window w)
        {
            RemoveFromAltTab((new WindowInteropHelper(w)).Handle);
        }

    }
}
