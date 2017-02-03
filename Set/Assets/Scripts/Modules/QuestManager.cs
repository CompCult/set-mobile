using UnityEngine;
using System.Collections;

public static class QuestManager
{
	private static Activity _activity;
	private static ActivityResponse _activityResponse;

	public static Activity activity { get { return _activity; } set { _activity = value; } }
	public static ActivityResponse activityResponse { get { return _activityResponse; } set { _activityResponse = value; } }
			
	private static Quiz _quiz;
	private static QuizResponse _quizResponse;

	public static Quiz quiz { get { return _quiz; } set { _quiz = value; } }
	public static QuizResponse quizResponse { get { return _quizResponse; } set { _quizResponse = value; } }

	public static Activity CreateActivity (string json)
	{
		return JsonUtility.FromJson<Activity>(json);
	}

	public static void UpdateActivity(string JSON)
	{
		Debug.Log("Mission updated and old quiz removed");

		_quiz = null;
		_activity = CreateActivity(JSON);
		_activityResponse = new ActivityResponse ();
	}

	public static void UpdateActivity(Activity activity)
	{
		Debug.Log("Mission updated and old quiz removed");

		_quiz = null;
		_activity = activity;
		_activityResponse = new ActivityResponse();
	}

	public static Quiz CreateQuiz (string json)
	{
		return JsonUtility.FromJson<Quiz>(json);
	}

 	public static void UpdateQuiz(string JSON)
 	{
 		Debug.Log("Quiz updated and old activity removed");

 		_activity = null;
 		_quiz = CreateQuiz(JSON);
 		_quizResponse = new QuizResponse ();
 	}

 	public static void UpdateQuiz(Quiz quiz)
 	{
 		Debug.Log("Quiz updated and old activity removed");

 		_activity = null;
 		_quiz = quiz;
 		_quizResponse = new QuizResponse ();
 	}

	public static bool AreCoordsFilled()
 	{
 		if (Application.platform != RuntimePlatform.Android) 
 			return true;

 		return (activityResponse.coord_start != null); //&&activityResponse.coord_mid != null && activityResponse.coord_end != null);
 	}
}
