using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//file://C:/Users/User1/Documents/UnityProjects/EJaw_TestProject/AssetBundles

public class CameraClicker : MonoBehaviour{
	
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast (ray, out hit);
			if (hit.transform.GetComponent<IClickable>() != null) {
				hit.transform.GetComponent<IClickable> ().Click (hit.point);
				//Debug.Log (hit.point);
				//Instantiate (PrimitiveFigureArr[Random.Range(0,PrimitiveFigureArr.Length)], hit.point, Quaternion.identity);
			}
		}
	}

	void OnMouseDown(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast (ray, out hit);
		Debug.Log (hit.point);
		Debug.Log ("her");
		if (hit.collider.gameObject == gameObject) {
			Debug.Log (hit.point);
			//Instantiate (PrimitiveFigureArr[Random.Range(0,PrimitiveFigureArr.Length)], hit.point, Quaternion.identity);
		}
	}
		
}
