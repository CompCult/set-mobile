using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quiz 
{
	public int id,
			   next_quiz;

	public string name,
				  question,
				  option_1,
				  option_2,
				  option_3,
				  option_4,
				  option_5;

	public string[] answers;

	public void NormalizeAnswers()
	{
		answers = new string[5];

		answers[0] = option_1;
		answers[1] = option_2;
		answers[2] = option_3;
		answers[3] = option_4;
		answers[4] = option_5;
	}
}

