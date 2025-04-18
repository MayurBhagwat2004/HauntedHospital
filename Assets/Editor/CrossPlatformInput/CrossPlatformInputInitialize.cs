using System;
using System.Collections.Generic;
using UnityEditor;

namespace UnityStandardAssets.CrossPlatformInput.Inspector
{
    [InitializeOnLoad]
    public class CrossPlatformInitialize
    {
        static CrossPlatformInitialize()
        {
            var defines = GetDefinesList(BuildTargetGroup.Standalone);
            if (!defines.Contains("CROSS_PLATFORM_INPUT"))
            {
                SetEnabled("CROSS_PLATFORM_INPUT", true, false);
                SetEnabled("MOBILE_INPUT", true, true);
            }
        }

        [MenuItem("Mobile Input/Enable")]
        private static void Enable()
        {
            SetEnabled("MOBILE_INPUT", true, true);
            ShowDialog("enabled");
        }

        [MenuItem("Mobile Input/Enable", true)]
        private static bool EnableValidate()
        {
            var defines = GetDefinesList(BuildTargetGroup.Android);
            return !defines.Contains("MOBILE_INPUT");
        }

        [MenuItem("Mobile Input/Disable")]
        private static void Disable()
        {
            SetEnabled("MOBILE_INPUT", false, true);
            ShowDialog("disabled");
        }

        [MenuItem("Mobile Input/Disable", true)]
        private static bool DisableValidate()
        {
            var defines = GetDefinesList(BuildTargetGroup.Android);
            return defines.Contains("MOBILE_INPUT");
        }

        private static void SetEnabled(string defineName, bool enable, bool mobile)
        {
            BuildTargetGroup[] targetGroups = mobile
                ? new[] { BuildTargetGroup.Android, BuildTargetGroup.iOS }
                : new[] { BuildTargetGroup.Standalone };

            foreach (var group in targetGroups)
            {
                var defines = GetDefinesList(group);

                if (enable && !defines.Contains(defineName))
                    defines.Add(defineName);
                else if (!enable && defines.Contains(defineName))
                    defines.Remove(defineName);

                PlayerSettings.SetScriptingDefineSymbolsForGroup(group, string.Join(";", defines));
            }
        }

        private static List<string> GetDefinesList(BuildTargetGroup group)
        {
            return new List<string>(PlayerSettings.GetScriptingDefineSymbolsForGroup(group).Split(';'));
        }

        private static void ShowDialog(string state)
        {
            string message = $"You have {state} Mobile Input.";
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android ||
                EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
            {
                message += "\nUse Unity Remote on a connected device for testing in the Editor.";
            }
            else
            {
                message += "\nMobile Input won't work until you switch to a mobile platform.";
            }
            EditorUtility.DisplayDialog("Mobile Input", message, "OK");
        }
    }
}
