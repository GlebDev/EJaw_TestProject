using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PrimitiveFigure : MonoBehaviour, IClickable {

	[SerializeField] private Renderer renderer;
	private int ClickCount, ColorChangeDelay = 1;
	private Color color{
		set{
			renderer.material.color = value;
		}
		get{
			return renderer.material.color;
		}
	}
	
	// Use this for initialization
	void Start () {
		Observable.Timer (System.TimeSpan.FromSeconds (ColorChangeDelay))
			.Repeat () 
			.Subscribe (_ => { 
				СolorСhangeСycle();
			}).AddTo (this);
	}
		
	public void Click (Vector3 clickPosition){
		ClickCount++;
	}




	void СolorСhangeСycle(){
			color = new Color (Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
	}
}
