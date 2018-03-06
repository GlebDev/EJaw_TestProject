using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClicker : MonoBehaviour{
	
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast (ray, out hit);
			if (hit.transform.GetComponent<IClickable>() != null) {
				hit.transform.GetComponent<IClickable>().Click(hit);
			}
		}
	}
	private void IClickable_Click(RaycastHit hit){
		if (OnIClickableClick != null)
			OnIClickableClick (hit);
	}

	public System.Action<RaycastHit> OnIClickableClick;
}
