using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Common
{
	/// <summary>
	/// 
	/// </summary>
	public class ResourceManager
	{
		private static Dictionary<string, string> configMap = new Dictionary<string, string>();

		static ResourceManager()
        {
            string fileContent = GetFilePaths();
            //fileContent: "文件名=路径/r/n文件名=路径/r/n..."

            BuildMap(fileContent);
        }

        private static void BuildMap(string fileContent)
        {
            using (StringReader reader = new StringReader(fileContent))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] keyValue = line.Split('=');
                    configMap.Add(keyValue[0], keyValue[1]);
                }
            }
        }

        private static string GetFilePaths()
        {
			string url = "file://" + Application.streamingAssetsPath + "/ConfigMap.txt";
			UnityWebRequest webRequest = UnityWebRequest.Get(url);
			webRequest.SendWebRequest();
            while (true)
            {
				if (webRequest.downloadHandler.isDone)
					return webRequest.downloadHandler.text;
            }

        }

		public static T Load<T>(string prefabName) where T : Object
        {
            return Resources.Load<T>(configMap[prefabName]);
        }
	}
}