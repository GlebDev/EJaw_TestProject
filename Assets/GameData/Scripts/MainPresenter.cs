using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MainPresenter : MonoBehaviour {

	[SerializeField] private string assetBoundleFilePath; //file://C:/Users/User1/Documents/UnityProjects/EJaw_TestProject/Assets/AssetBundles
	[SerializeField] private GameArea gameArea;

	private GameDataJson data;
	private string[] allPrefbNames;
	private GeometryObjectData geometryObjectDataManager;
	private Object[] PrimitiveFigureArr ;
	[SerializeField] private AssetBundleLoader _AssetBundleManager;
	private int PrimitiveFigureTimerDelay;




	// Use this for initialization
	void Start () {
		//PrimitiveFigureArr = _AssetBundleManager.GetObjectArrFromAssetBundle (assetBoundleFilePath, "AssetBundles") as GameObject[];
		StartCoroutine(LoadAssetBundles());
		geometryObjectDataManager = Resources.Load<GeometryObjectData>("ScriptableObject");
		data = ResourcesLoader.LoadResourceTextfile ("Data");
		allPrefbNames = data.PrefabNames;
		gameArea.OnClick += GameArea_OnClick;
	}


	void GameArea_OnClick (RaycastHit hit){
		if(PrimitiveFigureArr != null){
			PrimitiveFigure figure = gameArea.СreateObject (PrimitiveFigureArr [Random.Range (0, PrimitiveFigureArr.Length)] as GameObject, hit.point).GetComponent<PrimitiveFigure>();
			figure.name = allPrefbNames.FirstOrDefault(x => x == figure.objectType);
			figure.filteredClickData = geometryObjectDataManager.ClicksData.Single (s => s.objectType == figure.objectType);
			figure.OnClick += Figure_OnClick;
			figure.OnStart += Figure_OnStart;
		}
	}


	void Figure_OnClick (RaycastHit hit){
		PrimitiveFigure curFigure = hit.transform.GetComponent<PrimitiveFigure> ();
		curFigure.ClickCount++;
		if (curFigure.ClickCount > curFigure.filteredClickData.minClicksCount && curFigure.ClickCount < curFigure.filteredClickData.maxClicksCount) {
			curFigure.StopTimer ();
			curFigure.color = curFigure.filteredClickData.color;
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
		AssetBundleLoader.CoroutineWithData cd = new AssetBundleLoader.CoroutineWithData(this, _AssetBundleManager.GetObjectArrFromAssetBundle(assetBoundleFilePath, "AssetBundles"));
		yield return cd.coroutine;

		PrimitiveFigureArr = cd.result as Object[];
	}


}
