using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BannerAdHost.AdsJumbo
{
    internal class AppInfo
    {
        internal class ClientSize
        {
            internal static int Width { get; set; } = -1;
            internal static int Height { get; set; } = -1;
            // If you want to crop the overall client size based on a two-line ad for a 300x600 ad size, set it to 178. If you prefer to use a three-line ad as a reference, then set it to 190.
        }
        internal class Banner
        {
            internal static string ApplicationId { get; set; } = "your_app_id";
            internal class Size
            {
                internal static int Width { get; set; } = -1;
                internal static int Height { get; set; } = -1;
            }
        }
        internal class VerticalScroll
        {
            internal static bool Visible = true;
        }
        internal class HorizontalScroll
        {
            internal static bool Visible = true;
        }
        internal static bool DpiScaleMode = true;
        internal static bool AutoScroll = false;
        internal static string Text = System.AppDomain.CurrentDomain.FriendlyName;
    }
}
