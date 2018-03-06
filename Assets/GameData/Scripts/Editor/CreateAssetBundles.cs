using UnityEditor;
using System.Collections;

public class CreateAssetBundles{
	[MenuItem ("Assets/Build AssetBundles")]
	static void BuildAllAssetBundles (){
		string path = EditorUtility.SaveFolderPanel("Save Bundle", "", "");  //Отображает диалоговое окно "Сохранить Папку" и возвращается
		if (path.Length != 0){
			BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
		}
	}
}
	