using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class FormatInput : MonoBehaviour 
{
	private InputField field;
	
	public string Type;

	public void Start () 
	{
		field = GetComponent<InputField>();
		field.onValueChanged.AddListener(delegate {FormatValue();});
	}
	
	private void FormatValue () 
	{
		var match = Regex.Matches(field.text, @"[0-9]|[.]|[-]|[\/]|[()]").Count;

		if(match < field.text.Length)
			field.text = field.text.Remove(field.text.Length - 1);
		else 
			VerifyType();
	}

	private void VerifyType()
	{
		switch(Type)
		{
			case "phone":
				FormatPhone();
				break;
			case "cpf":
				FormatCpf();
				break;
		}
	}

	private void FormatPhone() 
	{
		if((field.text.Length == 2) && !Input.GetKeyDown(KeyCode.Backspace))
			field.text = "(" + field.text + ")";
		else
			if((field.text.Length == 8) && !Input.GetKeyDown(KeyCode.Backspace))
				field.text = field.text + "-";

		field.caretPosition = field.text.Length + 1;
	}

	private void FormatCpf() 
	{
		if((field.text.Length == 3 || field.text.Length == 7) && !Input.GetKeyDown(KeyCode.Backspace))
			field.text = field.text + ".";
		else 
			if(field.text.Length == 11 && !Input.GetKeyDown(KeyCode.Backspace))
				field.text = field.text + "-";
		
		field.caretPosition = field.text.Length + 1;
	}
}