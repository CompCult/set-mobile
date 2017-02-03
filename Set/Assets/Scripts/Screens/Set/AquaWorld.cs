using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AquaWorld : GenericScreen
{
	void Start () 
	{
		UnityAndroidExtras.instance.Init();
		backScene = "Home";
	}
}
