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
	
	private static string lang;

	public static void Start () 
	{
		languageFile = (TextAsset) Resources.Load ("Lang/Translations", typeof(TextAsset));
		lang = "PT";

		texts = new Dictionary<string, string>();
		reader = XmlReader.Create(new StringReader(languageFile.text));
		xmlDoc.Load(reader);

		XmlNodeList words = xmlDoc["Data"].GetElementsByTagName(lang);
		
		for (int j = 0; j < words.Count; j++) 
		{
			texts.Add(words[j].Attributes["Key"].Value, words[j].Attributes["Word"].Value);
		}

		Debug.Log("Localization manager started");
	}

	public static string GetText(string key) 
	{
		return texts[key];
	}

	public static string GetLang () 
	{
		return lang;
	}
}
