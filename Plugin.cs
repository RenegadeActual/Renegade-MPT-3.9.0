using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using EFT.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using Renegade.Core.UI;

namespace Renegade.Core;

[BepInPlugin("com.renegade.core", "Renegade.Core", MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("EscapeFromTarkov.exe")]
[BepInDependency("com.SPT.custom", BepInDependency.DependencyFlags.HardDependency)] // This is used so that we guarantee to load after spt-custom, that way we can disable its patches
[BepInDependency("com.SPT.singleplayer", BepInDependency.DependencyFlags.HardDependency)] // This is used so that we guarantee to load after spt-singleplayer, that way we can disable its patches
[BepInDependency("com.SPT.core", BepInDependency.DependencyFlags.HardDependency)] // This is used so that we guarantee to load after spt-core, that way we can disable its patches
[BepInDependency("com.SPT.debugging", BepInDependency.DependencyFlags.HardDependency)] // This is used so that we guarantee to load after spt-debugging, that way we can disable its patches
// Hard Dependencies for AKI here to ensure we can overwrite the patches (after AKI 3.9.0 comes out of Alpha
public class RenegadePlugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
    public static RenegadePlugin Instance;
    public static string EFTVersionMajor { get; internal set; }

    protected void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"===================RENEGADE.CORE {MyPluginInfo.PLUGIN_VERSION}=======================");
        Logger.LogInfo($"Renegade.Core {MyPluginInfo.PLUGIN_VERSION} is loaded!");
        Logger.LogInfo($"Please report any bugs on the Github and be sure to include the logs.");
        Logger.LogInfo($"======================================================");

        new RenegadeVersionLabel_Patch().Enable();
        // TOS_Patch
        // Trader_Patch
        // Music_Patch
        // SplashScreen_Patch
    }
}
