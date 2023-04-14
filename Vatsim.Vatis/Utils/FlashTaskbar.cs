using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Vatsim.Vatis.Utils;

public static class FlashTaskbar
{
    public static void FlashWindow()
    {
        Task.Factory.StartNew(() =>
        {
            FLASHWINFO pwfi = FLASHWINFO.Create();
            pwfi.hwnd = Process.GetCurrentProcess().MainWindowHandle;
            pwfi.dwFlags = FlashWindowFlags.FLASHW_ALL | FlashWindowFlags.FLASHW_TIMERNOFG;
            pwfi.uCount = int.MaxValue;
            pwfi.dwTimeout = 0;
            FlashWindowEx(ref pwfi);
        });
    }

    private enum FlashWindowFlags
    {
        FLASHW_STOP = 0,
        FLASHW_CAPTION = 1,
        FLASHW_TRAY = 2,
        FLASHW_ALL = 3,
        FLASHW_TIMER = 4,
        FLASHW_TIMERNOFG = 12
    }

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct FLASHWINFO
    {
        public int cbSize;
        public IntPtr hwnd;
        public FlashWindowFlags dwFlags;
        public int uCount;
        public int dwTimeout;

        public static FLASHWINFO Create()
        {
            return new FLASHWINFO()
            {
                cbSize = Marshal.SizeOf(typeof(FLASHWINFO))
            };
        }
    }
}