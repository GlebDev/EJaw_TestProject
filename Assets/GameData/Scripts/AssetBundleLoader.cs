using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


interface IAssetBundleLoad{
	Object[] GetObjectArrFromAssetBundle (string url, string ManifetFileName);
	Object GetAssetBundleObject (string url, string name);
	AssetBundleManifest GetAssetBundleManifest (string url, string ManifetFileName);
}


public class AssetBundleLoader : MonoBehaviour, IAssetBundleLoad {

	public Object[] GetObjectArrFromAssetBundle(string url, string ManifetFileName) {
		AssetBundleManifest manifest = GetAssetBundleManifest (url, ManifetFileName);
		string[] names = manifest.GetAllAssetBundles();
		Object[] obj = new GameObject[names.Length];
		for (int i = 0; i < names.Length; i++) {
			obj [i] = GetAssetBundleObject (url + "/" + names[i], names [i]);
		}
		return obj;
	}
		
	public Object GetAssetBundleObject(string url, string name){
		WWW www = new WWW (url);
		if(!string.IsNullOrEmpty(www.error)) {
			throw new UnityException (www.error);
		}
		if(!www.assetBundle.Contains(name)) {
			throw new UnityException ("AssetBundle is not contain " + name);
		}
		Object obj = www.assetBundle.LoadAsset (name);
		www.assetBundle.Unload (false);
		if(obj == null){
			throw new UnityException ("AssetBundleObject is null");
		}
		return obj;
	}

	public AssetBundleManifest GetAssetBundleManifest(string url, string ManifetFileName) {
		AssetBundleManifest manifest = null;
		manifest = GetAssetBundleObject(url + "/" + ManifetFileName, "AssetBundleManifest") as AssetBundleManifest;
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