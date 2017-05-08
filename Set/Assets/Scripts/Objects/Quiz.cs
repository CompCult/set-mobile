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
}

