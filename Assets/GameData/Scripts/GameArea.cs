using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour, IClickable {

	public event System.Action<RaycastHit> OnClick;


	public void Click (RaycastHit hit){
		if (OnClick != null){
			OnClick (hit);
		}
	}

	// Use this for initialization
	void Start () {
		//PrimitiveFigureArr = _AssetBundleManager.GetObjectArrFromAssetBundle (assetBoundleAddress, "AssetBundles") as GameObject[];
		//StartCoroutine (_AssetBundleManager.GetAssetBundleObject (assetBoundleAddress, "capsule",PrimitiveFigureArr[0] ));// as GameObject[];
	}

	public GameObject СreateObject (GameObject new_object, Vector3 clickPosition){
		return Instantiate (new_object, clickPosition, Quaternion.identity);
	}


}
