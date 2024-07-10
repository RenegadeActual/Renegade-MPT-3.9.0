using EFT.UI;
using System.Reflection;
using SPT.Reflection.Patching;
using HarmonyLib;
using SPT.Common.Http;
using SPT.Common.Utils;
using SPT.Custom.Models;

namespace Renegade.Core.UI
{
    public class RenegadeVersionLabel_Patch : ModulePatch
    {
        private static string versionLabel;
        private static Traverse versionNumberTraverse;
        private static string renegadeVersion;
        private static string officialVersion;

        protected override MethodBase GetTargetMethod()
        {
            return typeof(VersionNumberClass).GetMethod(nameof(VersionNumberClass.Create), BindingFlags.Static | BindingFlags.Public);
        }

        [PatchPostfix]
        internal static void PatchPostfix(string major, object __result)
        {
            RenegadePlugin.EFTVersionMajor = major;

            if (string.IsNullOrEmpty(versionLabel))
            {
                string json = RequestHandler.GetJson("/singleplayer/settings/version");
                versionLabel = "Renegade-MPT";
                Logger.LogInfo($"Server version: {versionLabel}");
            }

            renegadeVersion = MyPluginInfo.PLUGIN_VERSION;
            versionLabel = "Renegade-MPT";

            Traverse preloaderUiTraverse = Traverse.Create(MonoBehaviourSingleton<PreloaderUI>.Instance);

            preloaderUiTraverse.Field("_alphaVersionLabel").Property("LocalizationKey").SetValue("{0}");

            versionNumberTraverse = Traverse.Create(__result);

            Logger.LogInfo($"versionNumberTraverse:" + versionNumberTraverse.Field("Major"));

            officialVersion = versionNumberTraverse.Field<string>("Major").Value;

            UpdateVersionLabel();
        }

        public static void UpdateVersionLabel()
        {
            Traverse preloaderUiTraverse = Traverse.Create(MonoBehaviourSingleton<PreloaderUI>.Instance);

            preloaderUiTraverse.Field("string_2").SetValue($"{renegadeVersion} | {versionLabel}");
            versionNumberTraverse.Field("Major").SetValue($"{versionLabel} {renegadeVersion}");
        }
    }
}