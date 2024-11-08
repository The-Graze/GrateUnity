using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;
public class CreateAssetBundles
{
    public static Dictionary<string, string> paths = new Dictionary<string, string>()
    {
        {"lcabundle", "LightsCameraAction/Resources/lcabundle"},
        //{"gorillascience", "GorillaScienceLib/gorillascience"},
        {"walksimulator", "WalkSimulator/assetbundle"},
		{"ghostrillabundle", "Ghostrilla/assetbundle"},
        {"cppbundle", "ComputerPlusPlus/cppbundle"},
        {"doommusic", "DoomMusic/assetbundle"},
        {"trollshieldbundle", "TrollShield/trollshieldbundle"},
        {"monkemenubundle", "Bark/Resources/barkbundle"},
    };

    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = "Assets/StreamingAssets";
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.ForceRebuildAssetBundle, EditorUserBuildSettings.activeBuildTarget);
        string here = "D:/Unity/GorillaCosmeticsUnityProject/Assets/StreamingAssets/";
        string there = "C:/Users/ultra/source/repos/_GorillaTag/";
        foreach (var entry in paths)
        {
            try
            {
                System.IO.File.Copy(here + entry.Key, there + entry.Value, true);
                Debug.Log($"Exported {entry.Key} to {there + entry.Value}");
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message + "\n" + e.StackTrace);
            }
        }
    }
}