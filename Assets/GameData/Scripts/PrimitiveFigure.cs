using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PrimitiveFigure : MonoBehaviour, IClickable {

	public event System.Action<RaycastHit> OnClick;
	public event System.Action<PrimitiveFigure> OnStart;

	[SerializeField] private Renderer renderer;
	[SerializeField] public string objectType;
	[SerializeField] private int clickCount ;
	private CompositeDisposable disposables = new CompositeDisposable ();
	private bool isTimerActive = true;

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

	public bool IsTimerActive{
		get{
			return isTimerActive;
		}
	}


	public ClickColorData filteredClickData{get; set;}
	
	// Use this for initialization
	void Start () {
		// call event OnStart
		if (OnStart != null){
			OnStart(this);
		}
	}
		
	public void Click (RaycastHit hit){
		// call event OnClick
		if (OnClick != null){
			OnClick (hit);
		}
	}

	public void StartTimer(System.Action TimerFunc, float repetitionDelay){
		isTimerActive = true;
		disposables = new CompositeDisposable ();
		Observable.Timer (System.TimeSpan.FromSeconds (repetitionDelay))
		.Repeat ()
		.Subscribe (_ => { 
			TimerFunc(); //Timer repeat this function every [repetitionDelay] seconds
		})
		.AddTo (disposables);
		
	}

	public void StopTimer(){
		isTimerActive = false;
		disposables.Dispose ();
	}

	public void SetRandomColor(){
		SetColor (new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f)));
	}

	public void SetColor(Color new_color){
		color = new_color;
	}
}
