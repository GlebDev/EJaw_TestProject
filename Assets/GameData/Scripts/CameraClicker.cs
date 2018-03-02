using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClicker : MonoBehaviour{
	[SerializeField] private string assetBoundleAddress;
	private GameObject[] PrimitiveFigureArr;
	// Use this for initialization
	void Start () {
		PrimitiveFigureArr = AssetBundleLoader.GetAllObjectFromAssetBundle (assetBoundleAddress, "AssetBundles");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast (ray, out hit);
		if (hit.collider.gameObject == gameObject) {
			Debug.Log (hit.point);
			Instantiate (PrimitiveFigureArr[Random.Range(0,PrimitiveFigureArr.Length)], hit.point, Quaternion.identity);
		}
	}
		
}
