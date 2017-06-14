using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public static class XcodeProjectModifer
{

    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string path)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

            PBXProject proj = new PBXProject();
            proj.ReadFromString(File.ReadAllText(projPath));
            string target = proj.TargetGuidByName("Unity-iPhone");

            // GoogleMobileAds (aka AdMob):
            proj.AddBuildProperty(target, "CLANG_ENABLE_MODULES", "YES");
            proj.AddBuildProperty(target, "OTHER_LDFLAGS", "$(inherited)");

            File.WriteAllText(projPath, proj.WriteToString());
        }
    }
}