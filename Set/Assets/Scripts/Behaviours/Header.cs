using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Header : MonoBehaviour 
{
	public void ReturnHome()
	{
		SceneManager.LoadScene("Home");
	}
}
