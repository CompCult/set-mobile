using UnityEngine;
using System.Collections;

public class Selection : GenericScreen {

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		backScene = "AquaHome";
	}
}
