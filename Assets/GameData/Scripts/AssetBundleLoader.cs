using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class AssetBundleLoader : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

	}
		

	public static GameObject[] GetAllObjectFromAssetBundle(string url, string ManifetFileName) {
		AssetBundleManifest manifest = GetAssetBundleManifest (url, ManifetFileName);
		string[] names = manifest.GetAllAssetBundles();
		GameObject[] obj = new GameObject[names.Length];
		for (int i = 0; i < names.Length; i++) {
			obj [i] = GetAssetBundleObject (url + "/" + names[i], names [i]) as GameObject;
		}
		return obj;
	}
		
	public static Object GetAssetBundleObject(string url, string name) {
		WWW www = new WWW (url);
		Object obj = www.assetBundle.LoadAsset (name);
		www.assetBundle.Unload (false);
		return obj;
	}

	private static AssetBundleManifest GetAssetBundleManifest(string url, string ManifetFileName) {
		AssetBundleManifest manifest = GetAssetBundleObject(url + "/" + ManifetFileName , "AssetBundleManifest") as AssetBundleManifest;
		return manifest;
	}

}


/*public class MyBehaviour : MonoBehaviour {
	void Start() {
		StartCoroutine(GetAssetBundle());
	}

	IEnumerator GetAssetBundle() {
		UnityWebRequest www = UnityWebRequest.GetAssetBundle("http://www.my-server.com/myData.unity3d");
		yield return www.SendWebRequest();

		if(www.isNetworkError || www.isHttpError) {
			Debug.Log(www.error);
		}
		else {
			AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
		}
	}
}*/