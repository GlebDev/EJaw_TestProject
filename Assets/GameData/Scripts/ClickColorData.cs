using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ClickColorData: System.Object{

	public string objectType;
	public int minClicksCount;
	public int maxClicksCount;
	public Color color;
	/*
	public string objectType{get; set;}
	public int minClicksCount{get; set;}
	public int maxClicksCount{get; set;}
	public Color color{get; set;}*/

	public ClickColorData():this("",0,1,Color.white){
	}

	public ClickColorData(string ObjectType, int MinClicksCount, int MaxClicksCount, Color MeshColor){
		//this = new ClickColorData ();
		objectType = ObjectType;
		minClicksCount = MinClicksCount;
		maxClicksCount = MaxClicksCount;
		color = MeshColor;
	}

}
