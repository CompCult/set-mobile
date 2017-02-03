using UnityEngine;
using System.Collections;

public class ActivityResponse 
{
	public int activity_id,
	user_id;

	public string text,
	coord_start,
	coord_mid,
	coord_end;

	public byte[] photo,
	audio;

	public override string ToString()
	{
		return "Atividade: " + activity_id + " | " +
			"User: " + user_id + " | " +
			"Texto: " + ((text == null) ? "não" : "sim") + " | " +
			"C Start: " + ((coord_start == null) ? "não" : coord_start) + " | " +
			"C Mid: " + ((coord_mid == null) ? "não" : coord_mid) + " | " +
			"C End: " + ((coord_end == null) ? "não" : coord_end) + " | " +
			"Foto: " + ((photo == null) ? "não" : "sim");
	}
}
