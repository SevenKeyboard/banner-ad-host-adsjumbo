using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BannerAdHost.AdsJumbo
{
    internal class WinAPIConstants
    {
        internal const int DPI_AWARENESS_CONTEXT_DEFAULT = 0
            , DPI_AWARENESS_CONTEXT_UNAWARE = -1
            , DPI_AWARENESS_CONTEXT_SYSTEM_AWARE = -2
            , DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE = -3
            , DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = -4
            , DPI_AWARENESS_CONTEXT_UNAWARE_GDISCALED = -5
            , EVENT_OBJECT_DESTROY = 0x8001
            , MAX_PATH = 260
            , MDT_EFFECTIVE_DPI = 0
            , MDT_ANGULAR_DPI = 1
            , MDT_RAW_DPI = 2
            , MDT_DEFAULT = MDT_EFFECTIVE_DPI
            , MONITOR_DEFAULTTOPRIMARY = 0x00000001
            , PROCESS_DPI_UNAWARE = 0
            , PROCESS_SYSTEM_DPI_AWARE = 1
            , PROCESS_PER_MONITOR_DPI_AWARE = 2
            , PROCESS_VM_READ = 0x0010
            , PROCESS_QUERY_INFORMATION = 0x0400
            , WINEVENT_OUTOFCONTEXT = 0
            , WS_BORDER = 0x00800000
            , WS_CAPTION = 0x00C00000
            , WS_EX_NOACTIVATE = 0x08000000;
    }
}
