using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Answer
{
	public int id, group_id;
	public bool validated;
	public string text, coordinates, quiz_answer, 
	answerable_id, answerable_type, created_at, updated_at;
}