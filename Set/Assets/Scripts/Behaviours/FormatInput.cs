using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class FormatInput : MonoBehaviour 
{
	private InputField inputField;

	public string type;
	public bool upperCase;

	private string regex,
	phoneRegex = "^[0-9]|[-]|[()]",
	cpfRegex = "^[0-9]|[.]|[-]";

	public void Start()
	{
		inputField = GetComponent<InputField>();

		CheckRegexType();

		inputField.onEndEdit.AddListener (delegate {LockInput (inputField);});
		inputField.onValueChanged.AddListener (delegate {CheckRegex (inputField);});
	}

	private void CheckRegexType()
	{
		switch (type)
		{
			case "phone": 
				regex = phoneRegex; break;
			case "cpf": 
				regex = cpfRegex; break;
		}
	}

	private void LockInput(InputField input)
	{
		switch (type)
		{
			case "phone": 
				FormatPhone(input); 
				break;
			case "cpf": 
				FormatCPF(input); 
				break;
		}
	}

	private void CheckRegex(InputField input)
	{
		if (regex == null)
			return;

		if (input.text.Length > 0 && !Regex.IsMatch(input.text, regex))
		{
			Debug.Log("Incorrect char");
			input.text = input.text.Substring(0, input.text.Length - 1);
			return;
		}
	}

	private void FormatPhone(InputField input)
	{
		if (input.text.Length == 10)
		{
			input.characterLimit = 14;
			input.text = string.Format("({0}){1}-{2}", input.text.Substring(0, 2), input.text.Substring(2, 4), input.text.Substring(6, 4));
		}
		else if (input.text.Length == 11)
		{
			input.characterLimit = 14;
			input.text = string.Format("({0}){1}-{2}", input.text.Substring(0, 2), input.text.Substring(2, 5), input.text.Substring(7, 4));
		}
	}

	private void FormatCPF(InputField input)
	{
		if (input.text.Length == 11)
		{
			input.characterLimit = 14;
			input.text = string.Format("{0}.{1}.{2}-{3}", input.text.Substring(0, 3), input.text.Substring(3, 3), input.text.Substring(6, 3), input.text.Substring(9, 2));
		}
	}
}