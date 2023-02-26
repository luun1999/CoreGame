using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildMenu : MonoBehaviour
{
    [MenuItem("Build/Windows")]
    public static void BuildWindows()
    {
        BuildCommand.PerformBuild();
    }
}
