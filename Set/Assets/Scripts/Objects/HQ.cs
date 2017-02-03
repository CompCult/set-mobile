using UnityEngine;
using System.Collections;

[System.Serializable]
public class HQ
{
	public int user_id, value; // Author and Rate
	public string photo_url;

	public Photo photo;

	public override string ToString()
	{
		return " / user_id: " + user_id +
		" / value: " + value +  
		" / url: " + photo_url + " HQ: " + photo.ToString(); 
	}
}
