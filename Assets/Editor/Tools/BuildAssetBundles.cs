using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BuildAssetBundles 
{
    [MenuItem("Tools/AssetBundle/Build Lua AssetBundle")] 
    static void BuildLuaBundles()
    {
        string outputPath = "Assets/AssetBundles/Lua";
        
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }

        string[] allBundleNames = AssetDatabase.GetAllAssetBundleNames();

        if (!allBundleNames.Contains("lua_bundle"))
        {
            Debug.LogError("No Lua AssetBundle found. Please ensure you have assigned the 'lua_bundle' AssetBundle name to your Lua files.");
            return;
        }

        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];
        buildMap[0].assetBundleName = "lua_bundle";

        string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundle("lua_bundle");
        buildMap[0].assetNames = assetPaths;

        BuildPipeline.BuildAssetBundles(
            outputPath, 
            buildMap,
            BuildAssetBundleOptions.None,
            EditorUserBuildSettings.activeBuildTarget
        );

        string streamingAssetsPath = "Assets/StreamingAssets/Lua";
        if (!Directory.Exists(streamingAssetsPath))
            Directory.CreateDirectory(streamingAssetsPath);

        string sourceFile = Path.Combine(outputPath, "lua_bundle");
        string destFile = Path.Combine(streamingAssetsPath, "lua_bundle");
        if (File.Exists(sourceFile))
        {
            File.Copy(sourceFile, destFile, true);
            Debug.Log($"Copied lua_bundle to: {destFile}");
            AssetDatabase.Refresh(); 
        }
        else
        {
            Debug.LogError("lua_bundle not found after build!");
        }
    }
}
