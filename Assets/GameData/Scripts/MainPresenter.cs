using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MainPresenter : MonoBehaviour {

	[SerializeField] private GeometryObjectData Admin;
	[SerializeField] private string assetBoundleAddress;
	[SerializeField] private GameArea gameArea;
	[SerializeField] private GeometryObjectData geometryObjectDataManager;

	private GameObject[] PrimitiveFigureArr = null;
	private IAssetBundleLoad _AssetBundleManager = new AssetBundleLoader();
	private List<PrimitiveFigure> FiguresList = new List<PrimitiveFigure> ();



	// Use this for initialization
	void Start () {
		PrimitiveFigureArr = _AssetBundleManager.GetObjectArrFromAssetBundle (assetBoundleAddress, "AssetBundles") as GameObject[];
		gameArea.OnClick += GameArea_OnClick;
		//i1.OnClick += I1_OnClick;
	}

	void Figure_OnClick (RaycastHit hit){
		PrimitiveFigure curFigure = hit.transform.GetComponent<PrimitiveFigure> ();
		curFigure.ClickCount++;

		if (curFigure.ClickCount > curFigure.filteredClickData.minClicksCount && curFigure.ClickCount < curFigure.filteredClickData.maxClicksCount) {
			curFigure.StopTimer ();
			curFigure.color = curFigure.filteredClickData.color;
		} else {
			curFigure.StartTimer ();
		}

	}

	void GameArea_OnClick (RaycastHit hit){
		PrimitiveFigure figure = gameArea.СreateObject (PrimitiveFigureArr [Random.Range (0, PrimitiveFigureArr.Length)], hit.point).GetComponent<PrimitiveFigure>();
		Debug.Log (figure.objectType);
		FiguresList.Add(figure);
		figure.OnClick += Figure_OnClick;
		//Debug.Log (geometryObjectDataManager.ClicksData);//.Single (s => s.objectType == "capsule"));
		figure.filteredClickData = geometryObjectDataManager.ClicksData.Single (s => s.objectType == figure.objectType);
	}

		
	// Update is called once per frame
	void Update () {
		
	}
}
