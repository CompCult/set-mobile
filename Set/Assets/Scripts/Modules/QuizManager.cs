using UnityEngine;
using System.Collections;

public static class QuizManager
{
	public static Quiz quiz;
	public static QuizResponse quizResponse;

	public static void UpdateQuiz (string json)
	{
		if (quiz != null)
			JsonUtility.FromJsonOverwrite(json, quiz);
		else
			quiz = JsonUtility.FromJson<Quiz>(json);

		quizResponse = new QuizResponse ();
		quiz.NormalizeAnswers();
	}

	public static void UpdateQuiz (Quiz newQuiz)
	{
		quiz = newQuiz;
		quizResponse = new QuizResponse ();
		quiz.NormalizeAnswers();
	}
}
