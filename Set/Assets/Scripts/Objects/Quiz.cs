using UnityEngine;
using System.Collections;

public class Quiz {

	public int id;

	public string name,
				  question,
				  option_1,
				  option_2,
				  option_3,
				  option_4,
				  option_5,
				  correct,
				  created_at,
				  updated_at;

	public Quiz CreateQuiz (string json)
	{
		return JsonUtility.FromJson<Quiz>(json);
	}

}
