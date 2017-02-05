using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class FormatInput : MonoBehaviour 
{
	private InputField field;
	private Text fieldText;
	
	public string Type;

	public void Start () 
	{
		field = GetComponent<InputField>();
		fieldText = field.GetComponent<Text>();

		field.onValueChanged.AddListener(delegate {FormatValue();});
	}
	
	private void FormatValue () 
	{
		var match = Regex.Matches(fieldText.text, @"[0-9]|[.]|[-]|[\/]").Count;

		if(match < fieldText.text.Length)
			fieldText.text = fieldText.text.Remove(fieldText.text.Length - 1);
		else 
			VerifyType();
	}

	private void VerifyType()
	{
		switch(Type)
		{
			case "date":
				FormatPhone();
				break;
			case "cpf":
				FormatCpf();
				break;
		}
	}

	private void FormatPhone() 
	{
		if((fieldText.text.Length == 2 || fieldText.text.Length == 5) && !Input.GetKeyDown(KeyCode.Backspace))
			fieldText.text = fieldText.text + "/";

		field.caretPosition = fieldText.text.Length + 1;
	}

	private void FormatCpf() 
	{
		if((fieldText.text.Length == 3 || fieldText.text.Length == 7) && !Input.GetKeyDown(KeyCode.Backspace))
			fieldText.text = fieldText.text + ".";
		else if(fieldText.text.Length == 11 && !Input.GetKeyDown(KeyCode.Backspace))
			fieldText.text = fieldText.text + "-";
		
		field.caretPosition = fieldText.text.Length + 1;
	}
}