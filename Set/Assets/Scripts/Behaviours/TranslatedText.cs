using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslatedText : MonoBehaviour 
{
	public string keyName;
	
	public void Start()
	{
		GetComponent<Text>().text = LocalizationManager.GetText(keyName);
	}
}
