using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BannerAdHost.AdsJumbo
{
    internal class CallingProcessInfo
    {
        internal static IntPtr Hwnd { get; set; } = IntPtr.Zero;
        internal static uint PID { get; set; } = 0;
        internal static string Path { get; set; } = string.Empty;
    }
}
