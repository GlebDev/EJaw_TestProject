using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class ResourcesLoader : MonoBehaviour {

	public static GameDataJson LoadResourceTextfile(string path){
		TextAsset targetFile = Resources.Load<TextAsset>(path);
		GameDataJson loadedData = JsonUtility.FromJson<GameDataJson>(targetFile.text);
		return loadedData;
	}

	public static void SaveResourceTextfile(string path, GameDataJson newData){
		string dataAsJson = JsonUtility.ToJson (newData);
		string filePath = Application.dataPath + path;;
		Debug.Log ( dataAsJson);
		File.WriteAllText (filePath, dataAsJson);
	}

}