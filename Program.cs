using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BannerAdHost.AdsJumbo
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            if (!InitializePropertiesFromArguments(ref args))
                return 1;
            
            callingProcessExistTimer = new System.Timers.Timer(10037);
            callingProcessExistTimer.Elapsed += OnTimedEvent;
            callingProcessExistTimer.AutoReset = true;
            callingProcessExistTimer.Enabled = true;
             
            deleWinEventProc = new DllCall.User32.WinEventDelegate(WinEventProc);
            hWinEventHook = DllCall.User32.SetWinEventHook(WinAPIConstants.EVENT_OBJECT_DESTROY
                                            , WinAPIConstants.EVENT_OBJECT_DESTROY
                                            , IntPtr.Zero
                                            , deleWinEventProc
                                            , CallingProcessInfo.PID
                                            , 0
                                            , WinAPIConstants.WINEVENT_OUTOFCONTEXT);
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            return 0;
        }
        static void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint idEventThread, uint dwmsEventTime)
        {
            if (eventType == WinAPIConstants.EVENT_OBJECT_DESTROY && hwnd.ToInt32() == CallingProcessInfo.Hwnd.ToInt32())
                Application.Exit();
        }
        private static System.Timers.Timer callingProcessExistTimer;
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (!DllCall.User32.IsWindow(CallingProcessInfo.Hwnd))
                Application.Exit();
        }
        static void OnApplicationExit(object sender, EventArgs e)
        {
            if (hWinEventHook != IntPtr.Zero)
                DllCall.User32.UnhookWinEvent(hWinEventHook);
        }
        static bool InitializePropertiesFromArguments(ref string[] args)
        {
            if (args.Length < 5)
                return false;
            // 1. Get parent gui's hWnd.
            IntPtr intptrHwnd = IntPtr.Zero;
            string strHwnd = args[0];
            try
            {
                intptrHwnd = strHwnd.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
                    ? (IntPtr)Convert.ToInt64(strHwnd.Substring(2), 16)
                    : (IntPtr)Int64.Parse(strHwnd);
                if (intptrHwnd == IntPtr.Zero || !DllCall.User32.IsWindow(intptrHwnd))
                    return false;
            }
            catch { return false; }
            CallingProcessInfo.Hwnd = intptrHwnd;
            if (DllCall.User32.GetWindowThreadProcessId(CallingProcessInfo.Hwnd, out IntPtr processId) != 0)
            {
                CallingProcessInfo.PID = (uint)processId;
                CallingProcessInfo.Path = GetProcessPath(CallingProcessInfo.PID);
            }
            else
            {
                return false;
            }
            // 2. App Name that is pre-registered with Ads Jumbo
            string appName = args[1];
            if (AdsJumboConfig.ApplicationIds.TryGetValue(appName, out string applicationId)
                && !string.IsNullOrWhiteSpace(applicationId))
            {
                AppInfo.Banner.ApplicationId = applicationId;
            }
            bool isTrustedSigner = false;
            if (AdsJumboConfig.TrustedThumbprints.Length > 0)
            {
                try
                {
                    using (var cert = new X509Certificate2(CallingProcessInfo.Path))
                    {
                        string thumbprint = cert.Thumbprint.ToLowerInvariant();
                        foreach (string trustedThumbprint in AdsJumboConfig.TrustedThumbprints)
                        {
                            if (thumbprint == trustedThumbprint)
                            {
                                isTrustedSigner = true;
                                break;
                            }
                        }
                    }
                }
                catch
                {
                    // treat as untrusted
                }
                if (!isTrustedSigner)
                    AppInfo.Banner.ApplicationId = "your_app_id";
            }
            // 3. Title
            if (!string.IsNullOrEmpty(args[2]))
                AppInfo.Text = args[2];
            // 4. Form.Width + "x" + Form.Height
            // 5. Form.bannerAd.Width + "x" + Form.bannerAd.Height
            if (!GetSizeFromSizeString(args[3], out int temp1, out int temp2)
            || !GetSizeFromSizeString(args[4], out int temp3, out int temp4))
                return false;
            AppInfo.ClientSize.Width = temp1;
            AppInfo.ClientSize.Height = temp2;
            AppInfo.Banner.Size.Width = temp3;
            AppInfo.Banner.Size.Height = temp4;
            // 6. [Options]
            if (6 <= args.Length)
            {
                Match match;
                match = Regex.Match(args[5], @"(?:^|\s)(\+|-|)DpiScaleMode\b", RegexOptions.IgnoreCase);
                if (match.Success)
                    AppInfo.DpiScaleMode = !(match.Groups[1].Value == "-");
                match = Regex.Match(args[5], @"(?:^|\s)(\+|-|)AutoScroll\b", RegexOptions.IgnoreCase);
                if (match.Success)
                    AppInfo.AutoScroll = !(match.Groups[1].Value == "-");
                match = Regex.Match(args[5], @"(?:^|\s)(\+|-|)VerticalScrollVisible\b", RegexOptions.IgnoreCase);
                if (match.Success)
                    AppInfo.VerticalScroll.Visible = !(match.Groups[1].Value == "-");
                match = Regex.Match(args[5], @"(?:^|\s)(\+|-|)HorizontalScrollVisible\b", RegexOptions.IgnoreCase);
                if (match.Success)
                    AppInfo.HorizontalScroll.Visible = !(match.Groups[1].Value == "-");
            }
            return true;
            //
            bool GetSizeFromSizeString(string input, out int width, out int height)
            {
                int widthPart = -1, heightPart = -1;
                string[] parts = input.Split('x');
                if (parts.Length == 2
                && int.TryParse(parts[0], out widthPart) && 0 <= widthPart
                && int.TryParse(parts[1], out heightPart) && 0 <= heightPart)
                {
                    width = widthPart; height = heightPart;
                    return true;
                }
                width = height = -1;
                return false;
            }
            //
            string GetProcessPath(uint PID)
            {
                string executablePath = string.Empty;
                IntPtr processHandle = DllCall.Kernel32.OpenProcess(WinAPIConstants.PROCESS_QUERY_INFORMATION | WinAPIConstants.PROCESS_VM_READ, false, PID);
                if (processHandle != IntPtr.Zero)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder(WinAPIConstants.MAX_PATH);
                        uint length = DllCall.Psapi.GetModuleFileNameExW(processHandle, IntPtr.Zero, sb, (uint)sb.Capacity);
                        if (length > 0)
                            executablePath = sb.ToString(0, (int)length);
                    }
                    finally
                    {
                        DllCall.Kernel32.CloseHandle(processHandle);
                    }
                }
                return executablePath;
            }
        }
        static DllCall.User32.WinEventDelegate deleWinEventProc = null;
        static IntPtr hWinEventHook = IntPtr.Zero;
    }
}
