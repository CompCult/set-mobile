using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuizAPI
{
	public static WWW RequestPublicQuizzes ()
	{
		WebAPI.apiPlace = "/quiz/public/";
		return WebAPI.Get();
	}

	public static WWW RequestPrivateQuiz (string quizID)
	{
		WebAPI.apiPlace = "/quiz/private/" + quizID + "/";
		return WebAPI.Get();
	}

	public static WWW SendQuizResponse (Quiz quiz, QuizResponse quizResponse)
	{
		WWWForm responseForm = new WWWForm ();

		if (quizResponse.user_id != 0)
		{
			responseForm.AddField("user_id", quizResponse.user_id);
			Debug.Log("User ID: " + quizResponse.user_id);
		}

 		responseForm.AddField ("answerable_id", quizResponse.quiz_id);
 		responseForm.AddField ("answerable_type", quizResponse.type);
 		responseForm.AddField ("quiz_answer", quizResponse.answer);

 		Debug.Log("ID: " + quizResponse.quiz_id + " / Type: " + quizResponse.type + " / Answer: " + quizResponse.answer);

		WebAPI.apiPlace = "/answer/create/";
		return WebAPI.Post(responseForm);
	}
}
