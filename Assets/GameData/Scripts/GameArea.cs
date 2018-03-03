using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour, IClickable {
	
	[SerializeField] private string assetBoundleAddress;
	private GameObject[] PrimitiveFigureArr = new GameObject[10];
	private IAssetBundleLoad _AssetBundleManager = new AssetBundleLoader();

	// Use this for initialization
	void Start () {
		PrimitiveFigureArr = _AssetBundleManager.GetObjectArrFromAssetBundle (assetBoundleAddress, "AssetBundles") as GameObject[];
		//StartCoroutine (_AssetBundleManager.GetAssetBundleObject (assetBoundleAddress, "capsule",PrimitiveFigureArr[0] ));// as GameObject[];
	}

	public void Click (Vector3 clickPosition){
		Debug.Log ("click"+ clickPosition);
		Instantiate (PrimitiveFigureArr[Random.Range(0,PrimitiveFigureArr.Length)], clickPosition, Quaternion.identity);
	}
}
