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
		
		NormalizeAnswers();
	}

	public static void UpdateQuiz (Quiz newQuiz)
	{
		quiz = newQuiz;
		quizResponse = new QuizResponse ();
		
		NormalizeAnswers();
	}

	public static void NormalizeAnswers()
	{
		quiz.answers = new string[5];

		quiz.answers[0] = quiz.option_1;
		quiz.answers[1] = quiz.option_2;
		quiz.answers[2] = quiz.option_3;
		quiz.answers[3] = quiz.option_4;
		quiz.answers[4] = quiz.option_5;
	}
}
