# banner-ad-host-adsjumbo
Displays an AdsJumbo banner window for a calling application.
It is designed to be easily embedded as a child window inside another application.

To use it with your own [AdsJumbo](https://adsjumbo.com/) account, copy `AdsJumboConfig.example.cs` to `AdsJumboConfig.cs` and fill in your application IDs and trusted certificate thumbprints.

```csharp
internal static readonly Dictionary<string, string> ApplicationIds =
    new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["your_app_name_1"] = "your_app_id_1",
        ["your_app_name_2"] = "your_app_id_2",
    };

internal static readonly string[] TrustedThumbprints =
{
    "your_app_thumbprint_1",
    "your_app_thumbprint_2",
};
```

> **Disclaimer:** This repository is provided **for reference only** and **as-is**. Functionality is not guaranteed and may change or break due to third-party dependencies, upstream changes, or configuration differences. **The author is not responsible for any damages or issues resulting from its use.**
