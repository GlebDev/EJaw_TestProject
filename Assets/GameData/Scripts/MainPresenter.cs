using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using CoroutineData;

public class MainPresenter : MonoBehaviour {

	[SerializeField] private string assetBoundleFilePath; //file://C:/Users/User1/Documents/UnityProjects/EJaw_TestProject/Assets/AssetBundles || https://glebdev.github.io/AssetBundles
	[SerializeField] private GameArea gameArea;
	[SerializeField] private AssetBundleLoader _AssetBundleManager;

	private GameDataJson data;
	private string[] allPrefbNames;
	private GeometryObjectData geometryObjectDataManager;
	private Object[] PrimitiveFigureArr ;
	private int PrimitiveFigureTimerDelay;




	// Use this for initialization
	void Start () {
		StartCoroutine(LoadAssetBundles()); //load Asset Bundles from server
		geometryObjectDataManager = Resources.Load<GeometryObjectData>("ScriptableObject"); //load GeometryObjectData from Recources
		data = ResourcesLoader.LoadResourceTextFile ("Data"); //load GameDataJson that contain PrefabNames array 
		allPrefbNames = data.PrefabNames;
		gameArea.OnClick += GameArea_OnClick;
	}


	void GameArea_OnClick (RaycastHit hit){
		if (PrimitiveFigureArr != null) {
			PrimitiveFigure figure = gameArea.СreateObject (PrimitiveFigureArr [Random.Range (0, PrimitiveFigureArr.Length)] as GameObject, hit.point).GetComponent<PrimitiveFigure> ();
			figure.name = allPrefbNames.FirstOrDefault (x => x == figure.ObjectType);
			figure.ClickData = geometryObjectDataManager.ClicksData.Single (s => s.objectType == figure.ObjectType);
			figure.OnClick += Figure_OnClick;
			figure.OnStart += Figure_OnStart;
		} else {
			Debug.Log ("Object is not loaded");
		}
	}


	void Figure_OnClick (RaycastHit hit){
		PrimitiveFigure curFigure = hit.transform.GetComponent<PrimitiveFigure> ();
		curFigure.ClickCount++;
		if (curFigure.ClickCount >= curFigure.ClickData.minClicksCount && curFigure.ClickCount <= curFigure.ClickData.maxClicksCount) {
			curFigure.StopTimer ();
			curFigure.color = curFigure.ClickData.color;
		} else {
			if(!curFigure.IsTimerActive){
				curFigure.StartTimer (curFigure.SetRandomColor, geometryObjectDataManager.ColorChangeTimerRepeatDelay);
			}
		}

	}

	void Figure_OnStart (PrimitiveFigure obj){
		obj.StartTimer (obj.SetRandomColor, geometryObjectDataManager.ColorChangeTimerRepeatDelay);
	}

	IEnumerator LoadAssetBundles() {
		CoroutineWithData cd = new CoroutineWithData(this, _AssetBundleManager.GetObjectArrFromAssetBundle(assetBoundleFilePath, "AssetBundles"));
		yield return cd.coroutine;

		PrimitiveFigureArr = cd.result as Object[];
	}


}
