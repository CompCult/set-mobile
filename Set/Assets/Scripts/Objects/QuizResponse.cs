using UnityEngine;
using System.Collections;

public class QuizResponse
{
	public int quiz_id,
	user_id,
	quiz_answer;

	public override string ToString()
	{
		return "Quiz: " + quiz_id + " | " +
			"User: " + user_id + " | " +
			"Resposta: " + quiz_answer;
	}
}
