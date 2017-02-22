using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class FormatInput : MonoBehaviour 
{
	public string type;
	public bool upperCase;

	private InputField inputField;

	public void Start()
	{
	 	inputField = GetComponent<InputField>();
	    inputField.onValueChanged.AddListener(OnValueChanged);
	}

	private void OnValueChanged(string input)
	{
	}
}