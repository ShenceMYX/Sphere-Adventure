using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


/// <summary>
/// 找到所有Resources文件 生成“文件名=路径”对应关系 方便直接通过文件名读取文件
/// </summary>
public class ResourceConfigFactory : Editor
{
    [MenuItem("Tools/Resources/Generate Resource Config Map")]
    public static void GenerateMap()
    {
        //1.读取所有资源文件
        string[] resFilesGUID = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/Resources" });

        string[] resFilesPath = new string[resFilesGUID.Length]; 
        string[] resFiles = new string[resFilesGUID.Length];

        for (int i = 0; i < resFilesGUID.Length; i++)
        {
            //Assets/Resources/Aura/AuraRing/AuraRingArcane.prefab
            resFilesPath[i] = AssetDatabase.GUIDToAssetPath(resFilesGUID[i]);
            //AuraRingArcane
            string fileName = Path.GetFileNameWithoutExtension(resFilesPath[i]);
            //Aura/AuraRing/AuraRingArcane
            string filePath = resFilesPath[i].Replace("Assets/Resources/", string.Empty).Replace(".prefab", string.Empty);
            //2.生成对应关系
            resFiles[i] = fileName + "=" + filePath;
        }

        //3.写入文件
        File.WriteAllLines("Assets/StreamingAssets/ConfigMap.txt", resFiles);

        AssetDatabase.Refresh();
    }
}
