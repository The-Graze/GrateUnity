﻿using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;
public class CreateAssetBundles
{
    public static Dictionary<string, string> paths = new Dictionary<string, string>()
    {
        {"monkemenubundle", "Grate/Grate/Resources/gratebundle"}
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
        string here = "/run/media/graze/Big/Graze/Documents/GitHub/GrateUnity/GrateUnity/Assets/StreamingAssets/";
        string there = "/run/media/graze/Big/repos/";
        foreach (var entry in paths)
        {
            try
            {
                File.Copy(here + entry.Key, there + entry.Value, true);
                Debug.Log($"Exported {entry.Key} to {there + entry.Value}");
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message + "\n" + e.StackTrace);
            }
        }
    }
}
