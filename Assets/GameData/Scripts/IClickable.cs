using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface IClickable{
	void Click (RaycastHit hit);
	event System.Action <RaycastHit> OnClick;

}
