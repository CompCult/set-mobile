using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public static class LocalizationManager 
{
	private static TextAsset languageFile;
	private static Dictionary<string, string> texts;
	
	private static XmlDocument xmlDoc = new XmlDocument();
	private static XmlReader reader;
	
	private static string[] tags;
	private static string lang;

	public static string GetLang () 
	{
		return lang;
	}

	public static void SetLang (string lan) 
	{
		PlayerPrefs.SetString("Language", lan);
	}

	public static void Start () 
	{
		tags = new string[] {"NULL", "PT", "EN"};
		languageFile = (TextAsset) Resources.Load ("Lang/Translations", typeof(TextAsset));

		if (!PlayerPrefs.HasKey("Language")) 
		{
			lang = tags[0];
			return;
		}
		else 
		{
			lang = PlayerPrefs.GetString("Language");
		}

		texts = new Dictionary<string, string>();
		reader = XmlReader.Create(new StringReader(languageFile.text));
		xmlDoc.Load(reader);

		XmlNodeList langs = xmlDoc["Data"].GetElementsByTagName(lang);
		
		for (int j = 0; j < langs.Count; j++) 
		{
			texts.Add(langs[j].Attributes["Key"].Value, langs[j].Attributes["Word"].Value);
		}
	}

	public static string GetText(string key) 
	{
		return texts[key];
	}
}
