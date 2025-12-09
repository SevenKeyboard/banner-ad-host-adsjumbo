using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BannerAdHost.AdsJumbo
{
    /// <summary>
    /// Configuration for AdsJumbo integration.
    /// This is an example file. Rename it to AdsJumboConfig.cs
    /// and replace the placeholder values with your own.
    /// </summary>
    internal static class AdsJumboConfig
    {
        /// <summary>
        /// Maps an app name to its AdsJumbo application ID.
        /// Application IDs can be found in your AdsJumbo account:
        /// https://adsjumbo.com/account/apps.php
        /// </summary>
        internal static readonly Dictionary<string, string> ApplicationIds =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // Examples:
                ["your_app_name_1"] = "your_app_id_1", // xxxxxxxxxxxx
                ["your_app_name_2"] = "your_app_id_2",
            };

        /// <summary>
        /// List of trusted code-signing certificate thumbprints.
        /// Values must be normalized:
        /// - remove all spaces
        /// - convert to lowercase
        ///
        /// How to find the thumbprint of a signed EXE (Windows GUI):
        /// 1. Right-click the EXE → Properties → "Digital Signatures" tab.
        /// 2. Select the signature → "Details" → "View Certificate".
        /// 3. Open the "Details" tab, select "Thumbprint" and copy it.
        /// 4. Remove spaces and convert to lowercase before adding here.
        ///
        /// PowerShell example:
        ///   Get-AuthenticodeSignature "C:\Path\YourApp.exe" | Select-Object -ExpandProperty SignerCertificate | Select-Object Subject, Thumbprint
        ///
        /// Leave this array empty to disable thumbprint checks.
        /// </summary>
        internal static readonly string[] TrustedThumbprints =
        {
            // Examples (normalized: lowercase, no spaces):
            "your_app_thumbprint_1", // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            "your_app_thumbprint_2",
        };
    }
}