using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class FormatInput : MonoBehaviour 
{
	private InputField field;
	
	public string Type;
	public bool isUpperCase;

	public void Start () 
	{
		field = GetComponent<InputField>();
		field.onValueChanged.AddListener(delegate {FormatValue();});
	}
	
	private void FormatValue () 
	{
		bool requiresMatch;

		var match = Regex.Matches(field.text, @"").Count;
		var cpfMatch = Regex.Matches(field.text, @"[0-9]|[.]|[-]").Count;
		var phoneMatch = Regex.Matches(field.text, @"[0-9]|[-]|[()]").Count;

		switch(Type)
		{
			case "phone":
				match = phoneMatch;
				requiresMatch = true;
				break;
			case "cpf":
				match = cpfMatch;
				requiresMatch = true;
				break;
			default:
				requiresMatch = false;
				break;
		}

		if(requiresMatch && match < field.text.Length)
			field.text = field.text.Remove(field.text.Length - 1);
		else 
			FormatByType();
	}

	private void FormatByType()
	{
		switch(Type)
		{
			case "phone":
				FormatPhone();
				break;
			case "cpf":
				FormatCpf();
				break;
			default:
				break;
		}

		if (isUpperCase)
			field.text = field.text.ToUpper();
	}

	private void FormatPhone() 
	{
		// TODO
		field.caretPosition = field.text.Length + 1;
	}

	private void FormatCpf() 
	{
		// TODO
		field.caretPosition = field.text.Length + 1;
	}
}