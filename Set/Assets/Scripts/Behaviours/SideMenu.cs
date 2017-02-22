using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SideMenu : MonoBehaviour 
{
	public Text nameField, xpField;

	public void Start () 
	{
		nameField.text = UserManager.user.name;
		xpField.text = UserManager.user.xp + " EXP";	
	}

	public void LoadScene(string name)
	{
		if (name != null) 
			SceneManager.LoadScene(name);
	}
}
