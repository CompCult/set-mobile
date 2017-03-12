using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendEmail : GenericScreen
{
	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = "GroupScreen";
	}
}
