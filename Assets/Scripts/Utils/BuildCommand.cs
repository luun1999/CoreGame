using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class BuildCommand
{
    public const string GAME_NAME = "SupermanGame";

    public static void PerformBuild()
    {
        var buildReport = BuildPipeline.BuildPlayer(GetBuildScene(), GetBuildPath(BuildTarget.StandaloneWindows), BuildTarget.StandaloneWindows, GetBuildOptions());

        if (buildReport.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            throw new System.Exception("Build end with status: " + buildReport.summary.result);
        }
    }

    public static string GetBuildPath(BuildTarget buildTarget)
    {
        string basePath = Application.dataPath + "/_build";

        if (buildTarget.ToString().ToLower().Contains("windows"))
        {
            string path = basePath + "/windows";
            if (!File.Exists(path))
                File.Create(path);

            return path + "/" + GetBuildName(buildTarget) + ".exe";
        }

        return basePath;
    }

    public static string GetBuildName(BuildTarget buildTarget)
    {
        string buildName = GAME_NAME + buildTarget.ToString();
        return buildName;
    }

    public static BuildOptions GetBuildOptions()
    {
        return BuildOptions.None;
    }

    public static string[] GetBuildScene()
    {
        List<string> scenes = new List<string>();

        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled && !string.IsNullOrEmpty(scene.path))
            {
                scenes.Add(scene.path);
            }
        }

        return scenes.ToArray();
    }
}
