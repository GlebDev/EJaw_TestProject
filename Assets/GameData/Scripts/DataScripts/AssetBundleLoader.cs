using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using CoroutineData;

public class AssetBundleLoader : MonoBehaviour {
		
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

}
	