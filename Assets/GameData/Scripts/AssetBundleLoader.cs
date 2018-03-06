using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class AssetBundleLoader : MonoBehaviour {

	public Object[] GetObjectArrFromAssetBundle1(string url, string ManifetFileName) {
		AssetBundleManifest manifest = GetAssetBundleManifest1 (url, ManifetFileName);
		string[] names = manifest.GetAllAssetBundles();
		Object[] obj = new GameObject[names.Length];
		for (int i = 0; i < names.Length; i++) {
			obj [i] = GetAssetBundleObject1 (url + "/" + names[i], names [i]);
		}
		return obj;
	}
		
	public Object GetAssetBundleObject1(string url, string name){
		//string FilePath = "file://" + Application.dataPath + url;
		string FilePath = url;
		Debug.Log (FilePath);
		WWW www = new WWW (FilePath);
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

	public AssetBundleManifest GetAssetBundleManifest1(string url, string ManifestFileName) {
		AssetBundleManifest manifest = null;
		manifest = GetAssetBundleObject1(url + "/" + ManifestFileName, "AssetBundleManifest") as AssetBundleManifest;
		return manifest;
	}
		
	public IEnumerator GetAssetBundle(string url, string bundle_name) {
		string FilePath = url;
		WWW www = new WWW(FilePath);
		yield return www;
		if(!string.IsNullOrEmpty(www.error)) {
			throw new UnityException (www.error);
			yield break;
		}
		if(!www.assetBundle.Contains(bundle_name)) {
			throw new UnityException ("AssetBundle is not contain " + bundle_name);
			yield break;
		}
		Object resultObj = www.assetBundle.LoadAsset (bundle_name);
		www.assetBundle.Unload (false);
		yield return resultObj;
	}

	public IEnumerator GetAssetBundleManifest(string url, string ManifestFileName) {
		//StartCoroutine (GetAssetBundle (url + "/" + ManifestFileName, "AssetBundleManifest", resultObj));

		CoroutineWithData cd = new CoroutineWithData(this, GetAssetBundle (url , "AssetBundleManifest") );
		yield return cd.coroutine;

		AssetBundleManifest resultObj  = cd.result as AssetBundleManifest;

		yield return resultObj;
	}

	public IEnumerator GetObjectArrFromAssetBundle(string url, string ManifetFileName) {
		CoroutineWithData cd = new CoroutineWithData(this, GetAssetBundleManifest(url + "/" + ManifetFileName, ManifetFileName));
		yield return cd.coroutine;
		AssetBundleManifest manifest = cd.result as AssetBundleManifest;
		string[] names = manifest.GetAllAssetBundles();
		Object[] resultObjArr = new Object[names.Length];
		for (int i = 0; i < names.Length; i++) {
			cd = new CoroutineWithData(this, GetAssetBundle(url + "/" + names[i], names[i]));
			yield return cd.coroutine;

			resultObjArr[i] = cd.result as Object;
		}
		yield return resultObjArr;
	}
		



	public class CoroutineWithData {
		public Coroutine coroutine { get; private set; }
		public object result;
		private IEnumerator target;
		public CoroutineWithData(MonoBehaviour owner, IEnumerator target) {
			this.target = target;
			this.coroutine = owner.StartCoroutine(Run());
		}

		private IEnumerator Run() {
			while(target.MoveNext()) {
				result = target.Current;
				yield return result;
			}
		}
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