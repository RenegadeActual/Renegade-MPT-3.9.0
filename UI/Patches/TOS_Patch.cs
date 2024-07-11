using Comfort.Common;
using System;
using System.Text;
using System.Reflection;
using EFT;
using EFT.UI;
using SPT.Reflection.Patching;
using UnityEngine;

namespace Renegade.Core.UI.Patches
{
    public class TOS_Patch : ModulePatch
    {
        protected const string str_1 = "Test";
        protected const string str_2 = "Test";

        protected override MethodBase GetTargetMethod() => typeof(TarkovApplication).GetMethod(nameof(TarkovApplication.method_23));

        private static bool HasShown = false;

        [PatchPostfix]
        public static void Postfix()
        {
            if (HasShown)
            {
                return;
            }

            HasShown = true;

            if (!RenegadePlugin.AcceptedTOS.Value)
            {
                if (Singleton<PreloaderUI>.Instance != null)
                {
                    Singleton<PreloaderUI>.Instance.ShowRenegadeMessage("RENEGADE-MPT", str_1, ErrorScreen.EButtonType.QuitButton, 30f,
                        Application.Quit, AcceptTos);
                }
                else
                {
                    Debug.LogError("PreloaderUI instance is null");
                    return;
                }
                RenegadePlugin.Logger.LogInfo("TOS not accepted, showing TOS dialog");
            }
            else
            {
                if (Singleton<PreloaderUI>.Instance != null)
                {
                    Singleton<PreloaderUI>.Instance.ShowRenegadeMessage("RENEGADE-MPT", str_2, ErrorScreen.EButtonType.OkButton, 0f,
                       null,
                       null);
                }
                else
                {
                    Debug.LogError("PreloaderUI instance is null");
                    return;
                }
                RenegadePlugin.Logger.LogInfo("TOS accepted, skipping TOS dialog");
            }
        }

        private static void AcceptTos()
        {
            RenegadePlugin.AcceptedTOS.Value = true;
        }
    }
}
