using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class FormatInput : MonoBehaviour 
{
	public InputField Field;
	public Text FieldText;
	public string Type;

	public void Start () 
	{
		Field.onValueChanged.AddListener(delegate {FormatValue();});
	}
	
	private void FormatValue () 
	{
		var match = Regex.Matches(Field.text, @"[0-9]|[.]|[-]|[\/]").Count;

		if(match < Field.text.Length)
			Field.text = Field.text.Remove(Field.text.Length - 1);
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
		if((Field.text.Length == 2 || Field.text.Length == 5) && !Input.GetKeyDown(KeyCode.Backspace))
			Field.text = Field.text + "/";

		Field.caretPosition = Field.text.Length + 1;
	}

	private void FormatCpf() 
	{
		if((Field.text.Length == 3 || Field.text.Length == 7) && !Input.GetKeyDown(KeyCode.Backspace))
			Field.text = Field.text + ".";
		else if(Field.text.Length == 11 && !Input.GetKeyDown(KeyCode.Backspace))
			Field.text = Field.text + "-";
		
		Field.caretPosition = Field.text.Length + 1;
	}
}
