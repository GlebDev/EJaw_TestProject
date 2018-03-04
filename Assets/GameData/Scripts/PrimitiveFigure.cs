using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PrimitiveFigure : MonoBehaviour, IClickable {

	public event System.Action<RaycastHit> OnClick;

	[SerializeField] private Renderer renderer;
	[SerializeField] public string objectType;
	[SerializeField] private int clickCount, colorChangeDelay = 1;
	private CompositeDisposable disposables = new CompositeDisposable ();

	public Color color{
		set{
			renderer.material.color = value;
		}
		get{
			return renderer.material.color;
		}
	}
	public int ClickCount{
		set{
			clickCount = value;
		}
		get{
			return clickCount;
		}
	}
	public int ColorChangeDelay{
		set{
			colorChangeDelay = value;
		}
		get{
			return colorChangeDelay;
		}
	}

	public ClickColorData filteredClickData;
	
	// Use this for initialization
	void Start () {
		disposables.Dispose ();
		StartTimer ();
	}
		
	public void Click (RaycastHit hit){
		if (OnClick != null){
			OnClick (hit);
		}
	}

	public void StartTimer(){
		if (disposables.IsDisposed) {
			disposables = new CompositeDisposable ();
			Observable.Timer (System.TimeSpan.FromSeconds (ColorChangeDelay))
			.Repeat ()
			.Subscribe (_ => { 
				SetRandomColor ();
			})
			.AddTo (disposables);
		}
		
	}

	public void StopTimer(){
		disposables.Dispose ();
	}

	public void SetRandomColor(){
		SetColor (new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f)));
	}

	public void SetColor(Color new_color){
		color = new_color;
	}
}
