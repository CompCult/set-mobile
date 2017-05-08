using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuizResponse 
{
	public int quiz_id,
	user_id,
	answer;

	public string type = "Quiz";
}
