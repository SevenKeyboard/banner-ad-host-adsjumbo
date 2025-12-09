using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BannerAdHost.AdsJumbo
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT
    {
        internal int X;
        internal int Y;
        internal POINT(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    internal class DllCall
    {
        internal class Kernel32
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool CloseHandle(IntPtr hObject);

            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);
        }

        internal class Psapi
        {
            [DllImport("Psapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            internal static extern uint GetModuleFileNameExW(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, uint nSize);
        }

        internal class Shcore
        {
            [DllImport("Shcore.dll", SetLastError = true)]
            internal static extern int GetDpiForMonitor(IntPtr hmonitor, int dpiType, out uint dpiX, out uint dpiY);

            [DllImport("Shcore.dll", SetLastError = true)]
            internal static extern int GetProcessDpiAwareness(IntPtr hprocess, out int value);

            [DllImport("Shcore.dll", SetLastError = true)]
            internal static extern int GetScaleFactorForMonitor(IntPtr hMon, out int pScale);

            [DllImport("Shcore.dll", SetLastError = true)]
            internal static extern int SetProcessDpiAwareness(int value);
        }

        internal class User32
        {
            [DllImport("User32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool AreDpiAwarenessContextsEqual(int dpiContextA, int dpiContextB);

            [DllImport("User32.dll", SetLastError = true)]
            internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [DllImport("User32.dll", SetLastError = true)]
            internal static extern int GetThreadDpiAwarenessContext();

            [DllImport("User32.dll", SetLastError = true)]
            internal static extern int GetWindowDpiAwarenessContext(IntPtr hWnd);

            [DllImport("User32.dll", SetLastError = true)]
            internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out IntPtr lpdwProcessId);

            [DllImport("User32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool IsWindow(IntPtr hWnd);

            [DllImport("User32.dll", SetLastError = true)]
            internal static extern IntPtr MonitorFromPoint(POINT pt, int dwFlags);

            [DllImport("User32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool SetProcessDpiAwarenessContext(int value);

            [DllImport("User32.dll", SetLastError = true)]
            internal static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate pfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);
            internal delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint idEventThread, uint dwmsEventTime);

            [DllImport("User32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool UnhookWinEvent(IntPtr hWinEventHook);
        }
    }
}
