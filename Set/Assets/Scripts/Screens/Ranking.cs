using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Ranking : GenericScreen 
{
	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = "Home";
	}
}
